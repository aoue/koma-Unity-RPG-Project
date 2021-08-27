using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    //status conditions: (de)buffs, stat increases, and stat multipliers.

    //how multipliers work.
    //every move that increases a stat multiplier has two values: inc, ceil
    //  - it sets the multiplier: Max(multiplier + inc, ceil)

    //about durations. duration is measured in rounds.
    //every move that increases a stat has two values: dur, ceil
    //  - it sets the (either stat or dot) duration: Max(duration + dur, ceil)

    // ap
    public int ap_duration { get; set; } //duration of added ap, in rounds.
    public int ap; //flat increase.

    // hp
    public int hp_duration { get; set; } //duration of added hp, in rounds.
    public float hp { get; set; } //multiplier for additionnal hp of hpmax. starts at 0 and adds hp*unit.get_hpmax() hp

    // stats
    public int stat_duration { get; set; } //duration of stat mults, in rounds.
    public float trance { get; set; } //multiplie to stamina cost payed of moves.
    public float patk { get; set; } //multiplier to said stat.
    public float pdef { get; set; } //"
    public float matk { get; set; } //"
    public float mdef { get; set; } //"
    public float dmg_taken { get; set; } //"
    public float dmg_dealt { get; set; } //"

    public int dot_duration { get; set; } //duration of dots (both kinds), in rounds.
    public int bad_dot { get; set; }  //flat number
    public int good_dot { get; set; } //"

    public string generate_string_left()
    {
        //generates a string to describe the unit's status condition.
        string toRet = "";
        if (stat_duration != 0)
        {
            toRet += "Stat dur: " + stat_duration;
            if (dmg_dealt != 1.0) toRet += "\nDMG out x" + dmg_dealt;
            if (dmg_taken != 1.0) toRet += "\nDMG in x" + dmg_taken;
        }
        if (hp_duration != 0)
        {
            toRet += "\nhp_dur: " + hp_duration;
            if (hp != 0) toRet += "\nHP up: +" + hp;
        }
        if (stat_duration != 0 && trance != 1.0f) toRet += "\nMP mult: " + trance;
        return toRet;
    }
    public string generate_string_right()
    {
        //generates a string to describe the unit's status condition.

        string toRet = "";
        if (ap_duration != 0) toRet = "\nap: " + ap + " (" + ap_duration + " turns)";

        if (dot_duration == 0) return toRet;
        toRet += "\nDots dur: " + dot_duration;
        if (bad_dot != 1) toRet += "\nDOT -" + bad_dot;
        if (good_dot != 1) toRet += "\nHOT +" + good_dot;          
        
        return toRet;
    }

    public void reset(Unit u)
    {
        //sets everything to its default state.
        dot_reset();
        stat_reset();
        hp_reset(u);
        ap_reset();
    }
    public bool decline(Unit u)
    {
        //stat mods decrease. if they hit 0, then stats are reset.
        bool validExpiry = false;
        if (hp_duration > 0)
        {
            validExpiry = true;
            hp_duration--;
        }
        if (dot_duration > 0)
        {
            validExpiry = true;
            dot_duration--;
        }
        if (stat_duration > 0)
        {
            validExpiry = true;
            stat_duration--;
        }
        if (ap_duration > 0)
        {
            validExpiry = true;
            ap_duration--;
        }

        bool expires = false;
        if (hp_duration == 0)
        {
            expires = true;
            hp_reset(u);
        }
        if (dot_duration == 0)
        {
            expires = true;
            dot_reset();
        }
        if (stat_duration == 0)
        {
            expires = true;
            stat_reset();
        }
        if (ap_duration == 0)
        {
            expires = true;
            ap_reset();
        }
        return expires & validExpiry; //only true if both are true. neat bit of bool logical operators.
    }
    void ap_reset()
    {
        ap_duration = 0;
        ap = 0;
    }
    void stat_reset()
    {        
        stat_duration = 0;
        trance = 1.0f;
        patk = 1.0f;
        pdef = 1.0f;
        matk = 1.0f;
        mdef = 1.0f;
        dmg_dealt = 1.0f;
        dmg_taken = 1.0f;
    }
    void dot_reset()
    {
        dot_duration = 0;
        bad_dot = 0;
        good_dot = 0;
    }
    void hp_reset(Unit u)
    {
        hp_duration = 0;
        hp = 0;
        hp_expires(u);
    }

    //HP
    public void apply_hp(float amount, float am_ceiling, int duration, int dur_ceiling)
    {
        if (hp_duration < dur_ceiling)
            hp_duration = Mathf.Min(hp_duration + duration, dur_ceiling);

        if (hp < am_ceiling)
            hp = Mathf.Min(hp + amount, am_ceiling);
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
    public void ap_duration_up(int duration, int dur_ceiling)
    {
        if (ap_duration < dur_ceiling)
            ap_duration = Mathf.Min(ap_duration + duration, dur_ceiling);
    }
    public void ap_up(int amount, int am_ceiling, int duration, int dur_ceiling)
    {
        //increase amount
        if ( amount > 0) //increasing ap
        {
            ap = Mathf.Min(ap + amount, am_ceiling);
            
        }
        else //decreasing ap
        {
            ap = Mathf.Max(ap + amount, am_ceiling);
        }
        //increase duration
        ap_duration = Mathf.Min(ap_duration + duration, dur_ceiling);
    }

    //STATS
    private float get_double(float current_amount, float toAdd, float ceiling)
    {
        //returns what current_amount should be set to should be.
        if (toAdd > 0) //we're increasing the stat
        {
            if (current_amount < ceiling)
                return Mathf.Min(current_amount + toAdd, ceiling);
        }
        else //we're decreasing the stat
        {
            if (current_amount > ceiling)
                return Mathf.Max(current_amount + toAdd, ceiling);
        }
        return current_amount;
    }
    public void stat_duration_up(int duration, int dur_ceiling)
    {
        //handles increasing (or not) stat duration stuff.
        if (stat_duration < dur_ceiling)
            stat_duration = Mathf.Min(stat_duration + duration, dur_ceiling);
    }   
    public void trance_up(float amount, float am_ceiling, int dur, int dur_ceiling)
    {
        trance = get_double(trance, amount, am_ceiling);
        if ( trance > am_ceiling)
        {
            trance =  Mathf.Max(trance + amount, am_ceiling);
        }
        stat_duration_up(dur, dur_ceiling);
    }
    public void patk_up(float amount, float am_ceiling, int dur, int dur_ceiling)
    {
        patk = get_double(patk, amount, am_ceiling);
        stat_duration_up(dur, dur_ceiling);
    }
    public void pdef_up(float amount, float am_ceiling, int dur, int dur_ceiling)
    {
        pdef = get_double(pdef, amount, am_ceiling);
        stat_duration_up(dur, dur_ceiling);
    }
    public void matk_up(float amount, float am_ceiling, int dur, int dur_ceiling)
    {
        matk = get_double(matk, amount, am_ceiling);
        stat_duration_up(dur, dur_ceiling);
    }
    public void mdef_up(float amount, float am_ceiling, int dur, int dur_ceiling)
    {
        mdef = get_double(mdef, amount, am_ceiling);
        stat_duration_up(dur, dur_ceiling);
    }
    public void dmgTaken_up(float amount, float am_ceiling, int dur, int dur_ceiling)
    {
        dmg_taken = get_double(dmg_taken, amount, am_ceiling);
        stat_duration_up(dur, dur_ceiling);
    }
    public void dmgDealt_up(float amount, float am_ceiling, int dur, int dur_ceiling)
    {
        dmg_dealt = get_double(dmg_dealt, amount, am_ceiling);
        stat_duration_up(dur, dur_ceiling);
    }
    

    //DOTS
    //units are damaged/healed from dots each time they act.
    public void apply_dot(int amount, int am_ceiling, int duration, int dur_ceiling)
    {
        if (amount > 0 && am_ceiling > good_dot)
        {
            good_dot = Mathf.Min(good_dot + amount, am_ceiling);
        }
        else if (amount < 0 && am_ceiling > bad_dot)
        {
            bad_dot = Mathf.Max(bad_dot + amount, am_ceiling); //here, the am_ceiling will be a floor. it's a dot we can't surpass.
        }

        if (dot_duration < Mathf.Min(dot_duration + duration, dur_ceiling))
        {
            dot_duration = Mathf.Min(dot_duration + duration, dur_ceiling);
        }
        
    }
    public int take_dot()
    {
        //take_dot returns an int that will be added to the unit's hp.
        return good_dot - bad_dot;
    }


}
