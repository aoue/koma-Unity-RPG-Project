using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLibrary : MonoBehaviour
{
    //all enemies in the game. This has access to them all.
    [SerializeField] private Enemy e0; //sabaind
    [SerializeField] private Enemy e1; //bow sabaind
    [SerializeField] private Enemy e2; //sabaind at large
    [SerializeField] private Enemy e3; //forest wolf


    //Note: battle ids are global things. A single id is for a single enemy group in the game.

    //used by pdm to get enemy information for the upcoming battle.
    //returns (Enemy[][] inWaves, int playerUnitLimit)
    public (Enemy[][], int) get_encounter(int id)
    {

        //Debug.Log("battleLibrary.get_encounter() called. id = " + id);
        switch (id)
        {
            case 0:
                Enemy[][] allWaves = new Enemy[1][];
                allWaves[0] =  new Enemy[]{ null, null, null, null, e0, null };
                return (allWaves, 3);

            default:
                Debug.Log("BattleLibrary.get_enemy() failed. No enemy group found.");
                break;
        }
        return (null, -1);
    }


}
