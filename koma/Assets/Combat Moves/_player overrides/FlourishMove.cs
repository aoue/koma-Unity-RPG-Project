using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlourishMove : Move
{
    //applies status: self ap decreased by 1 for 1 turn. (dur 2?)

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        
        bool canApply = pl[userIndex].status.update_status_state(target, buffType, explanationStr);
        //then, apply your buff if and only if the target's status state is not neutral.
        if (canApply)
        {
            pl[userIndex].status.ap_up(0, 1);
        }
    }
}
