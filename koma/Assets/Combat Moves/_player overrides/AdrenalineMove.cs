using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineMove : Move
{
    //applies status: increases hp and hpmax

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        pl[userIndex].status.apply_hp(0.3f, 0.3f, 3, 3);

    }
}
