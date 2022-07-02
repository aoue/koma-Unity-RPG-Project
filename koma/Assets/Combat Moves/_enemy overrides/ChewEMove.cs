using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChewEMove : EnemyMove
{
    //applies status: give target some dot.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        //amount, am ceil, set dur, dur ceil
        
        bool canApply = target.status.update_status_state(target, buffType, explanationStr);
        //then, apply your buff if and only if the target's status state is not neutral.
        if (canApply)
        {
            target.status.apply_dot(-4, 3);
        }
    }
}
