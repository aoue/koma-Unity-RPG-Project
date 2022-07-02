using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiningLightMove : Move
{
    //applies status: target's patk and matk are decreased.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        //amount, am ceil, set dur, dur ceil
        
        bool canApply = target.status.update_status_state(target, buffType, explanationStr);
        //then, apply your buff if and only if the target's status state is not neutral.
        if (canApply)
        {
            target.status.patk_up(0.85f, 2);
            target.status.matk_up(0.85f, 2);
        }
    }
}

