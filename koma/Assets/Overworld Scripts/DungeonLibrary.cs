using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonLibrary
{
    //we're going to be serializing this class, too.

    //saves all the information of a single dungeon.
    //for each dungeon:
    // -deepest floor reached
    // -exploration status of each floors
    // -unique mob party status of each floors

    private Exploration[,] explorationStatus;
    private bool[,] usedStatus;
    private bool cleared;
    private bool[] uniqueMobStatus; //does not yet exist in dungeons, though.

    public void pack(Dungeon dun)
    {
        //scoops up info from the dungeon in question and saves it.
        //exploration info.       
        explorationStatus = dun.explored_grid;
        //tileUsed info.
        usedStatus = dun.tileUsedGrid;
    }
    public void unpack(Dungeon dun)
    {
        //returns info to the dungeon in question.
        dun.cleared = cleared;
        dun.explored_grid = explorationStatus;
        dun.tileUsedGrid = usedStatus;
    }


}
