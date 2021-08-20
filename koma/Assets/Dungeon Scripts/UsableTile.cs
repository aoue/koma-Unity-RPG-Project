using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { FontOfHp, FontOfStamina }
public class UsableTile : Tile
{
    

    //so, this holds all the different kinds of tiles.
    [SerializeField] private TileType type; //tells the game what to do when you hit the useTile button on this tile.
    [SerializeField] private SpriteRenderer sparkleEffectRendy; //used to show/hide the tile's sparkle effect.

    public override void turn_on_sparkle()
    {
        //only does something for usableTiles
        sparkleEffectRendy.enabled = true;
    }
    public override void turn_off_sparkle()
    {
        //only does something for usableTiles
        sparkleEffectRendy.enabled = false;
    }
    public override void use_tile(DungeonManager dman)
    {
        //first, disable this tile's sparkle.
        sparkleEffectRendy.enabled = false;

        //figure out what kind of tile it is and then do its thing
        switch (type)
        {
            case TileType.FontOfHp:
                //for all the units in the party, restore some percentage of hp.
                Debug.Log("tile is font of hp");
                for(int i = 0; i < 6; i++)
                {
                    if (DungeonManager.party[i] != null)
                    {
                        DungeonManager.party[i].heal((int)(DungeonManager.party[i].get_hpMax() * 0.3f));
                    }
                }
                break;
            case TileType.FontOfStamina:
                //increases the stamina.
                Debug.Log("tile is font of stamina");
                DungeonManager.stamina += 30;
                break;


        }

    }


}
