using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExceedMove : Move
{
    //applies status: patk+, pdef+, matk+, mdef+ to self.
    //not too strong buff, but it buffs all four primary stats.

    
    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {

        //applies an amount that is dependent on level and matk and hpmax.
        //should be an int; equal to hp restored per friday's action.

        //calc here. formula is hpmax/20 + matk/10
        //int toHeal = (pl[userIndex].get_hpMax_actual() / 20) + (pl[userIndex].get_matk_actual() / 10);

        //to weak, new formula is hpmax/15 + matk/5.
        //lvl 1 friday: heals (140/10 + 95/5) =  14 + 19 = 33
        int toHeal = (target.get_hpMax_actual() / 10) + (pl[userIndex].get_matk_actual() / 5);
        target.status.apply_dot(toHeal, toHeal, 5, 5);

    }
}
