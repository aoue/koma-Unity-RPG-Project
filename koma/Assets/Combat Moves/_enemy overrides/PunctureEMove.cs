using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunctureEMove : EnemyMove
{
    //applies status: target's pdef is decreased.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        //amount, am ceil, set dur, dur ceil
        target.status.pdef_up(-0.2f, 0.5f, 2, 2);
    }
}
