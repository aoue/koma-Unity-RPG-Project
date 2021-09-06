using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExemplarMove : Move
{
    //applies status: applies HOT to self.
    //it's a softer, but long lasting heal.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {

        //applies an amount that is dependent on level and matk and hpmax.
        //should be an int; equal to hp restored per friday's action.

        //calc here. formula is hpmax/20 + matk/10
        int toHeal = (pl[userIndex].get_hpMax_actual() / 20) + (pl[userIndex].get_matk_actual() / 10);
        pl[userIndex].status.apply_dot(toHeal, toHeal, 5, 5);

    }
}
