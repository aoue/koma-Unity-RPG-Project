using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLibrary : MonoBehaviour
{
    //all player units in the game. This has access to the pool, which has access to them all.
    [SerializeField] private PlayerUnitPool plPool;

    //all enemies in the game. This has access to them all.
    [SerializeField] private Enemy e0; //sabaind
    [SerializeField] private Enemy e1; //bow sabaind
    [SerializeField] private Enemy e2; //sabaind at large
    [SerializeField] private Enemy e3; //forest wolf


    //Note: battle ids are global things. A single id is for a single enemy group in the game.

    //used by pdm to get enemy information for the upcoming battle.
    //returns (Enemy[][] inWaves, int threat, int playerUnitLimit)
    public (Enemy[][], int, int) get_encounter(int id)
    {
        //threat is used to level the enemies up, remember.
        //Debug.Log("battleLibrary.get_encounter() called. id = " + id);
        switch (id)
        {
            case 0:             
                return battle0();
            case 1:
                return battle1();                
            default:
                Debug.Log("BattleLibrary.get_enemy() failed. No matching group found for id " + id);
                break;
        }
        return (null, -1, -1);
    }

    private (Enemy[][], int, int) battle0() { Enemy[][] allWaves = new Enemy[1][]; allWaves[0] = new Enemy[6] { null, null, null, null, e0, null }; return (allWaves, 0, 3); }
    private (Enemy[][], int, int) battle1() { Enemy[][] allWaves = new Enemy[1][]; allWaves[0] = new Enemy[6] { null, e2, null, e0, e0, e0 }; return (allWaves, 0, 3); }

    //=================================================================

    public Unit[] get_deployment(int id)
    {
        //returns a preset player formation. Used to skip the pdm.
        //The id of a preset deployment is the same as the id of the battle it is for.

        //(in the future, you can make a thing to include only the units that are in the reserve party.)
        switch (id)
        {
            case 0:
                return deploy0();
            default:
                Debug.Log("BattleLibrary.get_deployment() failed. No matching case found for id " + id);
                break;
        }
        return null;
    }

    private Unit[] deploy0() { return new Unit[6] { null, plPool.get_unit(1), null, null, plPool.get_unit(0), plPool.get_unit(2) }; }

}
