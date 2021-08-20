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
    [SerializeField] private DungeonManager theBoss;

    public Enemy[][] get_preWaves() { return preWaves; }
    public int[] get_prebattle_partyFill() { return preBattlePartyHp; }
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
        Debug.Log("testing loser's filling.");
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
        preBattlePartyHp = new int[party.Length];
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] != null)
                preBattlePartyHp[i] = party[i].get_hp();
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
