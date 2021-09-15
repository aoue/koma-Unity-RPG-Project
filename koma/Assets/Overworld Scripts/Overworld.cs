﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum state { FIRSTTIME, RETURNING, LOADING }
public class Overworld : MonoBehaviour
{
    public static Overworld _instance;
    //script to control all overworld scenes
    // - it interacts with Event class objects (like telling them to load, etc)
    // - it loads other overworld locations and advances days.
    // - it launches the player on missions
    // - it allows item management and things like that.
    // - allows the player to save. (clicking on a special building)

    //game state
    [SerializeField] private Cart cart;
    public Dictionary<int, EventHolder> active_events; //holds a ref to all eventholders that are currently showing. used to update them.
    public int actPart { get; set; } //controls game flow. equivalent to a dayHolder.
    public static int dayProgression { get; set; } //controls game flow with respect to a sequence of story beats. hhow far we are in a certain actPart.
    public LeavingState dungeonLeavingState { get; set; } //LOSS, WITHDRAW, CLEAR. controls parts in pass time button.
    private bool isDaytime; //day or night.

    //serialized members - UI
    [SerializeField] private Button nextDayButton; //the next day button
    [SerializeField] private CharHolder[] charHolders; //holds character events.
    [SerializeField] private Part[] partHolders; //holds unique events for a given day; usually red.
    [SerializeField] private DungeonHolder[] dunHolders; //holds the area's dungeons. used to access them in a_new_day(), as specified in today's dayholder.
    [SerializeField] private Image bg; // the overworld area's background image.
    [SerializeField] private Text actPartText; // displays act part information.

    //managers
    [SerializeField] private SoundManager SM; //handles playing music and sounds, baby.
    [SerializeField] private FadeManager fader; //handles fading, baby.
    [SerializeField] private PrepDungeonManager pdm; //shows prep dungeon menu and controls transition to dungeon scene.
    [SerializeField] private BackgroundManager backgroundManager; //holds all backgrounds. 
    [SerializeField] private Notifier notifier; //handles notifications.

    private static state firstTime = state.FIRSTTIME; //used to detect new game

    
    void Awake()
    {
        //fader.fade_from_black();
        //^we don't use this here because it interferes with immediate loading.

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        
        active_events = new Dictionary<int, EventHolder>();
        ExpManager.setup();
    }
    void Start()
    {
        //new game?
        if (firstTime == state.FIRSTTIME)
        {
            firstTime = state.RETURNING;

            //set things up for day0, then.
            actPart = 0;
            dayProgression = 0;
            //setup managers for first time
            pdm.first_time_setup();
            isDaytime = true;
            a_new_part(0, false);
        }
        else if (firstTime == state.RETURNING)
        {
            //return from dungeon stuff
            //let cart help.            
            cart.ow_unload_cart(this);
            back_from_dungeon();
        }
        else //firstTime == state.LOADING
        {
            //loading game stuff
        }
    }

    //MANAGING DUNGEONS
    public static void enable_shownDungeons()
    {
        for (int i = 0; i < _instance.dunHolders.Length; i++)
        {
            _instance.dunHolders[i].enable_dungeon(true, true);
        }
    }
    public static void add_to_party(Unit[] newPartyMembers)
    {
        //makes pdm do all the dirty work
        _instance.pdm.add_to_party(newPartyMembers);
    }
    public static void open_dungeonPrepMenu(Dungeon dun)
    {
        _instance.pdm.load_up(dun);
    }

    //NEXT DAY BUTTON
    IEnumerator passTime_visuals(int a, bool b)
    {
        //controls the visual effects when the pass time button is pressed.
        //fade to black and back and play the pass time song.

        fader.fade_to_black(0.5f);
        SM.play_passTime();

        yield return new WaitForSeconds(2f);
        do_part(a, b);
    }
    public void passTime_pressed()
    {
        //called when next_day() is pressed.
        //this button only progresses us to the next day if we suceeded in our dungeon.
        //if we failed, then we're stuck.
        Debug.Log("passTime pressed.");

        if (partHolders[actPart].get_hasMandatoryDungeon() == true)
        {
            switch (dungeonLeavingState)
            {
                case LeavingState.LOSS:
                    //we lost the dungeon. we repeat the day.
                    Debug.Log("passTime pressed - loss.");
                    StartCoroutine(passTime_visuals(0, true));
                    break;
                case LeavingState.WITHDRAW:
                    //we withdrew from the dungeon. we repeat the day.
                    Debug.Log("passTime pressed - withdraw.");
                    StartCoroutine(passTime_visuals(0, true));
                    break;
                case LeavingState.CLEAR: //we can advacne to the next part.
                    Debug.Log("passTime pressed - clear.");
                    StartCoroutine(passTime_visuals(1, false));
                    break;
            }
        }
        else
        {
            Debug.Log("passTime pressed; forget doing a dungeon.");
            StartCoroutine(passTime_visuals(1, false));
        }
        
    }
    void do_part(int toAddToPart, bool toPassToPart)
    {
        a_new_part(toAddToPart, toPassToPart);
    }

    //MANAGING EVENTS  
    public static void progress_charEvent(int whichCharID)
    {
        _instance.charHolders[whichCharID].progress++;
        //Debug.Log("progressed charHolders[" + whichCharID + "] to progress = " + charHolders[whichCharID].progress);
        Debug.Log("(_instance ver.) progressed charHolders[" + whichCharID + "] to progress = " + _instance.charHolders[whichCharID].progress);
    }
    void a_new_part(int addToPart, bool repeating)
    {
        if (repeating == false) //stop the player from repeating events.
        {
            partHolders[actPart].reset_doneEvents();
            
        }

        //wipes the slate clean for a new day.
        // -clear the active_events dictionary.
        //what else? i know, autosave ;).
        actPart += addToPart;

        //reset day progression
        dayProgression = 0;

        //EVENTS
        //clear events; 
        active_events.Clear();

        //banish/load day events
        if (actPart > 0 && partHolders[actPart] != null) partHolders[actPart].banish_part();
        if (partHolders[actPart] != null) partHolders[actPart].load_part(this, repeating); //<-- sets up dungeon states in here too. 

        //banish/load char events:
        if (actPart > 2) //or whatever it is.
        {
            for (int i = 0; i < charHolders.Length; i++)
            {
                charHolders[i].banish_day();
                charHolders[i].prepare(this); //many char events will only be available at a certain part.
            }
        }
        

        //dungeon decay
        for (int i = 0; i < dunHolders.Length; i++)
        {
            dunHolders[i].dungeon_decay();
        }

        dungeonLeavingState = LeavingState.WITHDRAW;

        //update display
        update_display(); 
        set_background();
        SM.play_background_music(partHolders[actPart].get_dayAudio());

        //finally, check if the pary has an immediate event. if it does, then go to that event.
        if (partHolders[actPart].get_immediateEvent() != null)
        {
            // false tells ev manager not to do its pre-event pause
            EventManager.event_triggered(partHolders[actPart].get_immediateEvent(), false);
        }
    }
    public void add_active_event(EventHolder evholder, int id)
    {
        //touching a dictionary: <int, eventholder>
        active_events.Add(id, evholder);
    }
    public static void remove_active_event(int id)
    {
        //touching a dictionary: <int, eventholder>
        _instance.active_events.Remove(id);
    }
    public static void check_active_events_status()
    {
        //touching a dictionary: <int, eventholder>
        //go in and check whether each event is still allowed to be taken.
        //if it is: great, do nothing.
        //if it isn't: color it to show this and make it uninteractable.
        foreach (var keyValuePair in _instance.active_events)
        {
            //Debug.Log("checking");
            if (keyValuePair.Value.validate_event() == true)
            {
                keyValuePair.Value.setup_event();
            }
            else
            {
                keyValuePair.Value.disable_button();
            }
        }
    }

    //UPDATE UI
    public static void enable_nextDayButton()
    {
        _instance.nextDayButton.interactable = true;
    }
    void set_background()
    {
        bg.sprite = backgroundManager.get_backgroundSprite(actPart);
    }

    //SAVING
    public void fill_cart()
    {
        //called right before we load dungeon
        cart.ow_fill_cart(this);
    }
    public int[] save_evProgress()
    {
        int[] toRet = new int[charHolders.Length];
        for (int i = 0; i < toRet.Length; i++)
        {
            toRet[i] = charHolders[i].progress;
        }
        return toRet;
    }

    //RETURNING
    void back_from_dungeon()
    {
        //called when we get back from a dungeon and arrive back in the overworld.
        update_display();
        set_background();
        partHolders[actPart].ready_night();

        //setup background music. it's nightime, so it's a special track that gets played every night.
        SM.play_nightMusic();

        //distribute exp and heal party units.
        pdm.inc_exp();
    }
    public void restore_charEvents_progress(Cart cart)
    {
        for(int i = 0; i < charHolders.Length; i++)
        {
            charHolders[i].progress = cart.get_charEventsProgress(i);
        }
    }
    public void update_dungeon_states(Cart cart)
    {
        //called as part of returning to overworld from a dungeon.
        //we copy all the states of dungeons into here so they are not lost.

        //we've got dunHolders; those are the ones we have to update.
        for(int i = 0; i < dunHolders.Length; i++)
        {
            Dungeon tmp = dunHolders[i].get_dungeon();
            cart.update_single_dungeon(i, tmp);
            dunHolders[i].set_dungeon(tmp);
        }

    }

    //DISPLAY
    void update_display()
    {
        //sets act part progress text.
        actPartText.text = partHolders[actPart].get_partName();
    }


    //getters
    public Button get_nextDayButton() { return nextDayButton; }
    public DungeonHolder[] get_dunHolders() { return dunHolders; }
}
