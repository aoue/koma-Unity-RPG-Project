using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinPartyEvent : Beat
{
    //inherits from event.
    //a type of event that adds an array of units to the party

    [SerializeField] private Unit[] unitsToAdd;

    public override void post_event()
    {
        //often different, so leave it to each. default is nothing.
        //this would be were:
        // - an item is given to the player
        // - a unit learns a move
        // - rels are modified
        //post_event_message();
        Overworld.add_to_party(unitsToAdd);
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
