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
        //int toHeal = (pl[userIndex].get_hpMax_actual() / 20) + (pl[userIndex].get_matk_actual() / 10);

        //to weak, new formula is hpmax/15 + matk/5.
        //lvl 1 friday: heals (140/10 + 95/5) =  14 + 19 = 33
        

        bool canApply = target.status.update_status_state(target, buffType, explanationStr);
        //then, apply your buff if and only if the target's status state is not neutral.
        if (canApply)
        {
            int toHeal = (target.get_hpMax_actual() / 10) + (pl[userIndex].get_matk_actual() / 5);
            target.status.apply_dot(toHeal, 5);
        }
    }
}
