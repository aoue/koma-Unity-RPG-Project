using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlourishMove : Move
{
    //applies status: self ap decreased by 1 for 1 turn. (dur 2?)

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        pl[userIndex].status.ap_up(-1, -3, 1, 1);

    }
}
