using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTreeManager : MonoBehaviour
{
    //this is the level tree manager. it is used to manage the move learning/level uppping of party units.

    [SerializeField] private PrepDungeonManager pdm; //used to get proper order and party unit information. uses reserve party.
    [SerializeField] private GameObject levelTreeCanvas; //the canvas where this all happens. 
    [SerializeField] private LevelTreeUnitButton[] unitButtons; //used to switch to the levelTree for a different unit. draws from pdm's party.
    [SerializeField] private LevelTreePreviewer unitPreviewer;

    private List<Unit> LTparty;
    private Unit currentlySelectedUnit; //reference for the unit whose tree we're looking at.
    public Move moveInQuestion { get; set; } //the move the unit is doing anything with. set when the player mouses over a LevelTreeMove object.

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
        currentlySelectedUnit = LTparty[which];
        unitPreviewer.show(currentlySelectedUnit);
    }
    public void load_up()
    {
        //called when player clicks the overworld leveltree button.           
        //load in the party, eh?
        LTparty = pdm.get_reserveParty();

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
            currentlySelectedUnit = LTparty[0];
        }
        moveInQuestion = null;

        //display the currentlySelectedUnit's information:
        unitPreviewer.show(currentlySelectedUnit);
      
        levelTreeCanvas.SetActive(true);
    }

    public void click_levelTreeMove_assignmentButton(int which)
    {
        //which: the equip button on the levelTreeMove item that was clicked. 0-4. equips move to corresponding unit's move slot, 0-4

        //for this function to be called, the move must already be learned.
        //fails if:
        // -unit already has move equipped in some other slot.

        foreach (Move equippedMove in currentlySelectedUnit.get_moveset())
        {
            if (equippedMove == moveInQuestion)
            {
                return;
            }
        }

        //all clear, so now equip the move you want to the argumented slot
        currentlySelectedUnit.get_moveset()[which] = moveInQuestion;
        unitPreviewer.show(currentlySelectedUnit);
    }
    public void click_learnMove()
    {
        //the unit learns a move. it pays for the move with exp, and the move becomes permanently available in the moveTree.

    }
}
