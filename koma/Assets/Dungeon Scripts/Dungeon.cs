using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum Exploration { UNKNOWN, FOG, EXPLORED }
public class Dungeon : MonoBehaviour
{
    //a dungeon object. it holds all the information needed to turn the dungeonScene 
    //into a particular dungeon.

    //held in overworld by a dungeonHolder object.

    //holds:
    // -threat and stamina
    // -tile information. (jography; whether each tile is null, has an event, what type of event, etc)
    // -enemy formation logic.   

    //enemy formations are generated from here, taking the dungeon's threat into consideration.

    [SerializeField] private AudioClip bgTheme;
    [SerializeField] private AudioClip combatTheme;

    [SerializeField] private string dungeonTitle;
    [SerializeField] private int dungeonId;
    [SerializeField] private int threatDecay; //per day
    [SerializeField] private int unitLimit; //how many units can enter this dungeon in the party.

    public int totalTiles { get; set; } //total number of non-null tiles.
    public int exploredTiles { get; set; } //total number of explored tiles.
    public int expeditionCounter { get; set; } //total number of expeditons into dungeon.
    public int threat { get; set; } //current threat level. higher means more dangerous/leveled mobs and better loot.

    //list of all enemies that can be encountered
    [SerializeField] protected Enemy[] enemyPool; //arranged from lowest to highest. enemyPool[0] must have deployChance = 100.

    public bool cleared { get; set; } //true means the dungeon has been cleared (and can not be cleared again.)
    public Tile[,] dungeonGrid { get; set; } //2d aray of tiles. will be specified idiviudually in each child script.
    public Exploration[,] explored_grid { get; set; } //keeps track of whether a tile has been explored.
    public bool[,] tileUsedGrid { get; set; } //keeps track of whether a usable tile has been used before.

    //pseudo dictionary - will be turned into one at Start().
    [SerializeField] private Event[] mobEventLibrary; //retrieved and played after fighting a certain mob. regular event.
    [SerializeField] private Event[] tileEventLibrary; //each dungeon has its own way of converting a tile's x, y to an id for an event.
    [SerializeField] private int[] eventLibraryKeys;
    private Dictionary<int, Event> eventDict;
    
    //getters
    public AudioClip get_bgTheme() { return bgTheme; }
    public AudioClip get_combatTheme() { return combatTheme; }
    public string get_dungeonTitle() { return dungeonTitle; }
    public int get_dungeonId() { return dungeonId; }
    public int get_threatDecay() { return threatDecay; }
    public int get_unitLimit() { return unitLimit; }

    public Event get_eventDict_event(int key) { return eventDict[key]; }

    void Start()
    {
        //compile eventLibrary and eventLibraryKeys into a single dictionary.
        eventDict = new Dictionary<int, Event>();
        for (int i = 0; i < eventLibraryKeys.Length; i++)
        {
            eventDict.Add(eventLibraryKeys[i], tileEventLibrary[i]);
        }
        Debug.Log("dungeon start() called");
    }


    //FORMATION GENERATION
    public Enemy[][] generate_waves(int threat, int[] mobPartyInfo)
    {
        //creates multiple waves in a battle at once.
        Enemy[][] allWaves = new Enemy[mobPartyInfo.Length][]; //to return
        for (int i = 0; i < allWaves.Length; i++)
        {
            allWaves[i] = new Enemy[6];
        }

        for (int i = 0; i < allWaves.Length; i++)
        {
            Debug.Log(i);
            if (mobPartyInfo[i] == -1) //create random formation
            {
                allWaves[i] = generate_formation(threat);
            }
            else //retrieve unique formation
            {
                retrieve_uniqueFormation(allWaves[i], mobPartyInfo[i]);
            }
        }
        return allWaves;
    }
    public Enemy[] generate_formation(int threat)
    {
        //this works like this:
        // -convert threat into points
        // -randomize the order of rows you're going to purchase.
        // -buy a first row, randomly choose between all those you can afford, weighted more heavily towards the expensive end.
        // -buy the most expensive opposite row that you can afford
        //combine the two and return a completed list.
        return null;

        int points = threat + 10;
        bool frontFirst = true;
        if (UnityEngine.Random.Range(0, 2) == 1) frontFirst = false;

        if (frontFirst)
        {
            //pick front            
            points = pick_front(points);
        }
        else
        {
            //pick back
            points = pick_back(points);
        }


        if (frontFirst)
        {
            //pick back
            points = pick_back(points);
        }
        else
        {
            //pick front
            points = pick_front(points);
        }
    }
    protected virtual int pick_front(int val)
    {
        return val;
    }
    protected virtual int pick_back(int val)
    {
        return val;
    }
    public virtual void retrieve_uniqueFormation(Enemy[] formation, int id)
    {
        
    }
    

    //VIRTUALS
    public virtual int specify_grid()
    {
        //nothing. only exists to be overriden
        return -1;
    }
    
    public virtual void spawn_mob_parties(List<MobParty> activeParties)
    {
        //virtual, exists only to be overriden.
        //used to place mob parties in a dungeon. method can vary.
    }
    public virtual string generate_partyName()
    {
        //randomly combines short strings into a longer name for the leader of the party.
        //then add something like: 's marauders to the end.
        return "default string";
    }


    //SENSIBLE THINGS
    public Event retrieve_mobEvent(int index)
    {
        return mobEventLibrary[index];
    }
    public Event retrieve_TileEvent(int x, int y)
    {
        int key = (x * 100) + y;
        Debug.Log("trying to retrieve tile event, key = " + key);
        return get_eventDict_event(key);
    }
    public Tile get_tile(int x, int y)
    {
        return dungeonGrid[x, y];
    }

    public Exploration isTileExplored(int x, int y)
    {
        //returns whether a single tile is explored.
        //Debug.Log("returning explored tile at coordinates: " + explored_grid[x, y] + "| x: " + x + ", y: " + y);
        return explored_grid[x, y];
    }
    public void set_tileUnknown(int x, int y)
    {
        //sets a tile to unknown.
        explored_grid[x, y] = Exploration.UNKNOWN;
    }

    public void set_tileFoggy(int x, int y)
    {
        //sets a tile to fog.
        explored_grid[x, y] = Exploration.FOG;
    }

    public void set_tileExplored(int x, int y)
    {
        //sets a tile to explored.
        explored_grid[x, y] = Exploration.EXPLORED;
    }

    

}
