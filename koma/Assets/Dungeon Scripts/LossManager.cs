using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossManager : MonoBehaviour
{
    //handles the stuff we do when the player loses a battle.
    //shows menu with 2 options:
    // -retry
    // -withdraw

    //also, holds a prebattle version of the units for current battle in case the player
    //ends up wanting to fight them again.
    private Enemy[][] preWaves;

    private int[] preBattlePartyHp;
    private int[] preBattlePartyMp;

    [SerializeField] private DungeonManager theBoss;

    public Enemy[][] get_preWaves() { return preWaves; }

    public void prebattle_fill(Queue<Enemy[]> waves)
    {
        Enemy[][] tmp = waves.ToArray();
        preWaves = new Enemy[tmp.Length][];
        for (int i = 0; i < tmp.Length; i++)
        {
            preWaves[i] = tmp[i];
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
                preBattlePartyHp[i] = party[i].get_hp();
                preBattlePartyMp[i] = party[i].get_mp();
            }
        }
    }
    public void setup_party_for_retry(Unit[] party)
    {
        //fills in the party's information when the retry option is chosen.
        //sets hp and mp. (don't need to set unit.place, because swaps are only relative to combat manager's pl, not dman's party)
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] != null)
            {
                party[i].set_hp(preBattlePartyHp[i]);
                party[i].set_mp(preBattlePartyMp[i]);
            }
        }

    }

    //on button clicks
    public void click_retry()
    {
        hide();
        theBoss.go_fight(true);
    }
    public void click_withdraw()
    {
        hide();
        theBoss.withdraw(LeavingState.LOSS);
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
