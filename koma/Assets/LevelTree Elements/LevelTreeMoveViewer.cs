using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTreeMoveViewer : MonoBehaviour
{
    //used to display things in the level tree move viewer.
    //doesn't handle any events or anything like that, just encapsulates move view display
    //on the orders of the level tree manager.

    //these all get filled out by fill_move()
    [SerializeField] Text expCostText; //filled by ltm's expCost
    [SerializeField] Text minLevelText; //filled by ltm's minLevel

    [SerializeField] Text moveNameText; //filled by move's nom
    [SerializeField] Text moveDescrText; //filled by move's generate string

    public void fill(string expCostStr, string minLevelStr, string moveNameStr, string moveDescrStr)
    {
        expCostText.text = expCostStr;
        minLevelText.text = minLevelStr;
        moveNameText.text = moveNameStr;
        moveDescrText.text = moveDescrStr;
    }
    public void hide()
    {
        expCostText.text = "";
        minLevelText.text = "";
        moveNameText.text = "";
        moveDescrText.text = "";
    }

}
