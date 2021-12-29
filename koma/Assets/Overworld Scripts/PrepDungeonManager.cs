using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PrepDungeonManager : MonoBehaviour
{
    //takes a hint from dungeonholders->overworld and shows the prep dungeon menu.
    //allows players to modify party stuff, etc.
    //player hits confirm button when ready and party valid, and we load the dungeonScene.
    //can click on a single unit box for a more detailed look at the unit, but you can't edit them here.

    [SerializeField] private Overworld theWorld; //used to save information in the cart.

    private static List<Unit> reserveParty; //all units, including the ones in the expedition party.
    private static Unit[] party; //the expedition party. always size 6. empty units are null.
    //private Inventory inven; (holds arm, wpn, acc, AND moves)
    [SerializeField] private Text unusedText;
    [SerializeField] private GameObject unitPreviewGO;
    [SerializeField] private Image bgImage;
    [SerializeField] private Button embarkButton; //button that sends us to the dungeon.
    [SerializeField] private Text unitLimitText;
    [SerializeField] private UnitBox[] unitBoxes; //dimensions are 500x250. 2:1
    [SerializeField] private ReserveUnitBox[] reserveUnitBoxes; //dimensions are 110x110. 1:1
    private int unitsInParty; //to help with the unit limit.
    private int dungeonId; //the id of the dungeon we're headed to.
    private int unitLimit; //how many units can you bring into this dungeon at this time. set in load_up.
    private bool unitInFront; //when not true, can only place units in the front row. check everytime a unit is placed.

    [SerializeField] private Sprite[] affOrbSprites;

    
    public void inc_exp()
    {
        foreach (Unit partyMember in reserveParty)
        {
            partyMember.set_hp(partyMember.get_hpMax());
            partyMember.set_mp(partyMember.get_mpMax());
        }
        
        ExpManager.distribute_xp(reserveParty);
    }

    public List<Unit> get_reserveParty() { return reserveParty; }
    public void refill_reserve(List<Unit> replacementParty) { reserveParty = replacementParty; }

    public void load_up(Dungeon dun)
    {
        unitPreviewGO.SetActive(false);
        DungeonManager.heldDun = dun;

        //take necessary info from dun
        unitLimit = dun.get_unitLimit();
        unitLimitText.text = "Unit Limit: " + unitLimit;
        dungeonId = dun.get_dungeonId();

        //add units alraedy in party to their boxes
        //party length is always 6        
        for (int i = 0; i < 6; i++)
        {
            party[i] = null;
            unitBoxes[i].fill_empty();
        }
        foreach(Unit u in reserveParty)
        {
            u.inParty = false;
            u.set_mp(u.get_mpMax());
        }
        unitsInParty = 0;

        load_reserveParty();
        isPartyValid();
        gameObject.SetActive(true);
        
    }

    //STRIDES
    void load_reserveParty()
    {
        
        //sets up reserve party.
        //fills each slot with a reservepartyunit. if the unit is in the party, then the slot is not interactable.
        for (int i = 0; i < reserveParty.Count; i++)
        {
            if (reserveParty[i] != null)
            {                
                reserveUnitBoxes[i].fill_unit(reserveParty[i]);
            }
        }

        //hide the rest of the boxes.
        for (int i = reserveParty.Count; i < reserveUnitBoxes.Length; i++)
        {
            reserveUnitBoxes[i].gameObject.SetActive(false);
        }

    }
    public void first_time_setup()
    {       
        //called by overworld on its firsttime
        if (party == null)
        {
            party = new Unit[6];
        }
        if (reserveParty == null)
        {
            reserveParty = new List<Unit>();
        }
    }
    public void add_to_party(Unit[] newPartyMembers)
    {
        //called from overworld after join party type events
        //adds to reserse party.
        foreach(Unit u in newPartyMembers)
        {
            u.set_hp(u.get_hpMax());
            u.set_mp(u.get_mpMax());
            reserveParty.Add(u);
            u.inParty = false;
        }
    }
    public void close()
    {
        //called on close button click.
        gameObject.SetActive(false);
    }
    
    public void to_the_dungeon()
    {
        //pass along any necessary relevant info to the dungeonSceneManager.

        //(i think you need to do dontdestroyonload to the player units too.)

        

        //finally, load the dungeon scene.
        //Debug.Log("loading dungeon scene");
        //pass party to dungeonManager
        DungeonManager.party = party;

        theWorld.fill_cart();

        SceneManager.LoadScene("DungeonScene");
    }

    //SWAPPING
    public void swap_active_units(int slotID)
    {
        //this function is used to swap the units (or emptiness) in two slots.
        //id (0 - ?) of picked up unit

        //here's how the picked up id works:
        //id -1 : the player wants to remove that unit from the party. dragged it into removal slot.
        //ids 0 - 5: the player picked up from the active party slots
        //ids 6+ : the player picked up from one of the reserve portraits. substract 6 to get index in the reserve party.
        //Note: it is already guaranteed by this point that the id will get a non-null element.
        //Note: we must enforce the unit limit. (only matters when bringing in a unit from reserve to an empty slot)

        int pick = DragDrop.pickedUpID;
        int drop = slotID;
        Unit u;
        
        if (drop == -1) //if drop == 7, then the player wants to remove that unit from the party.
        {
            party[pick].inParty = false;
            party[pick] = null;
            unitsInParty--;
            unitBoxes[pick].fill_empty();
        }      
        else if ( pick < 6 && pick != drop)
        {
            //then use the party and the unit's index is pick.
            
            u = party[pick];
            u.inParty = true;
            if (party[drop] == null) //we're swapping with a blank space
            {               
                party[pick] = null;
                unitBoxes[pick].fill_empty();
            }
            else //we're swapping with another unit
            {
                party[pick] = party[drop];     
                unitBoxes[pick].fill_unit(party[drop], affOrbSprites[party[drop].get_affinity()]);
                
            }
            party[drop] = u;
            unitBoxes[drop].fill_unit(party[drop], affOrbSprites[party[drop].get_affinity()]);
        }       
        else if (pick >= 6)
        {
            //then use the reserveParty and the unit's index is pick - 6.

            //if dragged into an empty slot:
            if (party[drop] != null) //the dropped party slot has a unit in it already, though.
            {
                party[drop].inParty = false;
            }
            u = reserveParty[pick - 6];
            u.inParty = true;
            party[drop] = u;

            // unitbox need to be rewritten,
            unitBoxes[drop].fill_unit(party[drop], affOrbSprites[party[drop].get_affinity()]);

            // reserveParty; check all slots there again, change interactable where necessary.
            //for each unit in reserve, if u.inPARTY: interact=true, else: interact=false.
        }
        else
        {
            return;
        }

        //reset the drag droppers.
        for (int i = 0; i < 6; i++)
        {
            //but only if unit is active
            if (unitBoxes[i].gameObject.activeSelf == true) unitBoxes[i].gameObject.GetComponent<DragDrop>().reset_after_drop();
        }

        refresh();
    }

    //LITTLE STEPS
    public Unit get_unit(int which)
    {
        //0-5: check party.
        //6+ : subtract 6, then check reserveParty.
        if (which < 6)
        {
            return party[which];
        }
        else
        {
            return reserveParty[which - 6];
        }
    }
    void refresh()
    {
        //refreshes menus after a change has been made
        load_reserveParty();
        isPartyValid();
    }   

    void isPartyValid()
    {
        //checks if the party is valid:
        //conditions:
        // -there is at least one unit in the front three rows.
        // -the number of non-null units in the party is not greater than the unit limit
        //if all conditions are met, then it enables the embarkButton.

        bool isValid = true;

        if (party[0] == null && party[1] == null && party[2] == null)
        {
            isValid = false;
        }
        else
        {
            int nonNullCount = 0;
            for (int i = 0; i < 6; i++)
            {
                if (party[i] != null)
                {
                    nonNullCount++;
                }
            }

            if (nonNullCount > unitLimit) isValid = false;

        }

        if (unitsInParty > unitLimit) isValid = false;

        //do the move check here

        if (isValid == true)
        {
            embarkButton.interactable = true;
        }
        else
        {
            embarkButton.interactable = false;
        }

    }

    

}
