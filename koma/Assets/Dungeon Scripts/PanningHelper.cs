using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanningHelper : MonoBehaviour
{
    //when mouse enters this region: cammera dragging allowed
    //when mouse leaves this region: camera dragging not allowed

    [SerializeField] private DungeonManager dMan;

    public void enter_area()
    {
        dMan.set_canDragScreen(true);

    }
    public void leave_area()
    {
        dMan.set_canDragScreen(false);

    }

}
