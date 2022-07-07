using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum buffState { NEUTRAL, BUFFED, DEBUFFED }; //remember, it's a ladder. 
public enum defendState { NOT, DEFEND, BARRIER };

public class Status
{
    //a unit may only have a single stat modifier on at a time. So, they can either be affected by a buff, by nothing, or by a debuff.

    //status conditions: (de)buffs, stat increases, and stat multipliers.

    //how multipliers work.
    //every move that increases a stat multiplier has two values: inc, ceil
    //  - it sets the multiplier: Max(multiplier + inc, ceil)

    //a status, when applied, overwrites the previous status.

    //defending
    public defendState defState { get; set; } //reset to NOT at round start.

    // ap
    public int ap_duration { get; set; } //duration of added ap, in rounds.
    public int ap; //flat increase.

    // hp
    public float hp { get; set; } //multiplier for additionnal hp of hpmax. starts at 0 and adds hp*unit.get_hpmax() hp

    // stats
    public int duration { get; set; } //duration of stat mults, in rounds.
    public float trance { get; set; } //multiplie to stamina cost payed of moves.
    public float patk { get; set; } //multiplier to said stat.
    public float pdef { get; set; } //"
    public float matk { get; set; } //"
    public float mdef { get; set; } //"
    public float dmg_taken { get; set; } //"
    public float dmg_dealt { get; set; } //"

    public int dot_duration { get; set; } //duration of dots (both kinds), in rounds.
    public int dot { get; set; } // flat number. Adds or subtracts from hp.

    //ui
    private buffState state;
    private string explanation_text; //explains the current status affliction. Assigned by the status move during use.

    //UI - Preview slot status text generation.
    // it shows the explanation text by the status.
    public string generate_string()
    {
        string defStr = "";
        switch (defState)
        {
            case defendState.NOT:
                defStr = "";
                break;
            case defendState.DEFEND:
                defStr = "(Defending) ";
                break;
            case defendState.BARRIER:
                defStr = "(Barrier up) ";
                break;
        }

        if (duration > 1)
        {
            return defStr + explanation_text + "\n" + duration + " turns remaining";
        }
        else if (duration == 1)
        {
            return defStr + explanation_text + "\n" + duration + " turn remaining";
        }
        return defStr;
    }

    public bool update_status_state(Unit u, buffState moveType, string explanation)
    {
        //adjust ui elements of the status and update state.
        status_ladder(moveType);
        if (state == buffState.NEUTRAL)
        {
            reset(u);
            return false;
        }
        else
        {
            explanation_text = explanation;
            return true;
        }      
    }
    void status_ladder(buffState moveType)
    {
        if (moveType == buffState.BUFFED)
        {
            if (state == buffState.NEUTRAL) state = buffState.BUFFED;
            else if (state == buffState.DEBUFFED) state = buffState.NEUTRAL;
        }
        else if (moveType == buffState.DEBUFFED)
        {
            if (state == buffState.NEUTRAL) state = buffState.DEBUFFED;
            else if (state == buffState.BUFFED) state = buffState.NEUTRAL;
        }
    }
    public void reset(Unit u)
    {
        explanation_text = "";
        duration = 0;
        trance = 1.0f;
        patk = 1.0f;
        pdef = 1.0f;
        matk = 1.0f;
        mdef = 1.0f;
        dmg_dealt = 1.0f;
        dmg_taken = 1.0f;
        dot = 0;
        hp = 0;
        hp_expires(u);
    }
    public bool decline(Unit u)
    {
        //stat mods duration decrease. if they hit 0, then stats are reset.
        bool validExpiry = false;
        if (duration > 0)
        {
            validExpiry = true;
            duration--;
        }
        bool expires = false;
        if (duration == 0)
        {
            expires = true;
        }

        if (expires & validExpiry) //only true if both are true. neat bit of bool logical operators.
        {
            reset(u);
            return true;
            
        }
        return false;
    }
    

    //HP
    public void apply_hp(float amount, int dur)
    {
        duration = dur;
        hp = amount;
    }
    void hp_expires(Unit unit)
    {
        //called when a unit's hp buff expires. hp_duration = 0; hp = 0; <- these have both already happened.
        if (unit.get_hp() > unit.get_hpMax_actual())
        {
            unit.set_hp(unit.get_hpMax_actual());
        }      
    }

    //AP
    public void ap_up(int amount, int dur)
    {
        //increase amount
        ap = amount;
        duration = dur;
    }

    //STATS
    public void trance_up(float amount, int dur)
    {
        trance = amount;
        duration = dur;
    }
    public void patk_up(float amount, int dur)
    {
        patk = amount;
        duration = dur;
    }
    public void pdef_up(float amount, int dur)
    {
        pdef = amount;
        duration = dur;
    }
    public void matk_up(float amount, int dur)
    {
        matk = amount;
        duration = dur;
    }
    public void mdef_up(float amount, int dur)
    {
        mdef = amount;
        duration = dur;
    }
    public void dmgTaken_up(float amount, int dur)
    {
        dmg_taken = amount;
        duration = dur;
    }
    public void dmgDealt_up(float amount, int dur)
    {
        dmg_dealt = amount;
        duration = dur;
    }
    

    //DOTS
    //units are damaged/healed from dots each time they act.
    public void apply_dot(int amount, int duration)
    {
        dot = amount;
        duration = duration;
    }
    public int take_dot()
    {
        //take_dot returns an int that will be added to the unit's hp.
        return dot;
    }


}
