using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessMove : Move
{
    //applies status: increases dmg dealt.
    //type: buff.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        //so, it is a buff.
        //This means it will only affect units whose states are currently neutral or buffed.

        //first, call update_status_state()
        bool canApply = target.status.update_status_state(target, buffType, explanationStr);

        //then, apply your buff if and only if the target's status state is not neutral.
        if (canApply)
        {
            target.status.dmgDealt_up(1.25f, 3);
        }

    }
}
