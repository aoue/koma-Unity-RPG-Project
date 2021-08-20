using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncourageMove : Move
{
    //applies status: decreases stam mult.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        target.status.trance_up(-0.2f, -0.7f, 3, 3);

    }
}
