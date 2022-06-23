using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitPool : MonoBehaviour
{
    //An object that holds references to all player units.
    //used to add and remove player units from the pdm's reserve party.

    //Unit legend:
    // -mc: 0
    // -friday: 1
    // -yve: 2
    // -nai: 3
    // -
    // -
    // -

    [SerializeField] private Unit[] allUnits;

    public Unit get_unit(int id)
    {
        return allUnits[id];
    }
}
