using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { FontOfTriumph, FontOfHp, FontOfMp, SmallTreasure }
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
            case TileType.FontOfTriumph:
                //for all the units in the party, restore full hp and mp.
                for (int i = 0; i < 6; i++)
                {
                    if (DungeonManager.party[i] != null)
                    {
                        DungeonManager.party[i].set_hp(DungeonManager.party[i].get_hpMax());
                        DungeonManager.party[i].set_mp(DungeonManager.party[i].get_hpMax());
                    }
                }
                dman.display_pbp_message("Fully restored party HP and MP.");
                break;
            case TileType.FontOfHp:
                //for all the units in the party, restore some percentage (30%) of hp.
                for(int i = 0; i < 6; i++)
                {
                    if (DungeonManager.party[i] != null)
                    {
                        DungeonManager.party[i].heal((int)(DungeonManager.party[i].get_hpMax() * 0.3f));
                    }
                }
                dman.display_pbp_message("Partially restored party HP.");
                break;
            case TileType.FontOfMp:
                //for all the units in the party, restore some percentage (30%) of mp.
                for (int i = 0; i < 6; i++)
                {
                    if (DungeonManager.party[i] != null)
                    {
                        DungeonManager.party[i].mp_heal((int)(DungeonManager.party[i].get_mpMax() * 0.3f));
                    }
                }
                dman.display_pbp_message("Partially restored party MP.");
                break;
            case TileType.SmallTreasure:
                //roll for loot; is an amount of money within a certain range, added to by threat.
                //randomly generate golden within a certain range and depending on the threat, raise the floor and ceiling.
                int threatBonus = 5 * dman.get_threat();
                int goldAmount = UnityEngine.Random.Range(80 + threatBonus, 100 + threatBonus);
                dman.obtainedGold += goldAmount;
                dman.display_pbp_message("Gained " + goldAmount + " gold.");
                break;
        }

    }


}
