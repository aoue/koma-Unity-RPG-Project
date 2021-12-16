using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    //enemy class.
    //inherits from unit.
    //its additionnal fields and functions are for dungeon ai stuff.


    //calculating unit priority.
    [SerializeField] private bool doSpawningVariance; //if false, do not do anything when spawning_variance() is called.
    [SerializeField] private int default_priority; //added to unit's priority
    [SerializeField] private int discipline; //[-discipline, discipline] forms a range that a random number is gen. and added to priority.
    [SerializeField] private float concerned_level; //unit is concerned when hp / hpmax is below this. range 0-1, inclusive.
    [SerializeField] private int concerned_priority; //added to unit's priority when unit is concerned.
    [SerializeField] private bool isHealer; //true if the unit is a healer.
    [SerializeField] private bool isBuffer; //true if the unit is a buffer.
    [SerializeField] private int healer_priority; //added to unit's priority if an ally is concerned.
    
    public float get_break_multiplier() { return break_multiplier; }

    //move picking system.
    [SerializeField] private int mpRegen; //the amount of mp the unit regens per round.
    private bool useHealIfPicked; //help healers fulfill their role.

    //overrides from parent class.
    public virtual void stat_modify(int threat)
    {
        //called by dungeon. increases the monster's level and stats a threat-based number of times. may also give it new moves.
        //can be very specific, so we'll override this function for each enemy type.

        //things to increase:
        // -level
        // -hpmax
        // -patk, pdef, matk, mdef
        // -exp (given out, that is.)
    }
    protected int stat_modify_helper(int first, int second)
    {
        //randomly returns first or second.
        return UnityEngine.Random.Range(first, second + 1);
        
    }
    public void spawning_variance()
    {
        if (doSpawningVariance == false) return;
        
        //called by dungeon when an instance of a mob class is spawned. we go in and 
        //randomly apply some spread to their stats so the enemies aren't all the same
        //since that would be boring.

        //vary them not by a set amount, but by a percentage.
        //between 0.85f & 1.1f
        float[] varArr = new float[7];
        for(int i = 0; i < 7; i++)
        {
            varArr[i] = UnityEngine.Random.Range(0.95f, 1.1f);
        }

        //stats to vary:
        // -level
        level = (int)(level * varArr[0]);

        // -hpmax
        hpMax = (int)(hpMax * varArr[1]);

        // -starting stamina
        mp = (int)(mp * varArr[2]);

        // -patk, pdef, matk, mdef
        patk = (int)(patk * varArr[3]);
        pdef = (int)(pdef * varArr[4]);
        matk = (int)(matk * varArr[5]);
        mdef = (int)(mdef * varArr[6]);
    }
    public override bool refresh(bool startOfBattle)
    {
        isScheduled = false;
        if (startOfBattle)
        {
            hp = get_hpMax_actual();
            break_level = 0;
            ooa = false;
            mp = UnityEngine.Random.Range(20, 60);
        }
        else if (ooa == true) return false; //don't do any of this if out of action.

        ap = get_apMax_actual();
        mp += mpRegen;

        brokenThisRound = false;

        //handle status  
        bool expired = false;
        if (startOfBattle == true)
        {
            status.reset(this);
        }
        else
        {
            //break_level = break_level / 2;
            if (break_level == 100) break_level = 99;

            expired = status.decline(this);
        }
        return expired;
    }

    //AI
    public bool is_concerned()
    {
        if (((float)hp) / ((float)get_hpMax_actual()) <= concerned_level)
        {
            return true;
        }
        return false;
    }
    public int calc_priority(bool needHealing)
    {
        //returns the unit's priority to act in battle.
        //takes a variety of other serialized variables into account, as well as the 
        //unit's current state, to check this.

        int pri = default_priority;
        
        //concerned
        if ( is_concerned() == true ) pri += concerned_priority;

        //healing
        if (needHealing == true) { pri += healer_priority; useHealIfPicked = true; }

        //disciple
        pri += UnityEngine.Random.Range(-discipline, discipline);
        
        return pri;
    }
    public EnemyMove pick_move(int roll, bool canPickEOR)
    {
        //we have roll.
        //we know whether we can pick EOR moves: canPickEOR
        //we know whether we must pick a heal  : useHealIfPicked
        //lastly: as long as not move0, enemies will ignore a move that is not in their row.

        preferredRow userRow;
        if (place > 2) userRow = preferredRow.FRONT;
        else userRow = preferredRow.BACK;


        EnemyMove chosenMove = null;
        if ( useHealIfPicked == true )
        {
            useHealIfPicked = false;
            for (int i = moveset.Length - 1; i >= 0; i--)
            {
                //Debug.Log("H. looking at move " + i);
                //valid move requiremets:

                // if not last move, then unit must be in the move's preferred row
                if (i != 0 && moveset[i].get_preferredRow() != preferredRow.AMBI && moveset[i].get_preferredRow() != userRow)
                {
                    break;
                }

                // ap >= apDrain, stam drain < roll, does not conflict with EOR, isHeal is true.               
                if ( moveset[i].get_mpDrain() <= roll && moveset[i].get_isHeal() == true && !(moveset[i].get_phase() == executionTime.ENDOFROUND && canPickEOR == false) && ap >= moveset[i].get_apDrain())
                {
                    //then, move is valid. :)
                    Debug.Log("enemy picking healing move with index " + i);
                    chosenMove = (EnemyMove)moveset[i];
                    break;
                }
            }           
        }
        if ( chosenMove == null )
        {
            //then we couldn't find a healing move we liked. go through and look again, but this time, it cannot be a healing move.
            for (int i = moveset.Length - 1; i >= 0; i--)
            {
                //valid move requiremets:

                // if not last move, then unit must be in the move's preferred row
                if (i != 0 && moveset[i].get_preferredRow() != preferredRow.AMBI && moveset[i].get_preferredRow() != userRow)
                {
                    break;
                }

                // stam drain < roll, does not conflict with EOR, isHeal is false.
                if (moveset[i].get_mpDrain() <= roll && moveset[i].get_isHeal() == false && !(moveset[i].get_phase() == executionTime.ENDOFROUND && canPickEOR == false) && ap >= moveset[i].get_apDrain())
                {
                    //then, move is valid. :)
                    Debug.Log("enemy picking move with index " + i);
                    chosenMove = (EnemyMove)moveset[i];
                    break;
                }
            }
        }

        if (chosenMove == null) chosenMove = (EnemyMove)moveset[0]; //last ditch effort

        mp -= Mathf.Max(0, chosenMove.get_mpDrain());
        return chosenMove;
    }


}
