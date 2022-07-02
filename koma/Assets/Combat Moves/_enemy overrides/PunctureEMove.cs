using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunctureEMove : EnemyMove
{
    //applies status: target's pdef is decreased.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        //amount, am ceil, set dur, dur ceil
        
        bool canApply = target.status.update_status_state(target, buffType, explanationStr);

        //then, apply your buff if and only if the target's status state is not neutral.
        if (canApply)
        {
            target.status.pdef_up(0.8f, 2);
        }
    }
}
