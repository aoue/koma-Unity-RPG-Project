using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Direction { NORTH, EAST, SOUTH, WEST }
public enum DungeonState { OOC, WAIT, COMBAT, SETUP, EVENT }
public enum LeavingState { LOSS, WITHDRAW, CLEAR }
public class DungeonManager : MonoBehaviour
{
    //_instance baby.
    public static DungeonManager _instance;

    //SETTINGS
    private float spacing = 1.73f; //space between tiles. currently set at 1.73, as tiles are 173x173
    private float transitionDuration = 2f; //the time movement/rotation takes. higher is faster.
    private float hpPercentHealedPerStamina = 10; //the % of all unit's maxhp healed with 1 stamina in dungeon healer. this can be changed with difficulty setting or by buying upgrades or something.
    private float rotationDuration = 0.5f;
    private float hpPercentHealedPerNewTile = 1; //the % of a unit's maxhp healed when the party moves onto an unexplored tile.
    private float mpPercentHealedPerNewTile = 1; //the % of a unit's maxmp healed when the party moves onto an unexplored tile.

    [SerializeField] private Cart cart;
    [SerializeField] private Sprite[] affOrbSprites;

    //is the boss for the dungeon scene.
    public static bool canSwapUnits;
    private bool canDragScreen;
    public static Dungeon heldDun; //set by prepdungeonmanager
    public static Unit[] party; //set by prepdungeonmanager
    public static int stamina; //set by prepdungeonmanager

    public static DungeonState state; //help govern the managers.
    private Direction facing; //directio the party is facing. N-E-S-W

    private int threat; //read from dungeon

    //the coordinates of the party in the dungeon.
    private int xParty;
    private int yParty;

    //the size of the dungeon.
    public int obtainedXP { get; set; }
    private PathfinderManager pathManager;
    private int vision;
    private int xDungeon;
    private int yDungeon;
    private int savedHealStamCost;

    [SerializeField] private DungeonNotifier dungeonNotifier; //for some post combat things.
    [SerializeField] private SoundManager SM; //handles music and sounds, baby.
    [SerializeField] private FadeManager fader; //handles fading, baby.
    [SerializeField] private EncounterManager encManager; //managers events when we bump a new tile
    [SerializeField] private CombatManager combatManager; //combat manager. to fight you know.
    [SerializeField] private EventManager evManager; //the dungeon's event manager. doesn't have tooltipmanager, otherwise the same.
    [SerializeField] private DungeonUnitBox[] partyBoxes;
    [SerializeField] private Text staminaText;
    [SerializeField] private Text coordText;
    [SerializeField] private Camera cammy;
    [SerializeField] private GameObject partyFlag;
    [SerializeField] private Button[] movementArrows; //array of 3. 0:left, 1:forward, 1:right.
    [SerializeField] private Button useTileButton;
    [SerializeField] private Button useHealButton;
    [SerializeField] private Button withdrawButton;
    [SerializeField] private Button recenterCameraButton;
    [SerializeField] private GameObject compassGO; //rotates.
    [SerializeField] private CanvasGroup unitBoxCanvasGroup;
    [SerializeField] private Canvas panningDetectionCanvas; //note: starts disabled.
    [SerializeField] private Sprite unexploredSprite; //to combat the prefabs being changed during tests
    [SerializeField] private PlayByPlay pbp; //dungeon play by play text: shows tile type name, stam drain, and vision.
    [SerializeField] private LossManager loser; //handles when we lose a battle.
    [SerializeField] private DungeonHealer healer; //handles the dungeon healing interface

    private List<MobParty> lambs; //to the slaughter. the group of enemies the player is going to have to fight.
    private List<NeutralParty> neutralParties;
    private List<MobParty> activeParties; //a list of all the other active parties in the dungeon. they all take turns.
    //private List<Den> activeDens; //are dens active? do they constantly send out new parties? well, why not.

    //exploration assistants
    private SpriteRenderer[,] rendyGrid;
    private List<Tuple> visibleTiles; //maintained to help with visibility checks, so we don't have to deal with the whole grid each time.

    void Awake()
    {
        //fade from black, if you please.
        fader.fade_from_black();

        if (_instance != null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this;

        xParty = 0;
        yParty = 0;

        threat = Mathf.Max(heldDun.threat, 1);

        neutralParties = new List<NeutralParty>();
        lambs = new List<MobParty>();
        activeParties = new List<MobParty>();
        visibleTiles = new List<Tuple>();
        
        //read and display information
        threat = heldDun.threat;
        staminaText.text = "Stamina: " + stamina;
        
        //party setup
        fill_party();

        //dungeon setup
        fill_dungeon();
        allow_pick_starting_position();
        SM.play_background_music(heldDun.get_bgTheme());
    }

    //SETUP
    public static DungeonManager get_dMan() { return _instance; }
    
    public void allow_pick_starting_position()
    {
        //hide movement controls.
        disable_movement_arrows();
        //when entering a dungeon, the player can pick a tile they want to land on.
        //not all tiles are valid, only a HomeTile type is valid.
        //highlights all valid tiles. 
        //deployment phase ends when the player clicks the tile they want; then we go to regular dungeon moving.

        canDragScreen = true;
        state = DungeonState.SETUP;
    }
    public void picked_home_tile(int x, int y)
    {
        //called when the player clicks on a home tile. it spawns them there.
        xParty = x;
        yParty = y;
        update_coord_text();

        facing = Direction.NORTH;
        state = DungeonState.OOC;


        place_partyFlag(false);
        place_camera(false);
        partyFlag.SetActive(true);
        
        enable_movement_arrows();
        update_forward_arrow();
        update_map();
        update_pbp(true);
        update_ai_parties();
        panningDetectionCanvas.gameObject.SetActive(true);
    }
    void fill_party()
    {
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] == null)
            {
                partyBoxes[i].fill_empty();
            }
            else
            {
                if (party[i].status == null) party[i].create_status();
                partyBoxes[i].fill_unit(party[i], affOrbSprites[party[i].get_affinity()], false);
            }
        }

    }
    void fill_dungeon()
    {
        //responsible for making the dungeon's tiles.
        //iterates over the dungeon's dungeonGrid member
        //and instantiates tiles based on it.
        //keeps that grid in mind; it is our real guide.
        //the ui is just for the player; the scripts don't care.

        int toSplit = heldDun.specify_grid(); //makes the dungeon build it's grid.
        xDungeon = toSplit / 100;
        yDungeon = toSplit % 100;

        pathManager = new PathfinderManager();
        pathManager.setup(xDungeon, yDungeon);

        rendyGrid = new SpriteRenderer[xDungeon, yDungeon];

        //now show the grid.
        //for now, show all tiles. (rules can come later)
        for (int x = 0; x < xDungeon; x++)
        {
            for (int y = 0; y < yDungeon; y++)
            {
                if (heldDun.dungeonGrid[x, y] != null)
                {
                    pathManager.tileValidAt(x, y);

                    //Debug.Log("creating tile at " + x + ", " + y);
                    Vector2 pos = new Vector2(x, y) * spacing;

                    //SPRITE ATTEMPT
                    Tile lala = Instantiate(heldDun.dungeonGrid[x, y], pos, Quaternion.identity);
                    lala.set_x(x);
                    lala.set_y(y);
                    rendyGrid[x, y] = lala.get_rendy();

                    //if tile is supposed to start explored and hasn't been barged onto.
                    
                    if (heldDun.isTileExplored(x, y) != Exploration.EXPLORED && lala.get_startsVisible() == true)
                    {
                        heldDun.set_tileFoggy(x, y);
                    }

                    //if explored, give it its image.
                    if (heldDun.isTileExplored(x, y) == Exploration.EXPLORED)
                    {
                        //show tile as it is.
                        lala.set_tile_image();
                        lala.set_valid();

                        //and we turn sparkle on too.
                        if ( lala is UsableTile && heldDun.tileUsedGrid[x, y] == false)
                        {
                            lala.turn_on_sparkle();
                        }
                        else if ( lala is HomeTile && lala.isValidExit() == true)
                        {
                            lala.turn_on_sparkle();
                        }

                    }
                    else if (heldDun.isTileExplored(x, y) == Exploration.FOG) 
                    {
                        //show tile as fog.
                        lala.set_sprite(unexploredSprite);
                    }
                    else //i.e. tile == Exploration.UNKNOWN
                    {
                        //hide tile. completely invisible.
                        heldDun.set_tileUnknown(x, y);
                        rendyGrid[x, y].enabled = false;
                        lala.hide_textMesh();
                    }
                }
                else
                {
                    pathManager.tileInvalidAt(x, y);
                }
            }
        }

        //now, spawn monsters.
        heldDun.spawn_mob_parties(activeParties);
        
    }

    //SLIDING
    IEnumerator slideListener(bool isRotation)
    {
        //this is silly. the smarter thing is just to disable ui for the (slighly longer than) length
        //of time it takes the sprites to finish moving.

        //listens for when all slide_sprite calls have been finished.
        //disables and re-enables movement.
        disable_movement_arrows();
        canSwapUnits = false;

        if (isRotation)
            yield return new WaitForSeconds(rotationDuration);
        else
            yield return new WaitForSeconds(1.78f / transitionDuration);
        
        if ( state != DungeonState.EVENT && state != DungeonState.COMBAT)
        {
            //re enable movement controls
            enable_movement_arrows();
            update_forward_arrow();
            canSwapUnits = true;
        }

        if (!isRotation) SM.stop_soundPlayer();
    }
    IEnumerator slide_ai(GameObject toMove, List<Tuple> path)
    {
        //sprite moves in a series of straight lines.
        //spends time / movement perhaps each move part.
        foreach(Tuple tup in path)
        {
            Vector3 toHere = new Vector3(tup.x * spacing, tup.y * spacing, toMove.transform.position.z);

            bool keepGoing = true;
            while (keepGoing == true)
            {
                toMove.transform.position = Vector3.MoveTowards(toMove.transform.position, toHere, Time.deltaTime * path.Count * transitionDuration);
                //check if we're there, in which case stop coroutine
                if (Vector3.Distance(toMove.transform.position, toHere) < 0.001f)
                {
                    keepGoing = false;
                }
                yield return null;
            }
        }        
    }
    IEnumerator slide_sprite(GameObject toMove, Vector3 toHere)
    {
        //used to move our sprites in a straight line.
        bool keepGoing = true;
        while (keepGoing == true)
        {
            toMove.transform.position = Vector3.MoveTowards(toMove.transform.position, toHere, Time.deltaTime * transitionDuration);
            //check if we're there, in which case stop coroutine
            if (Vector3.Distance(toMove.transform.position, toHere) < 0.001f)
            {
                keepGoing = false;
            }
            yield return null;
        }


    }
    IEnumerator rotate_sprite(GameObject toRotate, Vector3 toHere, int direction, bool makeExact = true)
    {
        //used to rotate camera and sprites.
        //direct: -1 means left, 1 means right

        float finalZ = toRotate.transform.eulerAngles.z + toHere.z;

        var fromAngle = toRotate.transform.rotation;
        var toAngle = Quaternion.Euler(toRotate.transform.eulerAngles + toHere);

        for ( var t = 0f; t < 1; t += Time.deltaTime / rotationDuration)
        {
            toRotate.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        //set exact
        toRotate.transform.eulerAngles = new Vector3(0, 0, finalZ);
    }
    void rotate_tileEffects(Vector3 toHere, int direction, bool makeExact = true)
    {
        //this coroutine, called whenever we need to do rotating, rotates all the little effects on tiles, like the stam cost textmesh or sparkler effects.
        //it doesn't rotate the tiles themselves, though.

        //can reach all tiles through rendy grid.
        for(int x = 0; x < xDungeon; x++)
        {
            for (int y = 0; y < yDungeon; y++)
            {
                if (rendyGrid[x, y] != null)
                {
                    //then grab the tile gameobject and rotate all its children gameobjects!
                    foreach (Transform child in rendyGrid[x, y].transform)
                    {
                        StartCoroutine(rotate_sprite(child.gameObject, toHere, direction, makeExact));
                    }
                }

            }
        }

    }
    void place_aiParty(GameObject toPlace, int x, int y, bool slide = true)
    {
        Vector3 pos = new Vector3(x * spacing, y * spacing, toPlace.transform.position.z);

        if (slide == true)
        {
            StartCoroutine(slide_ai(toPlace, toPlace.GetComponent<MobParty>().get_moves()));
        }
        else
        {
            toPlace.transform.position = pos;
        }     
    }
    void place_partyFlag(bool slide = true)
    {
        //places party flag based on xParty and yParty.
        Vector3 pos = new Vector3(xParty * spacing, yParty * spacing, partyFlag.transform.position.z);
        
        if (slide == true)
        {
            StartCoroutine(slide_sprite(partyFlag, pos));
        }
        else
        {
            partyFlag.transform.position = pos;
        }

    }
    void place_camera(bool slide = true)
    {
        //camera should be trailing, so that the party flag is in the screen center
        float xOffset = 0f;
        float yOffset = 0f;
        switch (facing)
        {
            case Direction.NORTH:
                yOffset = -2f;
                break;
            case Direction.SOUTH:
                yOffset = 2f;
                break;
            case Direction.EAST:
                xOffset = -2f;
                break;
            case Direction.WEST:
                xOffset = 2f;
                break;
        }
        Vector3 pos = new Vector3((xParty * spacing) + xOffset, (yParty * spacing) + yOffset, -10f);
        if (slide == true)
        {           
            StartCoroutine(slide_sprite(cammy.gameObject, pos));
        }
        else
        {
            cammy.transform.position = pos;
        }
    }

    //ENCOUNTERS
    void show_loss_menu()
    {
        //on loss, show menu with 2 options:
        // -retry
        // -withdraw
        loser.show();
    }
    public void return_control(int battleXP, bool wasVictory = true, Unit[] pl = null)
    {
        //called after an event or a battle. returns control to dungeon manager.
        //and acts appropriately.
        switch (state)
        {
            case DungeonState.WAIT:
                //help us manage post-battle stuff.
                combatManager.close();
                //now, check if there is a post combat event.
                //if there is, then pass control over there.
                //note: there could be multiple events, so queue them all up.
                //the mob's event id is the same as its unique party id.

                for (int i = 0; i < party.Length; i++)
                {
                    if (party[i] != null && party[i].get_ooa() == true)
                    {
                        party[i].set_hp(1);
                    }
                }

                //play event. we'll play all events from lambs one at a time.
                foreach (MobParty mp in lambs)
                {
                    if ( mp.get_eventID() != -1) //if mob has event
                    {
                        //play event.
                        StartCoroutine(healthy_pause(2.0f, false));
                        evManager.begin_event(heldDun.retrieve_mobEvent(mp.get_eventID()));
                        mp.did_event(); //mark so we don't play this mp's event again.                        
                        return;
                    }
                }
                //when no more events to play, back to dungeon boii.
                foreach(MobParty mp in lambs)
                {
                    activeParties.Remove(mp);
                    Destroy(mp.gameObject);
                }
                lambs.Clear();
                adjust_ui_after_event();               
                break;
            case DungeonState.COMBAT:
                //did we win? -> destroy lambs. enable ui again. can proceed.
                if (wasVictory)
                {
                    state = DungeonState.WAIT;                                   
                    for (int i = 0; i < party.Length; i++)
                    {
                        party[i] = pl[i];
                    }
                    int threatInc = 0;
                    for(int i = 0; i < lambs.Count; i++)
                    {
                        threat += UnityEngine.Random.Range(4, 10);
                    }             
                    obtainedXP += battleXP;

                    //notifier
                    string noteString = "Obtained: " + battleXP + " Exp." 
                        + "\nThreat rises to " + (threat);                    

                    dungeonNotifier.show_note("Victory!", noteString, "bottom text");
                
                    //return

                    //the function will then be called again on notifier dismiss and fall into 
                    //the DungeonState.WAIT path.
                }
                else
                {
                    combatManager.close();   
                    show_loss_menu();     
                }

                break;
            case DungeonState.EVENT:
                //Debug.Log("returning after dungeon event");
                adjust_ui_after_event();
                move_ai_parties();
                break;
        }
        fill_party();
        SM.play_background_music(heldDun.get_bgTheme());
    }
    IEnumerator healthy_pause(float duration, bool doBattleAfter, Enemy[][] waves = null)
    {
        yield return new WaitForSeconds(duration);
        if (doBattleAfter)
        {
            combatManager.load_battle(party, waves, threat);
        }
        else
        {
            unitBoxCanvasGroup.alpha = 0f;
        }
    }
    public void go_fight(bool isRetry = false)
    {
        //passes control to fightManager, who is gonna pass it back to us later.
        //combatManager.begin_battle();

        //so, we have lambs, a list of mobParties.
        //do this:
        // -create a combat wave from each one. (a list of enemies. an enemy party)
        // -go to combat manager and fight each wave there.
        //when done (win or lose), the combat manager will pass control back and we'll do our stuff here.
        Enemy[][] waves;
        if (isRetry == true)
        {
            int[] resetHp = loser.get_prebattle_partyFill();
            for (int i = 0; i < resetHp.Length; i++)
            {
                if (party[i] != null)
                    party[i].set_hp(resetHp[i]);
            }
            waves = loser.get_preWaves();
        }
        else
        {
            waves = new Enemy[lambs.Count][];
            for (int i = 0; i < lambs.Count; i++)
            {
                //generate one wave from each party.
                if (lambs[i].get_uniqueParty() == -1)
                {
                    waves[i] = heldDun.generate_formation(threat);

                }
                else
                {
                    waves[i] = heldDun.retrieve_uniqueFormation(lambs[i].get_uniqueParty());
                }
            }
            loser.prebattle_partyFill(party);
        }
        
        fader.fade_to_black();
        StartCoroutine(healthy_pause(2.0f, true, waves));
    }
    void go_tile_event()
    {
        //passes control to eventManager, who is gonna pass it back to us later. 
        StartCoroutine(healthy_pause(2.0f, false));
        evManager.begin_event(heldDun.retrieve_TileEvent(xParty, yParty));
    }
    void update_map()
    {
        //updates the skeleton of the map, setting any unexplored tiles the
        //player has vision on to state exploration->fog.

        vision = heldDun.get_tile(xParty, yParty).get_vision();

        //do stuff to all tiles in the visible list
        //go through back to front, because we may remove from list.
        for (int i = visibleTiles.Count - 1; i >= 0; i--)
        {
            //check if it's still in range: visibleTiles[i]
            float hyp = Mathf.Sqrt(Mathf.Pow(visibleTiles[i].x - xParty, 2) + Mathf.Pow(visibleTiles[i].y - yParty, 2));
            if (hyp > vision) //i.e. if out of vision.
            {
                //then, set its alpha back to 0.6f
                Color temp = rendyGrid[visibleTiles[i].x, visibleTiles[i].y].color;
                temp.a = 0.6f;
                rendyGrid[visibleTiles[i].x, visibleTiles[i].y].color = temp;                
                visibleTiles.RemoveAt(i);
            }
        }


        //vision + map borders.
        int left = Mathf.Max(0, xParty - vision);
        int right = Mathf.Min(xDungeon - 1, xParty + vision);
        int bottom = Mathf.Max(0, yParty - vision);
        int top = Mathf.Min(yDungeon - 1, yParty + vision);

        for (int x = left; x < right + 1; x++)
        {
            for (int y = bottom; y < top + 1; y++)
            {
                if (heldDun.get_tile(x,y) != null)
                {
                    Exploration tileState = heldDun.isTileExplored(x, y);
                    
                    float hyp = Mathf.Sqrt(Mathf.Pow(x - xParty, 2) + Mathf.Pow(y - yParty, 2));
                    if (hyp <= vision)
                    {

                        visibleTiles.Add(new Tuple(x, y));
                        if (tileState == Exploration.UNKNOWN) //if the tile is unknown and within our vision, then set it to foggy.
                        {
                            heldDun.set_tileFoggy(x, y);
                            rendyGrid[x, y].enabled = true;
                            rendyGrid[x, y].sprite = unexploredSprite;
                            rendyGrid[x, y].gameObject.GetComponent<Tile>().show_textMesh();
                        }
                            
                        //set tile alpha to full.
                        if (rendyGrid[x, y].color.a != 1f)
                        {
                            Color tmp = rendyGrid[x, y].color;
                            tmp.a = 1f;
                            rendyGrid[x, y].color = tmp;
                        }
                    }
                }            
            }
        }      
    }
    void update_ai_parties()
    {
        //updates whether each ai party should have its renderer on or off.
        //updated on enter_new_tile

        foreach (MobParty mp in activeParties)
        {
            //if the tile it's on is unknown, definitely off. i.e. do nothing in that case.
            if ( heldDun.isTileExplored(mp.get_x(), mp.get_y()) != Exploration.UNKNOWN )
            {
                //next, if mob party within vision range, render them.
                float hyp = Mathf.Sqrt(Mathf.Pow(mp.get_x() - xParty, 2) + Mathf.Pow(mp.get_y() - yParty, 2));
                //Debug.Log("check mob visibility. Hyp = " + hyp + " | vision = " + vision);
                if (hyp <= vision)
                {
                    //Debug.Log("mobparty vision Approved. Showing at x" + mp.get_x() + ", y" + mp.get_y());
                    place_aiParty(mp.gameObject, mp.get_x(), mp.get_y(), false);
                    
                    mp.set_visibility(true);
                }
                else
                {
                    mp.set_visibility(false);
                }
            }
        }

    }
    void enter_new_tile()
    {
        //called everytime the player enters a tile. (by party_advance() )
        //explores the tile and plays the tiles event, if it has one.
        //tell enemymobs to move
        //check for collisions too.
                
        update_map();
        update_ai_parties();
        update_pbp();
        state = DungeonState.OOC;

        //soundmanager plays walking sound
        SM.play_walkingSound();

        //if the tile is a home tile that is also an exit, set withdraw button to true.
        if (heldDun.get_tile(xParty, yParty) is HomeTile )
        {
            if (heldDun.get_tile(xParty, yParty).isValidExit() == true)
                withdrawButton.interactable = true;
        }

        //if the tile was not explored, then do its event
        if ( heldDun.isTileExplored(xParty, yParty) != Exploration.EXPLORED )
        {
            //tile not explored; heal hp and mp of each party unit.
            unexplored_tile();

            //decrement stamina
            stamina = System.Math.Max(0, stamina - heldDun.get_tile(xParty, yParty).get_tileDrain());
            staminaText.text = "Stamina:\n" + stamina;

            rendyGrid[xParty, yParty].gameObject.GetComponent<Tile>().set_tile_image();
            //if this breaks tiles that aren't Tile, but inherit from it, just do rendyGrid[].enabled = true; and fill the image manually.

            //then, update our knowledge of the map with this new information
            heldDun.set_tileExplored(xParty, yParty);

            if ( heldDun.get_tile(xParty, yParty).hasEvent == true )
            {
                //then retrieve its event, based on its coordinates.
                //and pass it to eventmanager and have them play it.
                canDragScreen = false;
                adjust_ui_before_event();
                state = DungeonState.EVENT;
                StartCoroutine(go_encManager(heldDun.retrieve_TileEvent(xParty, yParty).get_noteTitle(), true, false, false));
            }
            else
            {
                move_ai_parties();
            }
        }
        else
        {
            move_ai_parties();
        }
        
        
    }
    
    //AI
    void choose_ai_party_destination(MobParty mp)
    {
        //taking into account the ai's vision, allows it to be sidelined by
        //a neutral or player party. it will go after the closest target party.
        //calc using a running minimum.
        int mob_vision = mp.get_vision();
        int closestPartyIndex = -1; //an index into neutral parties, or -1 for the player.

        float minHyp = Mathf.Sqrt(Mathf.Pow(mp.get_x() - xParty, 2) + Mathf.Pow(mp.get_y() - yParty, 2));

        bool standingOnTop = false; // true if the closest party is on the same tile as mp. we don't move in that case.
        for (int i = 0; i < neutralParties.Count; i++)
        {
            float currentHyp = Mathf.Sqrt(Mathf.Pow(mp.get_x() - neutralParties[i].get_x(), 2) + Mathf.Pow(mp.get_y() - neutralParties[i].get_y(), 2));
            if ( minHyp > currentHyp )
            {
                closestPartyIndex = i;
                minHyp = currentHyp;
            }

        }
        if (minHyp == 0) //if the target is on our tile, no need to move.
        {
            mp.clear_destination();
            return;
        }
        

        //if the closest party is in view range; great. we will pursue it.
        //if it isn't in view range; then forget it, we're not pursuing anyone.
        if ( minHyp <= mob_vision )
        {
            //we're good! we've found a destination.
            List<Tuple> path;
            if ( closestPartyIndex == -1)
            {
                path = pathManager.find_path(new Tuple(mp.get_x(), mp.get_y()), new Tuple(xParty, yParty));

            }
            else
            {
                path = pathManager.find_path(new Tuple(mp.get_x(), mp.get_y()), new Tuple(neutralParties[closestPartyIndex].get_x(), neutralParties[closestPartyIndex].get_y()));
            }
            mp.set_destination(path);
            return;
        }
        //OTHERWISE.
        //if we weren't sidelined, we keep doing what we were doing.
    }
    void move_ai_parties()
    {
        //ai moves at the same tile as the player.

        //choose what direction each ai party is going to move
        //set the tiles each mob party is standing on to valid in pathManager (with their old coords)      
        /*
        foreach (MobParty mp in activeParties)
        {
            pathManager.tileValidAt(mp.get_x(), mp.get_y());
        }
        */
        foreach(MobParty mp in activeParties)
        {
            choose_ai_party_destination(mp);
        }
        
        //update their visibility (if they're going to be in player's view at start of end of move, show them)
        //(yes, this can result in an ai party being shown over an unknown tile. but that's intended.)
        foreach (MobParty mp in activeParties)
        {            
            float hyp = Mathf.Sqrt(Mathf.Pow(mp.get_x() - xParty, 2) + Mathf.Pow(mp.get_y() - yParty, 2));
            if (hyp <= vision + mp.get_movement()) //add movement so we can see mobs come in from the darkness or leave into it.
            {
                //if not already visible
                place_aiParty(mp.gameObject, mp.get_x(), mp.get_y(), false);
                mp.set_visibility(true);
            }
            else
            {
                mp.set_visibility(false);
            }           
        }

        //finally, move all ai parties all at once using slide and update their coords
        foreach (MobParty mp in activeParties)
        {
            place_aiParty(mp.gameObject, mp.get_x(), mp.get_y());
            //pathManager.tileInvalidAt(mp.get_x(), mp.get_y());
        }
        
        //lastly, check for collisions between parties
        check_collision_parties();
    }
    void check_collision_parties()
    {
        //check for collisions between parties.

        //first, check for collisions between neutral parties and mob parties
        // -in the case of a collision, simply destroy the neutral party
        foreach (NeutralParty np in neutralParties)
        {
            //check if it's collided with a party
            foreach (MobParty mp in activeParties)
            {
                if ( np.get_x() == mp.get_x() && np.get_y() == mp.get_y())
                {
                    resolve_mobNeutral_collision(np, mp);
                    break;
                }
            }
        }

        //next, check for collisions between the player party and mob parties (can be more than 1)
        //(compile all mob parties that have collided with the player as a group)
        lambs.Clear();
        foreach ( MobParty mp in activeParties)
        {
            if ( mp.get_x() == xParty && mp.get_y() == yParty)
            {
                lambs.Add(mp);
            }

        }

        //if there are collisions for the player, go to encounter manager and let it do its thing.
        //set state to combatState.
        if (lambs.Count > 0)
        {
            //then, we have a battle on our hands.
            //disable ui, player can't: drag units, or hit any movement buttons.
            state = DungeonState.COMBAT;
            resolve_mobPlayer_collision();
        }
       
    }    
    void resolve_mobPlayer_collision()
    {
        //use the drop down manager to tell the player they're in a fight.
        string enemyGroupString = lambs[0].get_partyName();
        if (lambs.Count > 1)
        {
            //ACCOUNT FOR PLURAL
            enemyGroupString += " and " + (lambs.Count - 1) + " other";
            if (lambs.Count > 2) enemyGroupString += "s"; //phew, that was close.
        }
        StartCoroutine(go_encManager(enemyGroupString, true, false, false));
        //consider making the talkVal true. could be interesting to talk to enemy parties.
        //or maybe you can only talk to unique ones, like bosses or the like.
    }
    void resolve_mobNeutral_collision(NeutralParty np, MobParty mp)
    {
        //destroy the neutral party.
        Destroy(np, 1.78f);

        //some stuff to maybe add later:
        // -power up the mob party?
        // -mark them with something, then when the player meets the mobParty, they'll have captives and a scene.
        //could do some things... but for now, just leave it.
    }
    
    //EVENTS
    public void encounter_proceed()
    {
        //returns from encounter manager. depending on dungeon state, will do different things.
        switch (state)
        {
            case DungeonState.EVENT:
                go_tile_event();
                break;
            case DungeonState.COMBAT:
                go_fight();
                break;
        }
    }
    IEnumerator go_encManager(string encTitle, bool pro, bool talk, bool trade)
    {
        yield return new WaitForSeconds(1.9f / transitionDuration);
        encManager.show_dropDown(encTitle, pro, talk, trade);
    }
    void adjust_ui_before_event()
    {
        //adjust some elements of the ui while an event is playing.
        disable_movement_arrows();
        recenterCameraButton.interactable = false;
        withdrawButton.interactable = false;
        canSwapUnits = false;
        //set alpha of the party to transparent
        
    }
    void adjust_ui_after_event()
    {
        //adjust some elements of the ui while an event is playing.
        state = DungeonState.OOC;
        enable_movement_arrows();
        update_forward_arrow();
        recenterCameraButton.interactable = true;
        //
        canSwapUnits = true;

        //set alpha of the party to opaque
        unitBoxCanvasGroup.alpha = 1.0f;
    }
    
    //RUNNING
    void update_pbp(bool entering = false)
    {
        //update play by play text too
        //line 1: name and drain
        //line 2: vision

        string toShow;

        if (entering == true)
        {
            toShow = "Entered the dungeon.\nVisibility is ";
        }
        else
        {
            toShow = "Moved to " + heldDun.get_tile(xParty, yParty).get_tileName() + " using 1 stamina.\nVisibility is ";
        }

        int vis = heldDun.get_tile(xParty, yParty).get_vision();
        if (vis < 3) toShow += "poor.";
        else if (vis < 4) toShow += "fine.";
        else toShow += "good.";
        pbp.typeItOut(toShow);
    }
    void update_coord_text()
    {
        coordText.text = "X " + xParty + "\nY " + yParty;
    }
    public void swap_active_units(int slotID)
    {
        if (canDragScreen) return;

        int pick = DungeonDragDrop.pickedUpID;
        int drop = slotID;
        Unit u;

        if (pick == drop) return;

        //two possibilities:
        u = party[pick];
        u.inParty = true;
        if (party[drop] == null) //we're swapping with a blank space
        {
            party[pick] = null;
            partyBoxes[pick].fill_empty();
        }
        else //we're swapping with another unit
        {
            party[pick] = party[drop];
            partyBoxes[pick].fill_unit(party[drop], affOrbSprites[party[drop].get_affinity()], false);

        }
        party[drop] = u;
        partyBoxes[drop].fill_unit(party[drop], affOrbSprites[party[drop].get_affinity()], false);

        //reset the drag droppers.
        for (int i = 0; i < 6; i++)
        {
            //but only if unit is active
            if (partyBoxes[i].gameObject.activeSelf == true) partyBoxes[i].gameObject.GetComponent<DungeonDragDrop>().reset_after_drop();
        }
        validate_party();
    }
    void update_forward_arrow()
    {
        //sets the forward arrow to interactable or not depending on if there is a tile in that direction from us.

        //check the tile in front of the player. if null or outside dungeon size, disable forward movement arrow. else, enable.
        switch (facing)
        {
            case Direction.NORTH:
                if (yParty == yDungeon - 1)
                {
                    movementArrows[1].interactable = false;
                }                
                else if (heldDun.dungeonGrid[xParty, yParty + 1] == null)
                {
                    movementArrows[1].interactable = false;
                }
                else if (stamina == 0 && heldDun.explored_grid[xParty, yParty + 1] != Exploration.EXPLORED)
                {
                    movementArrows[1].interactable = false;
                }
                else
                {
                    movementArrows[1].interactable = true;
                }
                break;
            case Direction.SOUTH:
                if (yParty == 0)
                {
                    movementArrows[1].interactable = false;
                }
                else if (heldDun.dungeonGrid[xParty, yParty - 1] == null)
                {
                    movementArrows[1].interactable = false;
                }
                else if (stamina == 0 && heldDun.explored_grid[xParty, yParty - 1] != Exploration.EXPLORED)
                {
                    movementArrows[1].interactable = false;
                }
                else
                {
                    movementArrows[1].interactable = true;
                }
                break;
            case Direction.WEST:
                if (xParty == 0)
                {
                    movementArrows[1].interactable = false;
                }
                else if (heldDun.dungeonGrid[xParty - 1, yParty] == null)
                {
                    movementArrows[1].interactable = false;
                }
                else if (stamina == 0 && heldDun.explored_grid[xParty - 1, yParty] != Exploration.EXPLORED)
                {
                    movementArrows[1].interactable = false;
                }
                else
                {
                    movementArrows[1].interactable = true;
                }
                break;
            case Direction.EAST:
                if (xParty == xDungeon - 1)
                {
                    movementArrows[1].interactable = false;
                }
                else if (heldDun.dungeonGrid[xParty + 1, yParty] == null)
                {
                    movementArrows[1].interactable = false;
                }
                else if (stamina == 0 && heldDun.explored_grid[xParty + 1, yParty] != Exploration.EXPLORED)
                {
                    movementArrows[1].interactable = false;
                }
                else
                {
                    movementArrows[1].interactable = true;
                }
                break;
        }
    }
    public void hit_movement_arrow(int direct)
    {
        //arg legend: -1=sinister, 0=forward, 1=dexter.
        if (direct == 0) party_advance();
        else change_direction(direct);
    }
    void party_advance()
    {
        //moves the party forward. 
        //depending on direction, this can mean an increase by 1 in either x or y.
        switch (facing)
        {
            case Direction.NORTH:
                yParty += 1;
                break;
            case Direction.SOUTH:
                yParty -= 1;
                break;
            case Direction.EAST:
                xParty += 1;
                break;
            case Direction.WEST:
                xParty -= 1;
                break;
        }
        update_coord_text();

        //update_forward_arrow();

        StopAllCoroutines();
        StartCoroutine(slideListener(false));

        //move camera with it.
        place_camera();
        //move party flag to the new location.
        place_partyFlag();
        

        enter_new_tile();
    }
    void change_direction(int direct)
    {
        //sets direction to a new direction.
        //takes either -1 or 1, meaning a 90 degree rotation to the left or right.
        //sets the camera rotation.
        switch (facing)
        {
            case Direction.NORTH:
                if (direct == -1)
                {
                    facing = Direction.WEST;
                }
                else
                {
                    facing = Direction.EAST;
                }
                break;
            case Direction.SOUTH:
                if (direct == -1)
                {
                    facing = Direction.EAST;
                }
                else
                {
                    facing = Direction.WEST;
                }
                break;
            case Direction.EAST:
                if (direct == -1)
                {
                    facing = Direction.NORTH;
                }
                else
                {
                    facing = Direction.SOUTH;
                }
                break;
            case Direction.WEST:
                if (direct == -1)
                {
                    facing = Direction.SOUTH;
                }
                else
                {
                    facing = Direction.NORTH;
                }
                break;
        }
        //this is where you would update the compass.
        Vector3 rot = new Vector3(0f, 0f, direct * -90f);

        StopAllCoroutines();
        StartCoroutine(slideListener(true));
        StartCoroutine(rotate_sprite(cammy.gameObject, rot, direct));
        StartCoroutine(rotate_sprite(compassGO, rot, direct, false));
        StartCoroutine(rotate_sprite(partyFlag, rot, direct));
        rotate_tileEffects(rot, direct);

        foreach (MobParty mp in activeParties)
        {
            StartCoroutine(rotate_sprite(mp.gameObject, rot, direct));
        }
    }
    void validate_party()
    {
        //checks the party; if it doesn't like the party, it'll stop
        //the player from moving until the party is changed into a way it likes.

        //if no unit in the front; disable movement arrows.
        if (party[0] == null && party[1] == null && party[2] == null) disable_movement_arrows();
        else
        {
            enable_movement_arrows();
            update_forward_arrow();
        }
    }
    void disable_movement_arrows()
    {
        foreach (Button but in movementArrows)
        {
            but.interactable = false;
        }
        recenterCameraButton.interactable = false;
        withdrawButton.interactable = false;
        useTileButton.interactable = false;
        useHealButton.interactable = false;
    }
    void enable_movement_arrows()
    {
        foreach(Button but in movementArrows)
        {
            but.interactable = true;
        }
        recenterCameraButton.interactable = true;

        //enable or ignore withdraw button
        if (heldDun.get_tile(xParty, yParty) is HomeTile && heldDun.get_tile(xParty, yParty).isValidExit() == true)
        {
            rendyGrid[xParty, yParty].gameObject.GetComponent<HomeTile>().turn_on_sparkle();
            withdrawButton.interactable = true;
        }        
        //enable or ignore useTile button
        else if (heldDun.get_tile(xParty, yParty) is UsableTile && heldDun.tileUsedGrid[xParty, yParty] == false)
        {
            rendyGrid[xParty, yParty].gameObject.GetComponent<UsableTile>().turn_on_sparkle();
            useTileButton.interactable = true;            
        }
        //enable or ignore heal button
        useHealButton.interactable = false;
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] != null && party[i].get_hp() < party[i].get_hpMax()) { useHealButton.interactable = true; break; }
        }
            

    }

    //ENTER UNEXPLORED TILE EFFECTS
    public void unexplored_tile()
    {
        //heal each party unit's mp and hp when entering an unexplored tile.
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] != null)
            {
                int heal = Mathf.Max(1, (int)((hpPercentHealedPerNewTile / 100f) * party[i].get_hpMax()));
                party[i].heal(heal);

                int mpHeal = Mathf.Max(1, (int)((mpPercentHealedPerNewTile / 100f) * party[i].get_mpMax()));
                party[i].mp_heal(mpHeal);
            }
        }
        fill_party();
    }

    //DUNGEON HEALING BUTTON
    public void use_heal_button()
    {
        //called when the player hits the button. it opens up a more informative prompt that tells
        //the player the exact details, heal to _% and how much stam it will cost. then the player
        //hits confirm on that and we do the actual calculation.
        //lock input while the heal prompt is shown of course.

        //we pay stamina based on the percentage of each injured unit's hp we want to heal at a rate of x% per stamina.
        disable_movement_arrows();
        //first calc cost, in stamina.
        int cost = 0;
        for (int i = 0; i <party.Length; i++)
        {
            if (party[i] != null && party[i].get_hp() < party[i].get_hpMax())
            {
                //then we have to heal them. use hpPercentHealedPerStamina to calc cost.
                //trust me, the math is probably right.
                float percentageMissing = 100f * (1f - ((float)party[i].get_hp()) / ((float)party[i].get_hpMax()));
                cost += (int)(percentageMissing / hpPercentHealedPerStamina);
            }
        }
        savedHealStamCost = cost;
        string prompt = "Healing forces will take " + cost + " stamina";
        bool canHeal;
        if (cost > stamina)
        {
            canHeal = false;
            prompt += ", which is more than we have.";
        }
        else
        {
            canHeal = true;
            prompt += ".\nWould you like to proceed?";
        }
        healer.show(prompt, canHeal);
    }
    public void yes_heal_prompt()
    {
        //the player accepts the heal conditions.
        //take away their stamina, heal units, etc.
        stamina -= savedHealStamCost;
        staminaText.text = "Stamina: " + stamina;
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] != null) party[i].set_hp(party[i].get_hpMax());
        }
        fill_party();
        healer.hide();
        enable_movement_arrows();
    }
    public void no_heal_prompt()
    {
        //the player thought better of using the dungeon's healer function.
        //restore movement capabilities.
        healer.hide();
        enable_movement_arrows();
    }

    //CAMERA
    public void return_camera_to_party()
    {
        //brings the camera back to the party. lerps it over half a second.
        /*
        canDragScreen = false;
        Vector3 destination = new Vector3(0f, 0f, -10f);
        cammy.transform.position = Vector3.Lerp(transform.position, destination, 0.5f * Time.deltaTime);
        */
        place_camera(false);
    }
    public bool get_canDragScreen() { return canDragScreen; }
    public void set_canDragScreen(bool v) { canDragScreen = v; }

    //WITHDRAW
    public void press_withdraw_button()
    {
        //if we're on a CLEAR type home tile
        //Debug.Log("withdraw button pressed");
        if (heldDun.get_tile(xParty, yParty).get_isClear() == true && heldDun.cleared == false)
        {
            //Debug.Log("withdraw button pressed - 1");
            heldDun.cleared = true;
            withdraw(LeavingState.CLEAR);
        }
        else
        {
            //Debug.Log("withdraw button pressed - 2");
            withdraw(LeavingState.WITHDRAW);
        }
    }
    public void withdraw(LeavingState leaving)
    {
        //called when the player clicks the withdraw button.
        //it's instant.

        //so, we're going to have to save some info, etc, and ALSO bring some information back over
        //to the overworld.

        //we need to save:
        // -exp and loot obtained by the party
        // -dungeon's exploration/bosses defeated status.

        //we use a Cart object to ferry this information back.
        //in overworld's Awake, the state will be set to Returning, so the first thing it will do 
        //is unload the cart.

        cart.dun_fill_cart(heldDun, obtainedXP, leaving);

        SceneManager.LoadScene("OverworldScene");
    }

    //USE TILE BUTTON
    public void use_tile_button()
    {
        //if this button was clickable, then it means that the tile we're on has something for us.
        //AND that it hasn't been used before.
        useTileButton.interactable = false;
        heldDun.tileUsedGrid[xParty, yParty] = true;
        rendyGrid[xParty, yParty].gameObject.GetComponent<UsableTile>().turn_off_sparkle();

        heldDun.get_tile(xParty, yParty).use_tile(this);

        //update stamina text
        staminaText.text = "Stamina: " + stamina;

        //update dungeon boxes
        fill_party();

    }

    

    //MUSIC
    public AudioClip get_combatTheme()
    {
        return heldDun.get_combatTheme();
    }
}
