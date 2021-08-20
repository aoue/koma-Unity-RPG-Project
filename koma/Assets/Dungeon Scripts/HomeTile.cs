using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTile : Tile
{
    //kind of tile.
    //unremarkable, no unique event here.
    //once explored, can never become unexplored.

    //when loading a dungeon, the player can choose to start on any of these tiles.

    private DungeonManager dMan;
    private bool validStartingSpot;
    [SerializeField] private SpriteRenderer sparkleEffectRendy; //used to show/hide the tile's sparkle effect.

    [SerializeField] private bool isExit;
    [SerializeField] private bool isClear;

    public override void turn_on_sparkle()
    {
        //only does something for usableTiles
        sparkleEffectRendy.enabled = true;
    }

    //when the tile is clicked.
    void Start()
    {
        dMan = DungeonManager.get_dMan();
        //rendy = gameObject.GetComponent<SpriteRenderer>();
        //if (isExplored) set_tile_image();
    }
    
    public override void set_valid()
    {
        validStartingSpot = true;
        //also set alpha to 1f
        Color temp = get_rendy().color;
        temp.a = 1f;
        get_rendy().color = temp;
    }
    public void clicked()
    {
        if (validStartingSpot == true) dMan.picked_home_tile(get_x(), get_y());
    }

    public override bool isValidExit() { return isExit; }
    public override bool get_isClear() { return isClear; }

}
