using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChewEMove : EnemyMove
{
    //applies status: give target some dot.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        //amount, am ceil, set dur, dur ceil
        target.status.apply_dot(4, 20, 3, 3);
    }
}
