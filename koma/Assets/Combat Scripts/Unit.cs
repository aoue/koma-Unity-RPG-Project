using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //player unit, strictly speaking. enemy unit will inherit from it.

    //ui stuff
    public bool inParty { get; set; }
    [SerializeField] private Sprite moveSelectionPortrait; //dimensions?
    [SerializeField] private Sprite boxImg; //250x500 | 2:1 ratio.
    //a crit portrait? to cover a slice of the screen like in trails in the sky? a bit more than that though; include like head and upper body?

    //base stats
    [SerializeField] protected int exp; //unspent exp. increases and decreases just fine. (for enemy, it's how much exp they drop.)
    [SerializeField] protected int level;
    [SerializeField] protected string nom;
    [SerializeField] private int apMax;
    protected int ap;
    [SerializeField] protected int hpMax;
    protected int hp;
    [SerializeField] private int affinity;
    protected int mp; //greater/eq to 0 for player and determines how much the unit adds to the party's stamina, positive number for enemy (move picking influencer)
    [SerializeField] private int mpMax; //the unit's max stamina.
    [SerializeField] protected int patk;
    [SerializeField] protected int pdef;
    [SerializeField] protected int matk;
    [SerializeField] protected int mdef;
    protected bool ooa; //true: unit is ooa, cannot be selected. false: unit is active. can be selected.

    //targeting
    public int place { get; set; } //position on the battlefield. 0-5. equivalent to their position in pl.
    public Move nextMove { get; set; } //next scheduled move.
    public int targetSpot { get; set; } //0-5. 

    //status
    public Status status { get; set; }

    //there's also gear- but we're going to ignore it for now.
    //it will: add flat stat bonuses, add a defensive affinity, add an offensive affinity for moves that allow that.

    //finally, there's also the moveset. only 5 moves.
    //a null value means an empty moveslot
    [SerializeField] private DefendMove defendMove; //the 1 defend move the user has equipped.
    [SerializeField] protected Move[] moveset; //the 5 moves the unit has equipped. set at dungeon start.
    private List<Move> allKnownMoves; //all the moves the unit knows.

    //MODIFIERS
    
    public void inc_exp(int x) { exp += x; }
    public void create_status()
    {
        ooa = false;
        if (status == null) status = new Status();
    }
    public virtual bool refresh(bool startOfBattle)
    {
        //called at the start of a round.
        if (ooa == true) return false; //don't do any of this if out of action.
        
        ap = get_apMax_actual();

        //handle status    
        bool expired = false;
        if (startOfBattle == true)
        {           
            status.reset(this);
        }
        else
        {
            expired = status.decline(this);
        }
        return expired;
    }
    public bool wasKilled()
    {
        //returns true if unit is dead.
        //sets ooa to true and ap to 0.
        if ( hp == 0)
        {
            ooa = true;
            ap = 0;
            return true;
        }
        return false;
    }
    public void set_hp(int amount) { hp = amount; }
    public void drain_ap(int amount) { ap -= amount; }
    public void set_mp(int x) { mp = x; }
    public void drain_mp(int amount) { mp = (int)(mp - (amount * status.trance)); }
    public void mp_heal(int amount) { if (!ooa) hp = Mathf.Min(get_hpMax_actual(), hp + amount); }
    public void damage(int amount) { hp = Mathf.Max(0, hp - amount); }
    public void heal(int amount) { if (!ooa) hp = Mathf.Min(get_hpMax_actual(), hp + amount); }

    //GETTERS
    public Sprite get_activePortrait() { return moveSelectionPortrait; }
    public int get_exp() { return exp; }
    public int get_level() { return level; }
    public DefendMove get_defendMove() { return defendMove; }
    public Move[] get_moveset() { return moveset; }
    public int get_mp() { return mp; } //not for use in combat.
    public int get_mpMax() { return mpMax; } //not for use in combat.
    public Sprite get_boxImg() { return boxImg; }
    public string get_nom() { return nom; }
    public int get_apMax() { return apMax; }
    public int get_ap() { return ap; }
    public int get_hpMax() { return hpMax; }
    public int get_hp() { return hp; }
    public int get_affinity() { return affinity; }
    public int get_patk() { return patk; }
    public int get_pdef() { return pdef; }
    public int get_matk() { return matk; }
    public int get_mdef() { return mdef; }
    public bool get_ooa() { return ooa; }

    //GET ACTUALS - add gear increases and status multipliers.
    public int get_hpMax_actual() { if (status == null) return hpMax; return (int)(hpMax * (1f + status.hp)); }
    public int get_apMax_actual() { return Mathf.Max(0, apMax + status.ap); }
    public int get_patk_actual() { return (int)(patk * status.patk); }
    public int get_pdef_actual() { return (int)(pdef * status.pdef); }
    public int get_matk_actual() { return (int)(matk * status.matk); }
    public int get_mdef_actual() { return (int)(mdef * status.mdef); }

}
