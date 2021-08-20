using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : Event
{
    //Inherits from event. 
    //uses on dayProgression variable to enforce ordering.

    [SerializeField] protected int postEventProgression; //set progression to this when done.
    [SerializeField] protected bool enablePassTimeAfter; //if true, then enable next day button when done.
    [SerializeField] protected bool enableDungeonsAfter; //if true, then enable dungeons after this event. (only interactable, does not show hidden ones)


    public override void post_event()
    {
        //often different, so leave it to each. default is nothing.
        //this would be were:
        // - an item is given to the player
        // - a unit learns a move
        // - rels are modified

        Overworld.dayProgression = postEventProgression;
        if (enablePassTimeAfter == true)
        {           
            Overworld.enable_nextDayButton();
        }
        if (enableDungeonsAfter)
        {
            Overworld.enable_shownDungeons();
        }
    }

}
