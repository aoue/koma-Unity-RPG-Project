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

    private List<Unit> LTparty;
    private Unit currentlySelectedUnit; //reference for the unit whose tree we're looking at.
    private LevelTreeMove moveInQuestion { get; set; } //the move the unit is doing anything with. set when the player mouses over a LevelTreeMove object.

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
        unitPreviewer.show(currentlySelectedUnit);

        //show new currently shown unit's level tree.
        foreach(LevelTreeUnitManager lt in unitLevelTrees)
        {
            if (lt.get_linkId() == currentlySelectedUnit.get_unitId())
            {
                //load new currentlySelectedUnit's level tree
                lt.load_up(currentlySelectedUnit.get_allKnownMoveIds());
                moveViewer.hide();
                break;
            }
        }

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
        learnMoveButton.interactable = false;

        //display the currentlySelectedUnit's information:
        unitPreviewer.show(currentlySelectedUnit);
      
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
        moveViewer.fill(ltm.get_expCost().ToString(), ltm.get_minLevel().ToString(), ltm.get_containedMove().get_nom(), ltm.get_containedMove().generate_preview_text());
        moveInQuestion = ltm;
        //when locked, you can now interact with the learn move button.

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

        //update exp display in the previewer ui
        unitPreviewer.update_exp();
    }

    //LEVELING UP
    public void click_level_up()
    {
        //called when player clicks level up button.
        //for this button to have been clicked, we already know a level up is valid.
    }
    void validate_level_up_button()
    {
        //if the player no longer has enough exp to afford another level up, the button
        //is set to non-interactable.
        //otherwise, it is set to interactable.
    }
    int calculate_next_level_up_exp()
    {
        //returns the exp necessary for the unit's next level up.
        //value returned depends on: unit's level...

        //if level cap reached, fail immediately.

        //what else? number of moves learned?
        //should we have a system where learning moves increases level up exp
        //and leveling up increases move learning exp?


        return 0;
    }


}
