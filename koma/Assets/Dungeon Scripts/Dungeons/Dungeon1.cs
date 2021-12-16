using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon1 : Dungeon
{
    // a specific dungeon.

    void Awake()
    {
        totalTiles = 29;
    }
    [SerializeField] private UsableTile treasureTile; //used to acquire a bit of treasure
    [SerializeField] private UsableTile fontOfHp; //restores some hp to all party units. starts visible.
    [SerializeField] private UsableTile fontOfStamina; //restores some stamina. starts visible.
    [SerializeField] private UsableTile fontOnMp; //restores some stamina. starts visible.
    [SerializeField] private UsableTile fontOfFullRestore; //restores some stamina. starts visible.
    [SerializeField] private HomeTile homeTile; //can spawn here. starts visible.
    [SerializeField] private HomeTile homeTileWithExit; //can spawn and withdraw here. starts visible.
    [SerializeField] private HomeTile ClearTile; //is a home tile, but does not start explored. the first time you withdraw through this tile, dungeon is considered cleared.
    [SerializeField] private EventTile eventTile; //plays an event.
    [SerializeField] private Tile blankTile; //has nothing
    [SerializeField] private MobParty partyTemplate;

    //ENEMY POOL
    //0: forest wolv
    //1: sabaind
    //2: bow sabaind
    //3: agairy settler 
    //4: order sister
    //5: allowenn

    protected override int pick_front(int points)
    {
        return 0;
    }
    protected override int pick_back(int points)
    {
        return 0;
    }
    public override void retrieve_uniqueFormation(Enemy[] formation, int id)
    {
        //remember enemy layout is:
        // 0 1 2
        // 3 4 5

        Debug.Log("retrieve unique formation called, id = " + id);
        switch (id)
        {
            case 0: 
                formation[1] = enemyPool[3];
                formation[3] = enemyPool[3];
                formation[5] = enemyPool[3];
                break;
            case 1: 
                formation[2] = enemyPool[1];
                formation[3] = enemyPool[3];
                formation[4] = enemyPool[0];
                break;
            case 2: 
                formation[0] = enemyPool[1];
                formation[1] = enemyPool[0];
                formation[2] = enemyPool[1];
                formation[4] = enemyPool[0];
                break;
            case 3: 
                formation[1] = enemyPool[2];
                break;
        }
    }

    public override void spawn_mob_parties(List<MobParty> activeParties)
    {
        //FORMAT:
        //movement, vision, x, y, name, unique formation id, post battle event id.

        MobParty enc1 = Instantiate(partyTemplate, Vector2.zero, Quaternion.identity);
        enc1.fill(1, 1, 0, 3, generate_partyName(), 0, -1);
        activeParties.Add(enc1);

        MobParty enc2 = Instantiate(partyTemplate, Vector2.zero, Quaternion.identity);
        enc2.fill(1, 1, 3, 3, generate_partyName(), 1, -1);
        activeParties.Add(enc2);

        MobParty enc3 = Instantiate(partyTemplate, Vector2.zero, Quaternion.identity);
        enc3.fill(1, 1, 2, 6, generate_partyName(), 2, -1);
        activeParties.Add(enc3);

        MobParty clearBoss = Instantiate(partyTemplate, Vector2.zero, Quaternion.identity);
        clearBoss.fill(0, 0, 5, 6, generate_partyName(), 3, 0);
        activeParties.Add(clearBoss);

    }

    const int xDimension = 7;
    const int yDimension = 7;
    public override int specify_grid()
    {
        //create grid explicitly from specified tile types.
        //when the dungeon manager gets its hands on these, it'll instantiate a tile based on the type.

        //set tileUsedGrid. all false on new.
        if (tileUsedGrid == null)
        {
            tileUsedGrid = new bool[xDimension, yDimension];
            for (int i = 0; i < xDimension; i++)
            {
                for (int j = 0; j < yDimension; j++)
                {
                    tileUsedGrid[i, j] = false;
                }
            }
        }

        //resets to null on new game, but not within one game.
        if (explored_grid == null)
        {
            explored_grid = new Exploration[xDimension, yDimension];

            for (int i = 0; i < xDimension; i++)
            {
                for (int j = 0; j < yDimension; j++)
                {
                    //default
                    explored_grid[i, j] = Exploration.UNKNOWN;

                    //for testing
                    //explored_grid[i, j] = Exploration.EXPLORED;                    
                }
            }
            //set initial home tile to explored:
            explored_grid[0, 0] = Exploration.EXPLORED;

            //for fast passing
            explored_grid[4, 6] = Exploration.EXPLORED;

            //set exploredTiles equal to pre-explored tiles
            exploredTiles = 2;
        }

        dungeonGrid = new Tile[xDimension, yDimension]
        {
            { homeTile, eventTile, blankTile, blankTile, null, null, null },
            { null, null, null, blankTile, null, null, treasureTile },
            { null, null, null, blankTile, blankTile, blankTile, blankTile },
            { null, null, null, fontOfHp, null, null, blankTile },
            { null, null, null, null, null, null, homeTile },
            { null, null, null, null, null, null, eventTile },
            { null, null, null, null, null, null, ClearTile }
            //the last two tiles in the final array should have cave-type backgrounds. All the rest should be a mix of forest and plains.
        };

        //can add all kinds of checks in here or whatever you want, you know.
        return (xDimension * 100) + yDimension;
    }

    //Names and stuff
    private string[] syllableArray = new string[]
    {
        "nh", "te", "ryb", "ast", "gru", "spis", "klr", "mu", "ln", "cre",
        "ba", "rto", "nog", "apl", "gca", "bol", "pi", "qsd", "oka", "psx",
        "bv", "drf", "yii", "ntf", "o", "a", "gah", "e", "i", "y", "mat", "mmu",
        "muk", "zs", "dq"
    };
    private string[] suffixArray = new string[] //not currently being used in name generations
    {
        "band", "marauders", "mob", "posse", "hanger-ons", "gang", "choppers", "cohort", "followers", "sycophants",
        "lumpers", "thumpers", "breakers", "busters", "killers", "robbers", "wreckers", "mummers", "gutters", "stabbers",
        "nibblers", "eaters", "goons", "lovers", "clubbers", "winkers", "beaters", "stickers", "cutters", "canibblers",
        "stompers", "gougers", "fighters", "noncers", "cuckoos", "madders", "stealers", "pirates", "troupe", "screamers",
        "lickers", "lackeys", "grunts", "friends", "outlaws", "at-largers", "vagabonds", "raiders", "bandits", "screechers",
        "heavies"
    };
    public override string generate_partyName()
    {
        string name = "";
        int lastSyllableIndex = -1;

        int limit = Random.Range(3, 6);
        for (int i = 0; i < limit; i++)
        {
            int ran = Random.Range(0, syllableArray.Length);

            if (lastSyllableIndex == ran)
            {
                if (ran == 0)
                {
                    ran = 1;
                }
                else
                {
                    ran--;
                }
            }

            name += syllableArray[ran];
            lastSyllableIndex = ran;
        }

        //don't consolt suffix array for now. the names are super long.
        //int ran2 = Random.Range(0, suffixArray.Length);
        //name += "'s " + suffixArray[ran2];

        //capitalize first letter
        char[] a = name.ToCharArray();
        a[0] = char.ToUpper(a[0]);

        return new string(a);
    }



}
