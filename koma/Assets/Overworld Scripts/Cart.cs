using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    //a nice object that is used to ferry information from dungeons to overworld. :)
    //is also used to keep track of the overworld's status so it can be filled back in
    //upon return from dungeon.

    public static Cart _instance;

    //dungeon information.
    public DungeonLibrary[] dungeonStates = new DungeonLibrary[1];
    public int dunID; //id of most recent dungeon, so we know which one to update.
    public int xp; //exp we're carrying back to the overworld.
    public int gold; //gold we're carrying back to the overworld.
    public bool cleared; //cleared status which will update the dungeon.

    //current day stuff saved from overworld.
    public LeavingState leaveState; //true if we retreated on a loss
    public int actPart;
    public int[] charEventsProgress;

    //this is more lightweight than a save. we don't save history, for example.
    
    void Awake()
    {
        //singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(_instance);
    }
    
    //SAVING
    public void ow_fill_cart(Overworld world)
    {
        //overworld fills cart with information that is to be saved.
        _instance.actPart = world.actPart;

        //save charEvents progress
        _instance.charEventsProgress = world.save_evProgress();

    }
    public void dun_fill_cart(Dungeon d, int obtXP, int obtGold, LeavingState withdrawStatus)
    {
        //dungeon fills cart with information that is to be saved  and brought back
        //to the main dungeon info.
        _instance.leaveState = withdrawStatus;
        Debug.Log("_instance.leaveState = " + _instance.leaveState);
        if (withdrawStatus == LeavingState.CLEAR) _instance.cleared = true;
        else _instance.cleared = false;
        Debug.Log("dun_fill_cart(). cleared = " + _instance.cleared);

        _instance.dunID = d.get_dungeonId();
        if (_instance.dungeonStates[_instance.dunID] == null) _instance.dungeonStates[_instance.dunID] = new DungeonLibrary();
        _instance.dungeonStates[_instance.dunID].pack(d);
        _instance.xp = obtXP;
        _instance.gold = obtGold;
    }

    //RESTORING DATA
    public void ow_unload_cart(Overworld world)
    {
        //called when arriving back in overworld from dungeon.
        Debug.Log("_instance.leaveState = " + _instance.leaveState);
        //ow stuff
        world.actPart = _instance.actPart;
        world.restore_charEvents_progress(_instance);
        world.dungeonLeavingState = _instance.leaveState;
        Debug.Log(world.dungeonLeavingState);

        //add exp and gold stuff
        ExpManager.add_exp(_instance.xp, _instance.gold);

        //dungeon stuff
        world.update_dungeon_states(_instance);
        _instance.dunID = -1;
    }
    public void update_single_dungeon(int index, Dungeon dun)
    {
        _instance.dungeonStates[index].unpack(dun);
    }
    public int get_charEventsProgress(int index)
    {        
        return _instance.charEventsProgress[index];
    }

}
