using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTreeMove : MonoBehaviour
{
    //represents a level tree move.
    //can be learned and equipped by units.

    //different for each move
    [SerializeField] private Move containedMove; //the move that the slot represents
    [SerializeField] private int expCost; //the amount of exp the unit must pay to learn the move.
    [SerializeField] private int minLevel; //the lowest level a unit can learn this move.

    //preset
    [SerializeField] private Image bg; //to register clicks. Also, is set to the colour of the containedMove's affinity.
    [SerializeField] private CanvasGroup assignmentGroup; //hide when button is no-no intteractable.
    [SerializeField] private Text moveNameText; //text to display the move's name
    [SerializeField] private Text ExpCostText; //text to display the move's name

    //prerequisistes
    [SerializeField] private int[] prerequisiteIds; //the unit must know all moves with matching ids to learn this move.
    [SerializeField] bool alreadyLearned; 

    void Start()
    {
        if (containedMove == null)
        {
            Debug.Log("error: some LevelTreeMove container has no move assigned to it! Epic Fail!");
            return;
        }

        //set the move's name:
        moveNameText.text = containedMove.get_nom();

        //set the colour of the move bg to match containedMove's affinity:
        bg.color = AffKeyWords.get_aff_color(containedMove.get_affinity());
    }
    
    public void hide_assignment_buttons()
    {
        assignmentGroup.alpha = 0f;
    }
    public void show_assignment_buttons()
    {
        assignmentGroup.alpha = 100f;
    }

    //SETTERS
    public void set_alreadyLearned(bool v) { alreadyLearned = v; }
    public void set_expCostText(string s) { ExpCostText.text = s; }
    public void display_expCost() { ExpCostText.text = expCost.ToString() + " EXP"; }

    //GETTERS
    public Move get_containedMove() { return containedMove; }
    public int get_expCost() { return expCost; }
    public int get_minLevel() { return minLevel; }
    public int[] get_prereqs() { return prerequisiteIds; }
    public bool get_alreadyLearned() { return alreadyLearned; }
}
