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

    //save some more stuff too:
    //-threat
    //-expediton counter
    //-explored tiles
    private int threat;
    private int expeditonCounter;
    private int exploredTiles;


    public void pack(Dungeon dun)
    {
        //scoops up info from the dungeon in question and saves it.
        //exploration info.       
        explorationStatus = dun.explored_grid;
        threat = dun.threat;
        expeditonCounter = dun.expeditionCounter;
        exploredTiles = dun.exploredTiles;
        //tileUsed info.
        usedStatus = dun.tileUsedGrid;
    }
    public void unpack(Dungeon dun)
    {
        //returns info to the dungeon in question.
        dun.cleared = cleared;
        dun.threat = threat;
        dun.exploredTiles = exploredTiles;
        dun.expeditionCounter = expeditonCounter;
        dun.explored_grid = explorationStatus;
        dun.tileUsedGrid = usedStatus;
    }


}
