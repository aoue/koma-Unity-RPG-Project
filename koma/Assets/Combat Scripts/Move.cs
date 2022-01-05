using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum executionTime { INSTANT, NEXTTURN, ENDOFROUND }
public enum preferredRow { FRONT, BACK, AMBI }
public class Move : MonoBehaviour
{
    //moves that Units hold. they need: (delete when added)

    // -finally, any extra effects, represented by a virtual function.
    [SerializeField] private int moveID; //for player move's only. the move's unique id. used for restoring unit state on loads.
    [SerializeField] private AudioClip moveSound;
    [SerializeField] private string nom; //move's name

    [SerializeField] private bool targetsParty; //true: targets player units, false: targets enemy units
    [SerializeField] private bool mustTargetSelf; //true: targets only self. only used for player units, by the way.
    [SerializeField] private bool isHeal; //true: heal move, false: damage move.
    [SerializeField] private bool isStatus; //true: applies a status effect, false: does not.
    [SerializeField] private bool selfStatus; //true: applies status to self only, false: applies status to enemies only. if isStatus is false, then does nothing.
    [SerializeField] private bool selfStatusTextColorGreen; //true: self status text shows in green, false: shows in red.
    [SerializeField] private string statusText; //if isStatus is true, then this is the string that should show the status text.

    [SerializeField] private executionTime phase; //when is the move executed: instantly, in one turn, at the end of the round
    [SerializeField] private preferredRow preferredRow; //what row does the move not suffer penalties in: front, back, neither(ambi)

    [SerializeField] private int strikes; //the number of times the move hits.
    [SerializeField] private int power; //the move's base damage multiplier
    [SerializeField] private float break_mult; //the move's base damage multiplier
    [SerializeField] private float lowSpread; //the lowest possible spread multiplier the move can roll
    [SerializeField] private float highSpread; //the highest possible spread multiplier the move can roll
    [SerializeField] private int xSize; //how many tiles in the x area does the move hit: 1, 2, 3
    [SerializeField] private int ySize; //how many tiles in the y area does the move hit: 1, 2
    
    [SerializeField] protected int affinity; //the move's affinity. affects damage multiplier.
    [SerializeField] private bool usesPatk; //true: the user uses patk. false: the user uses matk.
    [SerializeField] private bool usesPdef; //true: the target uses pdef. false: the user uses mdef.
    [SerializeField] private int apDrain; //how much ap the user loses when they use this move.
    [SerializeField] private int mpDrain; //how much stamina the party loses when this move is used.
    [SerializeField] private string flavour; //move's flavour text. part of the descr.

    public virtual void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        //exists to be overriden.
        //this is where a move could apply poison, stat mods, etc.
    }

    public void set_affinity(int set)
    {
        affinity = set;
    }

    public bool get_selfStatusTextColorGreen() { return selfStatusTextColorGreen; }
    public bool get_selfStatus() { return selfStatus; }
    public int get_moveID() { return moveID; }
    public AudioClip get_sound() { return moveSound; }
    public string get_nom() { return nom; }
    public bool get_targetsParty() { return targetsParty; }
    public bool get_mustTargetSelf() { { return mustTargetSelf; } }
    public int get_apDrain() { return apDrain; }
    public int get_mpDrain() { return mpDrain; }
    public bool get_isHeal() { return isHeal; }
    public bool get_isStatus() { return isStatus; }
    public string get_statusText() { return statusText; }
    public executionTime get_phase() { return phase; }
    public int get_xSize() { return xSize; }
    public int get_ySize() { return ySize; }
    public int get_strikes() { return strikes; }
    public int get_power() { return power; }
    public float get_breakMult() { return break_mult; }
    public float get_lowSpread() { return lowSpread; }
    public float get_highSpread() { return highSpread; }
    public int get_affinity() { return affinity; }
    public bool get_usesPatk() { return usesPatk; }
    public bool get_usesPdef() { return usesPdef; }
    public string get_flavour() { return flavour; }
    public preferredRow get_preferredRow() { return preferredRow; }

    public string generate_preview_text()
    {
        //creates the string that will be shown in the move preview text.
        //you've got 3 lines.

        //need to show:
        // -phase

        string toReturn = "";

        switch (preferredRow)
        {
            case preferredRow.FRONT:
                toReturn = "(Front";
                break;
            case preferredRow.BACK:
                toReturn = "(Back";
                break;
            case preferredRow.AMBI:
                toReturn = "(Ambi";
                break;
        }
        switch (phase)
        {
            case executionTime.INSTANT:
                toReturn += "/Instant) ";
                break;
            case executionTime.NEXTTURN:
                toReturn += "/Next turn) ";
                break;
            case executionTime.ENDOFROUND:
                toReturn += "/End of round) ";
                break;
        }

        if (phase == executionTime.ENDOFROUND)
        {
            toReturn += nom + " | all AP, " + mpDrain + " MP\n";
        }
        else
        {
            toReturn += nom + " | " + apDrain + " AP, " + mpDrain + " MP\n";
        }

        if (isHeal)
        {
            toReturn += "Heal ";
        }
        else
        {
            toReturn += "Atk ";
        }
        toReturn += "Power: " + power;

        if (!isHeal)
        {
            toReturn += ", BRK x" + break_mult;
        }

        toReturn += " | " + xSize + "x" + ySize + " | ";

        if (isHeal)
        {
            toReturn += AffKeyWords.get_affName(affinity) + "/";
        }

        if (usesPatk)
        {
            toReturn += "Patk vs. ";
        }
        else
        {
            toReturn += "Matk vs. ";
        }

        if (usesPdef)
        {
            toReturn += "Pdef";
        }
        else
        {
            toReturn += "Mdef";
        }

        toReturn += "\n<i>" + flavour + "</i>";

        return toReturn;
    }
    
}
