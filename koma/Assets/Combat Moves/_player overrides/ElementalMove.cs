using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalMove : Move
{
    //applies status: randomly changes its affinity to any non-normal (0-5) affinity each time used.

    public override void apply_status(Unit target, Unit[] pl, Enemy[] el, int userIndex)
    {
        
        this.set_affinity(UnityEngine.Random.Range(0, 2 + 1));
    }


}
