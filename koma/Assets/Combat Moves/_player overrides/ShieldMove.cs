﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMove : Move
{
    //applies status: increases dmg resisted.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        
        bool canApply = target.status.update_status_state(target, buffType, explanationStr);
        //then, apply your buff if and only if the target's status state is not neutral.
        if (canApply)
        {
            target.status.dmgTaken_up(0.75f, 3);
        }
    }
}
