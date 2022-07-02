using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncourageMove : Move
{
    //applies status: decreases stam mult.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {

        bool canApply = target.status.update_status_state(target, buffType, explanationStr);
        //then, apply your buff if and only if the target's status state is not neutral.
        if (canApply)
        {
            target.status.trance_up(0.5f, 3);
        }
        

    }
}
