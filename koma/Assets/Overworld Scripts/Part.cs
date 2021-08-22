using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    //the new 'dayHolder' for this design of overworld. yeah.

    public static List<int> doneEvents; //parralel array with partEvents. keeps track of which events have already been done.
                                     //is reset each time we go to a new day.

    [SerializeField] private Event immediateEvent; //if null, do nothing. if not null, then on part, play event first directly.

    [SerializeField] private string partName; //the name for the part. shown instead of boring old 'part _'
    [SerializeField] private GameObject dungeonToRepeat;
    [SerializeField] private bool hasMandatoryDungeon;
    [SerializeField] private bool startPassTimeButtonActive; //controls whether the next day button should be inactive on day start.
    [SerializeField] private bool[] startDungeonsEnabled; //controls whether dungeons should start inactive or not. true=start inactive.
    [SerializeField] private EventHolder[] partEvents; //all the RED (mandatory) and BLUE (optional, missable) events in this part.
    [SerializeField] private AudioClip dayAudio; //what audio the game should start playing on day. if null, don't do anything.

    public Event get_immediateEvent() { return immediateEvent; }
    public string get_partName() { return partName; }
    public bool get_hasMandatoryDungeon() { return hasMandatoryDungeon; }
    public AudioClip get_dayAudio() { return dayAudio; }

    public void reset_doneEvents()
    {
        if (doneEvents == null) doneEvents = new List<int>();
        doneEvents.Clear();        
        //starts each part empty.
    }
    public void load_part(Overworld theWorld, bool repeatingPart)
    {
        //goes in and sets up all the part's events and other states. (passtime button, dungeons enabled, etc).

        if (hasMandatoryDungeon && repeatingPart == true)
        {
            //even if repeating, mandatory dungeon must be shown.
            //meaning we need a link to the mandatory dungeon.
            dungeonToRepeat.SetActive(true);
        }

        //set pass time button
        theWorld.get_nextDayButton().GetComponent<UnityEngine.UI.Button>().interactable = startPassTimeButtonActive;

        //show all valid events.
        //add all to active_events because they may become valid later in the day or something.
        for (int i = 0; i < partEvents.Length; i++)
        {
            if ( !doneEvents.Contains(partEvents[i].get_id()) )
            {
                theWorld.add_active_event(partEvents[i], partEvents[i].get_id());
                partEvents[i].gameObject.SetActive(false);

                if (partEvents[i].validate_event() == true)
                {
                    partEvents[i].setup_event();
                }
            }  
        }

        //finally, adjust dungeon status now
        for (int i = 0; i < startDungeonsEnabled.Length; i++)
        {
            if (startDungeonsEnabled[i] == true)
            {
                theWorld.get_dunHolders()[i].enable_dungeon(false, true);
            }
            else
            {
                theWorld.get_dunHolders()[i].disable_dungeon();
            }
        }
    }

    public void banish_part()
    {
        //called the day after time has passed.
        //hides all its events that may or may not be gone.

        foreach (EventHolder evh in partEvents)
        {
            if (evh != null)
            {
                evh.gameObject.SetActive(false);

            }
        }
    }


}
