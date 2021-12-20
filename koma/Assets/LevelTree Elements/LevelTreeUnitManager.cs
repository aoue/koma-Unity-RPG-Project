using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField ]private List<int> allKnownMoveIds; //all the known moves of this particular unit. it's a prefab, so they save themselves.

    public void load_up()
    {
        //if a move's id is in allKnownMoveIds, the move is already learned.
        //generate the level tree this way.

        //first pass: equippable moves
        //for all children of this gameobject (the children are of type leveltreemove)
        //  if leveltreemove contained move id is in allKnownMoveIds
        //      interactable
        //  else
        //      not interactable

        //second pass: learnable moves
        //for all children of this gameobject (the children are of type leveltreemove) 
        //  if not interactable
        //      if prereqs are all in allKnownMoveIds
        //          then learnable
        //

        gameObject.SetActive(true);
    }
    public void close()
    {
        gameObject.SetActive(false);
    }
    

    //getters
    public int get_linkId() { return linkId; }
}
