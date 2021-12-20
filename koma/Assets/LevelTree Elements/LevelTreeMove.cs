using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTreeMove : MonoBehaviour
{
    //represents a level tree move.
    //can be learned and equipped by units.

    [SerializeField] private Image bg; //to register clicks. Also, is set to the colour of the containedMove's affinity.
    [SerializeField] private Move containedMove; //the move that the slot represents
    [SerializeField] private Button[] assignmentButtons; //5 buttons. clicking 0-4 corresponds to equipping the move to unit's moveslot 0-4





}
