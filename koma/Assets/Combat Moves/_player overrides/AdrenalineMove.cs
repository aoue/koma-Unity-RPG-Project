using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineMove : Move
{
    //applies status: increases hp and hpmax

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        //apply_hp(float amount, float am_ceiling, int duration, int dur_ceiling)
        

        bool canApply = pl[userIndex].status.update_status_state(target, buffType, explanationStr);
        //then, apply your buff if and only if the target's status state is not neutral.
        if (canApply)
        {
            pl[userIndex].status.apply_hp(1.3f, 4);
        }
    }
}
