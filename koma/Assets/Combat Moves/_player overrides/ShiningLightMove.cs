using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiningLightMove : Move
{
    //applies status: target's patk and matk are decreased.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        //amount, am ceil, set dur, dur ceil
        target.status.patk_up(-0.15f, 0.7f, 2, 2);
        target.status.matk_up(-0.15f, 0.7f, 2, 2);
    }
}

