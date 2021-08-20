using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobParty : MonoBehaviour
{
    //party of monster found in the dungeon.

    //is created by a den when spawned with a few characteristics, like speed, danger, etc.
    //when fought, they go to dungeonManager with a specific fight key. it can either be a unique fight (like for a wandering monster), or randomly load up a formation.
    //starts untriggered and just wait in place.

    //when triggered, they pick some spot on the dungeon and walk there.
    //if they find a neutral party, they'll chase them.
    //if they find the player's party, they'll chase them.


    //it's spawned by the dungeon, and all its values are set there.
    //it's added to dungeonManager's active party list, and so 

    [SerializeField] private TextMesh tex;
    [SerializeField] private MeshRenderer texRendy;
    private int uniqueParty; //if -1 then generate a random party. else, get a unique party using this int as an identifier.
    private int eventID; //-1 means no event.
    private string partyName; //their name. randomly generated for them by the dungeon. Or, sometimes, a unique name for a specific party.
    private int movement; //in tiles. 0 for immobile.
    private int vision; //in tiles. 0 for blind.
    private int x;
    private int y;
    
    [SerializeField] private SpriteRenderer rendy;
    private List<Tuple> destination; //where the mob party is headed. each step along the way, in order.


    public void fill(int mv, int vs, int daX, int daY, string nom, int uniquePartyID, int evID)
    {
        //used to fill it when it's created.
        movement = mv;
        vision = vs;
        x = daX;
        y = daY;
        set_visibility(false);
        partyName = nom;       
        tex.text = partyName;
        uniqueParty = uniquePartyID;
        eventID = evID;
    }
    public void set_visibility(bool val)
    {
        texRendy.enabled = val;
        rendy.enabled = val;
    }
    public void did_event()
    {
        eventID = -1;
    }

    //MOVEMENT
    public List<Tuple> get_moves()
    {
        //returns the partial move that the unit will perform this turn.

        if (destination == null) return new List<Tuple>();

        List<Tuple> partialMove = new List<Tuple>();
        for (int i = 0; i < movement; i++)
        {
            if (destination.Count == 0) break; //safeguard

            partialMove.Add(destination[0]);
            destination.RemoveAt(0);
        }

        if (partialMove.Count > 0)
        {
            x = partialMove[partialMove.Count - 1].x;
            y = partialMove[partialMove.Count - 1].y;
        }
        
        return partialMove;
    }
    public void set_destination(List<Tuple> path)
    {
        destination = path;
    }
    public void clear_destination()
    {
        if (destination == null) return;
        destination.Clear();
    }

    //SETTERS
    public void set_x(int h) { x = h; }
    public void set_y(int h) { y = h; }
    public void set_movement(int h) { movement = h; }

    //GETTERS
    public int get_vision() { return vision; }
    public int get_eventID() { return eventID; }
    public int get_uniqueParty() { return uniqueParty; }
    public List<Tuple> get_destination() { return destination; }
    public string get_partyName() { return partyName; }
    public int get_movement() { return movement; }
    public int get_x() { return x; }
    public int get_y() { return y; }
}
