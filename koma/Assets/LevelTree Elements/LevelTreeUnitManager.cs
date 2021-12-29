using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTreeUnitManager : MonoBehaviour
{
    //manages the level tree for one unit.
    //must manage:
    // -which moves are learned by the unit already. Must be preserved between scenes.
    //this is preserved by the unit themselves, in their allKnownMoves list field.

    //for all moves (i.e. all children of the GO) in the level tree, check if it is in
    //the list. if it is, set it to learned and the player can equip it, etc.
    //if not, the player can learn it or they can't if the prereq isn't met.

    [SerializeField] private int linkId; //represents the level tree of the same unit.

    public void load_up(List<int> allKnownMoveIds)
    {
        //if a move's id is in allKnownMoveIds, the move is already learned.
        //load the level tree this way.

        foreach (Transform child in transform)
        {          
            if ( allKnownMoveIds.Contains(child.gameObject.GetComponent<LevelTreeMove>().get_containedMove().get_moveID()) )
            {
                //move is known:
                //-enable equip buttons
                //-make button interactable
                //child.gameObject.GetComponent<LevelTreeMove>().show_assignment_buttons();
                child.gameObject.GetComponent<Button>().interactable = true;
                //-set text to learned
                child.gameObject.GetComponent<LevelTreeMove>().set_expCostText("<i>Learned</i>");
            }
            else
            {
                //move is not known:
                //-disable equip buttons
                child.gameObject.GetComponent<LevelTreeMove>().hide_assignment_buttons();

                //check if move is learnable:
                //-if it is, interactable
                //-if it is not, not interactable
                if ( move_is_learnable(child.gameObject.GetComponent<LevelTreeMove>().get_prereqs(), allKnownMoveIds) == true)
                {
                    //move is learnable
                    child.gameObject.GetComponent<Button>().interactable = true;
                    //set text to exp cost
                    child.gameObject.GetComponent<LevelTreeMove>().display_expCost();
                }
                else
                {   
                    //move is not learnable
                    child.gameObject.GetComponent<Button>().interactable = false;
                    //set texp to ineligible
                    child.gameObject.GetComponent<LevelTreeMove>().set_expCostText("Ineligible");
                }                           
            }
        }

        gameObject.SetActive(true);
    }
    public void close()
    {
        gameObject.SetActive(false);
    }


    bool move_is_learnable(int[] prereqs, List<int> allKnownMoveIds)
    {
        //returns true if move is learnable, false otherwise.
        //a move is learnable is all of its prereqs are in the list of allKnownMoveIds.
        foreach (int i in prereqs)
        {
            if ( allKnownMoveIds.Contains(i) == false )
            {
                return false;
            }
        }
        return true;
    }

    //getters
    public int get_linkId() { return linkId; }
}
