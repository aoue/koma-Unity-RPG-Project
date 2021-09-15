using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTreeManager : MonoBehaviour
{
    //this is the level tree manager. it is used to manage each party unit.
    //each unit has a separate, hard-coded, level tree screen. uses unit's id to match them to a certain level tree screen. 

    //the level tree part:
    // -all moves the unit knows MUST be part of the move tree, even the ones they start knowing. (so they can re-equip them later)
    // -drag a level tree move into one of the unit's equip slots to swap the moves. can only drag learned moves. cannot have the same move twice in equipped moves.
    // -to learn a move you must be over min level, have enough exp, and have learned prereq.

    //regardless of unit, there are some things in common:
    // -image buttons (using pdm's reserve party) that are used to switch to a different unit's level tree screen.
    // -unit overview portion:
    //      - unit breakdown, stats and equipped moves. (<-- drag level tree moves onto these to swap them)
    //      - hovered move slot info. displays move's details, exp and min level to learn it (if unlearned), and learn move button (interactable if conditions are met)
    //      - level up area: displays current exp, exp needed to level up, and the level up button (interactable if have enough exp)

    //held info:
    // -held unit (the unit whose page we're viewing)
    // -held move (the last move clicked from the learn tree or unit's equipped moves. when not hovering a move, this is shown in hovered move slot.)
    //  this also means a move must be held move to be learned.
    

}
