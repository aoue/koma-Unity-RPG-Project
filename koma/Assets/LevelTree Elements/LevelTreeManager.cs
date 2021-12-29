using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTreeManager : MonoBehaviour
{
    //this is the level tree manager. it is used to manage the move learning/level uppping of party units.

    [SerializeField] private LevelTreeUnitManager[] unitLevelTrees; //the level trees. a special one for each unit.

    [SerializeField] private PrepDungeonManager pdm; //used to get proper order and party unit information. uses reserve party.
    [SerializeField] private GameObject levelTreeCanvas; //the canvas where this all happens. 
    [SerializeField] private LevelTreeUnitButton[] unitButtons; //used to switch to the levelTree for a different unit. draws from pdm's party.
    [SerializeField] private LevelTreePreviewer unitPreviewer; //used to see the currently viewed unit's stats
    [SerializeField] private LevelTreeMoveViewer moveViewer; //used to see the currently locked move's information
    [SerializeField] private Button learnMoveButton; //button player clicks to get a unit to learn a move in exchange for exp.
    [SerializeField] private Text levelUpButtonText; //text on the level up button; shows needed exp cost for next level up.
    [SerializeField] private Button levelUpButton; //button that allows level up. valid when unit has exp > level up cost

    private List<Unit> LTparty;
    private Unit currentlySelectedUnit; //reference for the unit whose tree we're looking at.
    private LevelTreeMove moveInQuestion { get; set; } //the move the unit is doing anything with. set when the player mouses over a LevelTreeMove object.

    private int[] levelUpExpCosts = new int[]
    {
        100, 140, 185, 200, 250, 300, 350, 400, 450, 500,
        600, 700, 800, 900, 1000, 1100, 1200, 1300, 1400, 1500,
        1650, 1800, 1950, 2100, 2250, 2400, 2550, 2700, 2850, 3000
    };

    //LOADING AND CLOSING
    public void close()
    {
        //called when player clicks the close button on the level tree.       
        levelTreeCanvas.SetActive(false);
    }
    public void switch_unit(int which)
    {
        //called when a unitButtons is clicked that is different to the currently being viewed unit.
        //reload tree;
        //reload stats;

        //hide currently shown unit's level tree
        foreach (LevelTreeUnitManager lt in unitLevelTrees)
        {
            if (lt.get_linkId() == currentlySelectedUnit.get_unitId())
            {
                lt.close();
                break;
            }
        }


        currentlySelectedUnit = LTparty[which];
        
        //show new currently shown unit's level tree.
        foreach (LevelTreeUnitManager lt in unitLevelTrees)
        {
            if (lt.get_linkId() == currentlySelectedUnit.get_unitId())
            {
                //load new currentlySelectedUnit's level tree
                lt.load_up(currentlySelectedUnit.get_allKnownMoveIds());

                break;
            }
        }
        if ( moveInQuestion != null)
        {
            moveInQuestion.hide_assignment_buttons();
            moveInQuestion = null;
        }
        
        moveViewer.hide();
        unitPreviewer.show(currentlySelectedUnit);

        //set up leveling up
        validate_level_up_button();
    }
    public void load_up()
    {
        //called when player clicks the overworld leveltree button.           
        //load in the party, eh?
        LTparty = pdm.get_reserveParty();
        moveViewer.hide();

        //fails if party is null
        if (LTparty == null || LTparty.Count == 0) return;

        //display the unitButtons, drawing info from pdm's reserve party.
        for (int i = 0; i < LTparty.Count; i++)
        {
            //fill in box portraits and names
            unitButtons[i].fill(LTparty[i].get_boxImg(), LTparty[i].get_nom());
        }
        if (currentlySelectedUnit == null)
        {
            //show level tree if none is being shown
            currentlySelectedUnit = LTparty[0];
            foreach (LevelTreeUnitManager lt in unitLevelTrees)
            {
                if (lt.get_linkId() == currentlySelectedUnit.get_unitId())
                {
                    lt.load_up(currentlySelectedUnit.get_allKnownMoveIds());
                    break;
                }
            }
        }
        moveInQuestion = null;
        moveViewer.hide();

        //display the currentlySelectedUnit's information:
        unitPreviewer.show(currentlySelectedUnit);

        //set up leveling up
        validate_level_up_button();

        //ready to show
        levelTreeCanvas.SetActive(true);
    }

    //LEVEL TREE MOVE BUTTONS
    public void hover_levelTreeMove(LevelTreeMove ltm)
    {
        //Debug.Log("hovered move: " + ltm.get_containedMove().get_nom());
        //called when the player hovers over one of the levelTreeMove objects on screen.
        //shows the locked move in the move displayer.      
        moveViewer.fill(ltm.get_expCost().ToString(), ltm.get_minLevel().ToString(), ltm.get_containedMove().get_nom(), ltm.get_containedMove().generate_preview_text());

        //if no locked move, then lock.
        if ( moveInQuestion == null)
        {
            moveInQuestion = ltm;
        }

    }
    public void unhover_levelTreeMove(LevelTreeMove ltm)
    {
        //called when the player unhovers over one of the levelTreeMove objects on screen.
        //shows the locked move in the displayer in the bottom right.
        if (ltm != moveInQuestion)
        {
            moveViewer.fill(ltm.get_expCost().ToString(), ltm.get_minLevel().ToString(), moveInQuestion.get_containedMove().get_nom(), moveInQuestion.get_containedMove().generate_preview_text());
        }
    }
    public void click_levelTreeMove(LevelTreeMove ltm)
    {
        //called when the player clicks one of the levelTreeMove objects on screen.
        //locks them to the move displayer in the bottom right.

        //hide the assignment buttons of the previously locked move.
        moveInQuestion.hide_assignment_buttons();

        moveViewer.fill(ltm.get_expCost().ToString(), ltm.get_minLevel().ToString(), ltm.get_containedMove().get_nom(), ltm.get_containedMove().generate_preview_text());
        moveInQuestion = ltm;
        //when locked, you can now interact with the learn move button.

        //only show the assignment buttons of the locked move; and only then if it is learned.
        if (ltm.get_alreadyLearned() == true)
        {
            ltm.show_assignment_buttons();
        }

        //if unit has enough exp to pay cost AND is high enough level AND move is not learned, then enable learn move button
        if (currentlySelectedUnit.get_exp() >= moveInQuestion.get_expCost() && currentlySelectedUnit.get_level() >= moveInQuestion.get_minLevel() && moveInQuestion.get_alreadyLearned() == false)
        {
            learnMoveButton.interactable = true;
        }
        else
        {
            learnMoveButton.interactable = false;
        }
    }
    public void click_levelTreeMove_assignmentButton(int which)
    {
        //which: the equip button on the levelTreeMove item that was clicked. 0-4. equips move to corresponding unit's move slot, 0-4

        //for this function to be called, the move must already be learned.
        //fails if:
        // -unit already has move equipped in some other slot.

        //prevent move spoofing
        if (moveInQuestion.get_alreadyLearned() == false)
        {
            return;
        }

        foreach (Move equippedMove in currentlySelectedUnit.get_moveset())
        {
            if (equippedMove == moveInQuestion.get_containedMove())
            {
                return;
            }
        }

        //all clear, so now equip the move you want to the argumented slot
        currentlySelectedUnit.get_moveset()[which] = moveInQuestion.get_containedMove();
        unitPreviewer.show(currentlySelectedUnit);
    }
    public void click_levelTreeMove_learnMove()
    {
        //the unit learns a move. it pays for the move with exp, and the move becomes permanently available in the moveTree.
        //add the move's id to that leveltreeunitmanager's allknownmoveids
        //called by pressing the learn move button in the bottom right near the move viewer.

        //so the player successfully learns the locked move.
        //-pay exp cost
        //-set move to learned
        currentlySelectedUnit.pay_exp(moveInQuestion.get_expCost());
        moveInQuestion.set_alreadyLearned(true);

        //-add move's id to unit's level tree allKnownMoveIds
        //-refresh ui as a whole
        foreach (LevelTreeUnitManager lt in unitLevelTrees)
        {
            if (lt.get_linkId() == currentlySelectedUnit.get_unitId())
            {
                currentlySelectedUnit.add_moveId(moveInQuestion.get_containedMove().get_moveID());
                lt.load_up(currentlySelectedUnit.get_allKnownMoveIds());
                break;
            }
        }

        //disable learn move button:
        //(don't want player learning the same move twice by mistake; throwing their exp down a hole)
        learnMoveButton.interactable = false;
        moveInQuestion.show_assignment_buttons();

        //update exp display in the previewer ui
        unitPreviewer.update_exp();
    }

    //LEVELING UP
    public void click_level_up()
    {
        //called when player clicks level up button.
        //for this button to have been clicked, we already know a level up is valid.

        //subtract exp from unit
        currentlySelectedUnit.pay_exp(calculate_next_level_up_exp());

        //increase unit's level
        currentlySelectedUnit.set_level(currentlySelectedUnit.get_level());

        //increase unit's stats.
        level_up_stat_increases();

        validate_level_up_button();

        //update ui: unit's stats
        unitPreviewer.show(currentlySelectedUnit);
    }
    void validate_level_up_button()
    {
        //if the player no longer has enough exp to afford another level up, the button
        //is set to non-interactable.
        //otherwise, it is set to interactable.
        int cost = calculate_next_level_up_exp();
        //Debug.Log("levelUpExpCosts.Length = " + levelUpExpCosts.Length);

        if ( currentlySelectedUnit.get_exp() >= cost /*&& currentlySelectedUnit.get_level() <= levelUpExpCosts.Length*/ )
        {
            levelUpButton.interactable = true;
        }
        else
        {
            levelUpButton.interactable = false;
        }
        levelUpButtonText.text = "Level Up\n(" + cost + " EXP)";
    }  
    int calculate_next_level_up_exp()
    {
        //returns the exp necessary for the unit's next level up.
        //Mathf.Pow(f, p): returns f raised to power p

        return levelUpExpCosts[currentlySelectedUnit.get_level() - 1];

        //EXPONENTIAL
        //formula:  100 * (1.4)^(lvl-1)
        //1: 100
        //2: 150
        //3: 225
        //4: 337 
        //5: 506
        //6: 759
        //7: 1139
        //8: 1708
        //9: 2562
        //20: 59763
        //30: 1728600
        //notes: nah...
        //return (int)(100 * Mathf.Pow(1.5f, currentlySelectedUnit.get_level() - 1));      
    }

    void level_up_stat_increases()
    {
        //what to increase:
        /*
        currentlySelectedUnit.inc_hpMax(0, 0);
        currentlySelectedUnit.inc_mpMax(0, 0);
        currentlySelectedUnit.inc_patk(0, 0);
        currentlySelectedUnit.inc_pdef(0, 0);
        currentlySelectedUnit.inc_matk(0, 0);
        currentlySelectedUnit.inc_mdef(0, 0);
        */

        //see the party units spreadsheet for notes and projections.

        //the switch determines by how much each stat increases,
        currentlySelectedUnit.set_level(currentlySelectedUnit.get_level() + 1);
        switch (currentlySelectedUnit.get_unitId())
        {
            case 0: //mc
                currentlySelectedUnit.inc_hpMax(10, 12);
                currentlySelectedUnit.inc_mpMax(2, 4);
                currentlySelectedUnit.inc_patk(3, 4);
                currentlySelectedUnit.inc_pdef(4, 6);
                currentlySelectedUnit.inc_matk(7, 10);
                currentlySelectedUnit.inc_mdef(7, 8);
                break;
            case 1: //friday
                currentlySelectedUnit.inc_hpMax(14, 17);
                currentlySelectedUnit.inc_mpMax(1, 2);
                currentlySelectedUnit.inc_patk(7, 9);
                currentlySelectedUnit.inc_pdef(9, 10);
                currentlySelectedUnit.inc_matk(7, 9);
                currentlySelectedUnit.inc_mdef(7, 9);
                break;
            case 2: //moth
                currentlySelectedUnit.inc_hpMax(9, 11);
                currentlySelectedUnit.inc_mpMax(2, 3);
                currentlySelectedUnit.inc_patk(3, 5);
                currentlySelectedUnit.inc_pdef(5, 6);
                currentlySelectedUnit.inc_matk(6, 7);
                currentlySelectedUnit.inc_mdef(9, 12);
                break;
            default:
                Debug.Log("Error: level_up_stat_increases() exited switch on default statement. unitId: " + currentlySelectedUnit.get_unitId() + "not found.");
                break;
        }


    }



}
