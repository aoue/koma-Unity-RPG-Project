using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMove : Move
{
    //applies status: increases dmg resisted.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        target.status.dmgTaken_up(-0.25f, 0.75f, 3, 3);
    }
}
