using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessMove : Move
{
    //applies status: increases dmg dealt.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        Debug.Log("Yes, i used bless on " + target.get_nom());
        target.status.dmgDealt_up(0.25f, 1.25f, 3, 3);
    }
}
