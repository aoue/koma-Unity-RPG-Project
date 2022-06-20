using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossManager : MonoBehaviour
{
    //handles the stuff we do when the player loses a battle.
    //shows menu with 2 options:
    // -retry
    // -withdraw

    [SerializeField] private PrepDungeonManager pdm;

    //also, holds a prebattle version of the units for current battle in case the player
    //ends up wanting to fight them again.
    private Enemy[][] preWaves;

    private int[] preBattlePartyHp;
    private int[] preBattlePartyMp;

    public Enemy[][] get_preWaves() { return preWaves; }

    public void prebattle_fill(Enemy[][] waves)
    {
        preWaves = new Enemy[waves.Length][];
        for (int i = 0; i < waves.Length; i++)
        {
            preWaves[i] = waves[i];
        }

        //checking
        /*
        Debug.Log("testing lossManager's filling.");
        for(int i = 0; i < preWaves.Length; i++)
        {
            Debug.Log("preWaves[" + i + "]:");
            for (int j = 0; j < preWaves[i].Length; j++)
            {
                Debug.Log("preWaves[" + i + "][" + j + "] = " + preWaves[i][j]);
            }
        }
        */
    }
    public void prebattle_partyFill(Unit[] party)
    {
        //save the party's information
        preBattlePartyHp = new int[party.Length];
        preBattlePartyMp = new int[party.Length];
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] != null)
            {
                party[i].set_preBattlePosition(i);
                preBattlePartyHp[i] = party[i].get_hp();
                preBattlePartyMp[i] = party[i].get_mp();
            }
        }
    }
    public void setup_party_for_retry(Unit[] party)
    {
        //fills in the party's information when the retry option is chosen.
        //sets hp and mp.
        Unit[] toFill = new Unit[6];
        //first, replace the party in their original spots.

        for (int j = 0; j < toFill.Length; j++)
        {
            //find the proper unit. we want to find the unit whose preBattlePosition == i
            Unit whoIsIt = null;
            for (int i = 0; i < party.Length; i++)
            {
                if (party[i] != null && party[i].get_preBattlePosition() == j)
                {
                    whoIsIt = party[i];
                }
            }
            toFill[j] = whoIsIt;
        }
    
        //give them back their previous stats.
        for (int i = 0; i < toFill.Length; i++)
        {
            if (toFill[i] != null)
            {
                toFill[i].set_hp(preBattlePartyHp[i]);
                toFill[i].set_mp(preBattlePartyMp[i]);               
            }
        }
        
        party = toFill;
    }

    //on button clicks
    public void click_retry()
    {
        //hide the loss manager
        hide();

        //and restart the battle.
        pdm.retry();
        
        


        //theBoss.go_fight(true);
    }
    public void click_withdraw()
    {
        hide();
        //theBoss.reporter_shortcut();
    }


    //show and hide
    public void show()
    {
        gameObject.SetActive(true);
    }
    void hide()
    {
        gameObject.SetActive(false);
    }

}
