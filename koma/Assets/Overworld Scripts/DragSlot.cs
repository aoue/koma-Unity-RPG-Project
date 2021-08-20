using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSlot : MonoBehaviour, IDropHandler
{
    //the slots things are dragged into

    [SerializeField] PrepDungeonManager daBoss;
    [SerializeField] private int slotID;

    public void OnDrop(PointerEventData eventData)
    {
        //we drop whatever it was we were dropping onto the whole unitBox.
        //the game has to figure out what we meant to switch out, which it can do easily.
        //(except for moves, maybe.)

        //Debug.Log("box " + DragDrop.pickedUpID + " was dropped on slot " + slotID);
        if (DragDrop.pickedUpID == -1) return;
        daBoss.swap_active_units(slotID);



    }


}
