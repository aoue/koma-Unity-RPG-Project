using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    //the building blocks of a dungeon.
    //all tiles of a certain type are the same.
    //for instance: 
    // -all combat tiles carry an id that they are combat tiles, and tell the dungeonmanager that. Then,
    //  it's the dungeon manager that does all the work from there. None of that info is stored in the tile.

    [SerializeField] private bool startsVisible; //determines whether the tile's location starts known.
    [SerializeField] private string tileName; //determines the name that the player will know the tile by.
    [SerializeField] private int tileDrain; //the amount of stamina drained from the party when they walk over it.
    [SerializeField] private int vision; //how far the party can see on a tile. it's higher on a mountain than a valley, for example.
    [SerializeField] private Sprite exploredSprite; //the sprite the tile will have when it has been explored
    [SerializeField] private SpriteRenderer rendy; //used to show/hide the tile
     
    private int x;
    private int y;
    public bool hasEvent;

    public virtual bool isValidExit() { return false; }
    public virtual bool get_isClear() { return false; }

    //ADJUST IMAGE BASED ON EXPLORATION
    public void set_tile_image() //explored
    {
        rendy.sprite = exploredSprite;
    }
    public void set_sprite(Sprite pain) //fog
    {
        rendy.sprite = pain;
    }
    public void hide_tile() //unknown
    {
        rendy.enabled = false;
    }
    public void show_tile()
    {
        rendy.enabled = true;
    }

    public Sprite get_exploredSprite() { return exploredSprite; }
    
    //virtuals
    public virtual void turn_off_sparkle() { }
    public virtual void turn_on_sparkle() { }
    public virtual void use_tile(DungeonManager dman) { }
    public virtual void enter_tile() { }
    public virtual void set_valid() { }

    public SpriteRenderer get_rendy() { return rendy; }
    public string get_tileName() { return tileName; }
    public int get_tileDrain() { return tileDrain; }
    public int get_vision() { return vision; }
    public int get_x() { return x; }
    public int get_y() { return y; }
    public void set_x(int arg) { x = arg; }
    public void set_y(int arg) { y = arg; }
    public bool get_startsVisible() { return startsVisible; }

}
