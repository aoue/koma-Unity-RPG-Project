using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon0 : Dungeon
{
    // a specific dungeon.

    [SerializeField] private UsableTile fontOfHp; //restores some hp to all party units. starts visible.
    [SerializeField] private UsableTile fontOfStamina; //restores some stamina. starts visible.
    [SerializeField] private HomeTile homeTile; //can spawn here. starts visible.
    [SerializeField] private HomeTile homeTileWithExit; //can spawn and withdraw here. starts visible.
    [SerializeField] private HomeTile ClearTile; //is a home tile, but does not start explored. the first time you withdraw through this tile, dungeon is considered cleared.
    [SerializeField] private EventTile eventTile; //plays an event.
    [SerializeField] private Tile blankTile; //has nothing
    [SerializeField] private MobParty partyTemplate;

    //ENEMY POOL
    //0: sentinel (melee unit)
    //1: sentry (ranged unit)
    //2: Mr A (boss, caster unit)

    protected override int pick_front(int points)
    {
        //assigns into formation[3, 4, 5]
        Debug.Log("dungeon0 calling pick_front() with points = " + points);
        //first, pick a formationID with points >= cost. first must be less/equal to 10.
        //and roll succeeded (higher is more likely to succeed). first must be 100.
        int[] formationCosts = new int[] { 10, 20, 30 };        
        int[] formationRolls = new int[] { 100, 80, 50 };
        
        //then, put the formation id into a switch.
        int id = -1;
        for (int i = formationCosts.Length - 1; i >= 0; i--)
        {
            //first, you have to be able to afford it.
            if (points < formationCosts[i]) continue;

            int roll = UnityEngine.Random.Range(0, 101);
            Debug.Log("at " + i + " with roll = " + roll);
            if ( roll - points <= formationRolls[i])
            {
                id = i;
                break;
            }
        }
        //fill formation.
        if (id == -1) return points;
        switch (id)
        {
            case 0:
                //Debug.Log("case 0");
                formation[3] = null;
                formation[4] = enemyPool[0];
                formation[5] = null;
                break;
            case 1:
                //Debug.Log("case 1");
                formation[3] = null;
                formation[4] = enemyPool[0];
                formation[5] = enemyPool[0];
                break;
            case 2:
                //Debug.Log("case 2");
                formation[3] = enemyPool[0];
                formation[4] = enemyPool[0];
                formation[5] = enemyPool[0];
                break;
        }       
        return points - formationCosts[id];
    }
    protected override int pick_back(int points)
    {
        Debug.Log("dungeon0 calling pick_back() with points = " + points);

        //assigns into formation[0, 1, 2]
        int[] formationCosts = new int[] { 10, 20, 30, 30};
        int[] formationRolls = new int[] { 100, 30, 25, 30 };

        //then, put the formation id into a switch.
        int id = -1;
        for (int i = formationCosts.Length - 1; i >= 0; i--)
        {
            //first, you have to be able to afford it.
            if (points < formationCosts[i]) continue;

            int roll = UnityEngine.Random.Range(0, 101);
            Debug.Log("at " + i + " with roll = " + roll);

            if (roll - points <= formationRolls[i])
            {
                id = i;
                break;
            }
        }
        //fill formation.
        if (id == -1) return points;
        switch (id)
        {
            case 0:
                //Debug.Log("case 0");
                formation[0] = enemyPool[1];
                formation[1] = null;
                formation[2] = null;
                break;
            case 1:
                //Debug.Log("case 1");
                formation[0] = null;
                formation[1] = enemyPool[1];
                formation[2] = enemyPool[1];
                break;
            case 2:
                //Debug.Log("case 2");
                formation[0] = enemyPool[1];
                formation[1] = enemyPool[1];
                formation[2] = enemyPool[1];
                break;
            case 3:
                formation[0] = null;
                formation[1] = enemyPool[2];
                formation[2] = null;
                break;
        }
        return points - formationCosts[id];
    }
    public override Enemy[] retrieve_uniqueFormation(int id)
    {
        //remember enemy layout is:
        // 0 1 2
        // 3 4 5
        if (formation == null) formation = new Enemy[6];
        for (int i = 0; i < formation.Length; i++)
        {
            formation[i] = null;
        }

        switch (id)
        {
            case 0: //first fight
                formation[3] = enemyPool[0];
                formation[5] = enemyPool[0];
                break;
            case 1: //first ambush wave
                formation[1] = enemyPool[1];
                formation[3] = enemyPool[0];
                formation[4] = enemyPool[0];
                formation[5] = enemyPool[0];
                break;
            case 2: //second ambush wave
                formation[4] = enemyPool[0];
                formation[0] = enemyPool[1];
                formation[1] = enemyPool[1];
                formation[2] = enemyPool[1];
                break;
            case 3: //mr a boss fight
                formation[1] = enemyPool[2];
                formation[3] = enemyPool[0];
                formation[5] = enemyPool[0];
                break;
        }
        return formation;
    }

    public override void spawn_mob_parties(List<MobParty> activeParties)
    {
        //must instantiate the parties from partyTemplate, then customize them.
        //first: instantiate all the bosses (unless the boss is dead)
        //       this type doesn't go far.
        //next: instantiate wandering monsters based on zoning in the map. e.g. 1 monsters in the zone, 1 in this zone, but the actual position can vary.
        //      this type is content to just wander.
        //finally: instantiate monsters near dens.
        //         this type is the most aggressive, and they head for known villages, etc.
        //FORMAT:
        //movement, vision, x, y, name, unique formation id, post battle event id.

        MobParty enc1 = Instantiate(partyTemplate, Vector2.zero, Quaternion.identity);
        enc1.fill(1, 1, 4, 1, "Patrol 1", 0, -1);
        activeParties.Add(enc1);

        MobParty enc2 = Instantiate(partyTemplate, Vector2.zero, Quaternion.identity);
        enc2.fill(1, 1, 3, 4, "Patrol 2", 1, -1);
        activeParties.Add(enc2);

        MobParty enc3 = Instantiate(partyTemplate, Vector2.zero, Quaternion.identity);
        enc3.fill(1, 1, 5, 4, "Patrol 3", 2, -1);
        activeParties.Add(enc3);

        MobParty clearBoss = Instantiate(partyTemplate, Vector2.zero, Quaternion.identity);
        clearBoss.fill(0, 0, 7, 4, "Mr A", 3, 0);
        activeParties.Add(clearBoss);
    }

    public override int specify_grid()
    {
        //create grid explicitly from specified tile types.
        //when the dungeon manager gets its hands on these, it'll instantiate a tile based on the type.

        //set tileUsedGrid. all false on new.
        if ( tileUsedGrid == null)
        {
            tileUsedGrid = new bool[9, 5];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    tileUsedGrid[i, j] = false;
                }
            }    
        }

        //resets to null on new game, but not within one game.
        if (explored_grid == null)
        {
            explored_grid = new Exploration[9, 5];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    //explored_grid[i, j] = Exploration.EXPLORED;
                    explored_grid[i, j] = Exploration.UNKNOWN;
                }
            }
            //set initial home tile to explored:
            explored_grid[1, 0] = Exploration.EXPLORED;
        }

        dungeonGrid = new Tile[9, 5]
        {
            { null, blankTile, null, null, null },
            { homeTileWithExit, blankTile, null, null, null },
            { null, blankTile, null, null, null },
            { null, blankTile, null, null, blankTile },
            { null, blankTile, blankTile, blankTile, blankTile },
            { null, blankTile, null, null, blankTile },
            { null, null, null, null, blankTile },
            { null, null, null, null, blankTile },
            { null, null, null, null, ClearTile }
        };

        //can add all kinds of checks in here or whatever you want, you know.
        return 905;
    }

    //Names and stuff
    private string[] syllableArray = new string[]
    {
        "nh", "te", "ryb", "ast", "gru", "spis", "klr", "mu", "ln", "cre",
        "ba", "rto", "nog", "apl", "gca", "bol", "pi", "qsd", "oka", "psx",
        "bv", "drf", "yii", "ntf", "o", "a", "gah", "e", "i", "y", "mat", "mmu",
        "muk", "zs", "dq"
    };
    private string[] suffixArray = new string[]
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
