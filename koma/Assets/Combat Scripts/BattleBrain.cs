using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BattleBrain
{
    //the battlebrain, used by combat.
    //used for damage or healing calculations and the like.

    //affinity table
    //2d array where elements are multipliers.

    private float[,] affMultArray = new float[7, 7]
    {
        {1f, 0.5f, 1.5f, 1f, 1f, 1f, 1f},
        {1.5f, 1f, 1f, 0.5f, 1f, 1f, 1f},
        {0.5f, 1f, 1f, 1.5f, 1f, 1f, 1f},
        {1f, 1.5f, 0.5f, 1f, 1f, 1f, 1f},
        {1f, 1f, 1f, 1f, 0.5f, 2.0f, 1f},
        {1f, 1f, 1f, 1f, 2.0f, 0.5f, 1f},
        {1f, 1f, 1f, 1f, 1f, 1f, 1f}
    };

    //UNIVERSAL CALCULATION FUNCTIONS
    float p_defenderPenalty(Enemy target)
    {
        //player version. returns 0.8f if the (enemy) target is in the back, 1f if not.
        if (target.place < 3) return 0.80f;
        return 1f;
    }
    float e_defenderPenalty(Unit target)
    {
        //enemy version. returns 0.8f if the (unit) target is in the back, 1f if not.
        if (target.place > 2) return 0.80f;
        return 1f;
    }
    float p_calculate_row_mod(Move move, int unitPosition)
    {
        //player version. player board positions are mirrored to enemies.
        //0-2: front.
        //3-5: back.
        switch (move.get_preferredRow())
        {
            case preferredRow.FRONT:
                if (unitPosition < 3) return 1.0f;
                return 0.65f;
            case preferredRow.BACK:
                if (unitPosition > 2) return 1.0f;
                return 0.65f;
            case preferredRow.AMBI:
                return 1.0f;
        }
        return 1.0f;
    }
    float e_calculate_row_mod(Move move, int unitPosition)
    {
        //enemy version. enemy board positions are mirrored to player.
        //0-2: front.
        //3-5: back.
        switch (move.get_preferredRow())
        {
            case preferredRow.FRONT:
                if (unitPosition > 2) return 1.0f;
                return 0.65f;
            case preferredRow.BACK:
                if (unitPosition < 3) return 1.0f;
                return 0.65f;
            case preferredRow.AMBI:
                return 1.0f;
        }
        return 1.0f;
    }
    public float calculate_aff_mod(int attacker_aff, int defender_aff)
    {
        return affMultArray[attacker_aff, defender_aff];
    }
    int calculate_damage(int level, int power, int atk, int def, float affMod, float spread, float rowMod, float defenderMod, float dmgDealtMod, float dmgTakenMod)
    {        
        int damage = Convert.ToInt32(((atk * (power + (level * 2))) / Mathf.Max(1, def)) * affMod * spread * rowMod * defenderMod * dmgDealtMod * dmgTakenMod);
            
        return damage;
    }
    int calculate_healing(int level, int power, int atk, int def, float spread, float rowMod)
    {
        //heals don't care about affinity OR defender penalty
        //unit's defense is divided by 2. target's defense shoud matter... but not make them unhealable.

        int heal = Convert.ToInt32(((atk * (power + (level * 2))) / Mathf.Max(1, (def / 2))) * spread * rowMod);

        return heal;
    }
    

    //the ints here are the damage/heal number. they will be returned 
    //to combatManager and shown as damage numbers.
    //PLAYER UNITS ACTING
    public List<int> unit_attacks(Unit user, Enemy[] targets, Move move, int userSpot)
    {
        //row multiplier
        float rowMod = p_calculate_row_mod(move, userSpot);
        float spread = UnityEngine.Random.Range(move.get_lowSpread(), move.get_highSpread());

        List<int> dmgList = new List<int>(); 
        foreach(Enemy target in targets)
        {
            if ( target != null)
            {
                int atk;
                int def;
                float affMod = calculate_aff_mod(move.get_affinity(), target.get_affinity());
                float defenderPen = p_defenderPenalty(target);

                if (move.get_usesPatk() == true) atk = user.get_patk_actual();
                else atk = user.get_matk_actual();

                if (move.get_usesPdef() == true) def = target.get_pdef_actual();
                else def = target.get_mdef_actual();

                dmgList.Add(calculate_damage(user.get_level(), move.get_power(), atk, def, affMod, spread, rowMod, defenderPen, user.status.dmg_dealt, target.status.dmg_taken));
            }
            else
            {
                dmgList.Add(-1);
            }
            
        }
        return dmgList;
    }
    public List<int> unit_heals(Unit user, Unit[] targets, Move move, int userSpot)
    {
        //row multiplier
        float rowMod = p_calculate_row_mod(move, userSpot);
        float spread = UnityEngine.Random.Range(move.get_lowSpread(), move.get_highSpread());


        List<int> healList = new List<int>();
        foreach (Unit target in targets)
        {
            if (target != null)
            {
                int atk;
                int def;

                if (move.get_usesPatk() == true) atk = user.get_patk_actual();
                else atk = user.get_matk_actual();

                if (move.get_usesPdef() == true) def = target.get_pdef_actual();
                else def = target.get_mdef_actual();

                healList.Add(calculate_healing(user.get_level(), move.get_power(), atk, def, spread, rowMod));
            }
            else
            {
                healList.Add(-1);
            }

        }

        return healList;
    }

    //MOBS ACTING
    public List<int> enemy_attacks(Enemy user, Unit[] targets, Move move, int userSpot)
    {
        //row multiplier
        float rowMod = e_calculate_row_mod(move, userSpot);
        float spread = UnityEngine.Random.Range(move.get_lowSpread(), move.get_highSpread());

        List<int> dmgList = new List<int>();

        foreach (Unit target in targets)
        {
            if (target != null)
            {
                int atk;
                int def;
                float affMod = calculate_aff_mod(move.get_affinity(), target.get_affinity());
                float defenderPen = e_defenderPenalty(target);

                if (move.get_usesPatk() == true) atk = user.get_patk_actual();
                else atk = user.get_matk_actual();

                if (move.get_usesPdef() == true) def = target.get_pdef_actual();
                else def = target.get_mdef_actual();

                dmgList.Add(calculate_damage(user.get_level(), move.get_power(), atk, def, affMod, spread, rowMod, defenderPen, user.status.dmg_dealt, target.status.dmg_taken));
            }
            else
            {
                dmgList.Add(-1);
            }

        }

        return dmgList;

    }
    public List<int> enemy_heals(Enemy user, Enemy[] targets, Move move, int userSpot)
    {
        //row multiplier
        float rowMod = e_calculate_row_mod(move, userSpot);
        float spread = UnityEngine.Random.Range(move.get_lowSpread(), move.get_highSpread());


        List<int> healList = new List<int>();
        foreach (Enemy target in targets)
        {
            if (target != null)
            {
                int atk;
                int def;

                if (move.get_usesPatk() == true) atk = user.get_patk_actual();
                else atk = user.get_matk_actual();

                if (move.get_usesPdef() == true) def = target.get_pdef_actual();
                else def = target.get_mdef_actual();

                healList.Add(calculate_healing(user.get_level(), move.get_power(), atk, def, spread, rowMod));
            }
            else
            {
                healList.Add(-1);
            }

        }
        return healList;
    }

}
