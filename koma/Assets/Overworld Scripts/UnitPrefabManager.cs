using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPrefabManager : MonoBehaviour
{
    //answers to Overworld.
    //has a ref to all party units.
    //used to set their stats. 
    // -either on a new game and setting them to defaults,
    // -or on a loaded game, setting them to the stored values in the save.

    [SerializeField] private Unit mcUnit;
    [SerializeField] private Move[] mcMoves;

    [SerializeField] private Unit fridayUnit;
    [SerializeField] private Move[] fridayMoves;

    [SerializeField] private Unit mothUnit;
    [SerializeField] private Move[] mothMoves;


    public void set_units_to_default()
    {
        //sets all unit stats to their default values.
        /*
        //template: unit ?
        mcUnit.set_level();
        mcUnit.set_exp();
        mcUnit.set_hpMax();
        mcUnit.set_mpMax();
        mcUnit.set_patk();
        mcUnit.set_pdef();
        mcUnit.set_matk();
        mcUnit.set_mdef();
        //set moves:
        mcUnit.get_moveset()[0] = 
        */

        //Mc: unit 0
        mcUnit.set_level(1);
        mcUnit.set_exp(0);
        mcUnit.set_hpMax(100);
        mcUnit.set_mpMax(30);
        mcUnit.set_patk(70);
        mcUnit.set_pdef(80);
        mcUnit.set_matk(110);
        mcUnit.set_mdef(80);
        //set moves: 0 magic arrow, 1 shield, 2 aftershock, 3 cutting breeze, null
        for (int i = 0; i < 4; i++)
        {
            mcUnit.get_moveset()[i] = mcMoves[i];
        }
        mcUnit.get_moveset()[4] = null;
        mcUnit.set_known_moveIds(new List<int> { 0, 1, 2, 3 });

        //Friday: unit 1
        fridayUnit.set_level(1);
        fridayUnit.set_exp(100);
        fridayUnit.set_hpMax(140);
        fridayUnit.set_mpMax(30);
        fridayUnit.set_patk(95);
        fridayUnit.set_pdef(115);
        fridayUnit.set_matk(95);
        fridayUnit.set_mdef(90);
        //set moves: 0 swordplay, 1 light, 2 shining light, 3 adrenaline, null
        for (int i = 0; i < 4; i++)
        {
            fridayUnit.get_moveset()[i] = fridayMoves[i];
        }
        fridayUnit.get_moveset()[4] = null;
        fridayUnit.set_known_moveIds( new List<int> { 0, 1, 2, 3 } );

        //Moth: unit 2
        mothUnit.set_level(1);
        mothUnit.set_exp(0);
        mothUnit.set_hpMax(90);
        mothUnit.set_mpMax(40);
        mothUnit.set_patk(60);
        mothUnit.set_pdef(75);
        mothUnit.set_matk(85);
        mothUnit.set_mdef(105);
        //set moves: 0 staff, 1 renew, 2 encourage, 3 bless, null
        for (int i = 0; i < 4; i++)
        {
            mothUnit.get_moveset()[i] = mothMoves[i];
        }
        mothUnit.get_moveset()[4] = null;
        mothUnit.set_known_moveIds(new List<int> { 0, 1, 2, 3 });

        Debug.Log("unitprefabmanager.set_units_to_default() done.");
    }

}
