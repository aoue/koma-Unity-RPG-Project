using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpeditionSummaryManager : MonoBehaviour
{
    //some things to add, going forward:
    //can add some statistics (the unit name and the numbers), e.g.:
    // -most damage
    // -most heals
    // -most damage taken
    // -most kills
    // -most revives
    // -most deaths
    //etc. 

    //can have special exp/gold increasers too.
    //e.g.:
    // -gold and exp * ? if you didn't use any of your heal button charges.
    // -
    
    //the expedition summary manager is for reporting the results of the expedition to the player
    //at the occasion of their withdrawal.
    //report screen includes:
    // -confirm button
    // -cancel button
    //displays:
    // -as a title: whether the expedition was a loss, withdraw, or clear.
    // -dungeon name and exploration number
    // -exp and gold gathered, and then the amount that will be returned.
    // -old dungeon exploration amount alongside the new amount.

    [SerializeField] private DungeonManager dman;

    //buttons
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    //text fields for displaying
    [SerializeField] private Text reportTitleText;
    [SerializeField] private Text dungeonAndExpeditionText;
    [SerializeField] private Text goodiesText;
    //[SerializeField] private Text

    private int beginningExploration; //the exploration (explored tiles) at the start of the mission. set by dman at start.
    private LeavingState heldLeaving; //saved on show().

    public void set_beginningExploration(int x)
    {
        beginningExploration = x;
    }

    public void show(LeavingState leaving)
    {
        //shows everything and allows the two buttons to be clicked.
        //triggered by withdrawButton press (the one on the ui bar) OR the post battle withdraw button press.

        //public enum LeavingState { LOSS, WITHDRAW, CLEAR }
        switch (leaving)
        {
            case LeavingState.LOSS:
                reportTitleText.text = "Expedition retreats...";
                break;
            case LeavingState.WITHDRAW:
                reportTitleText.text = "Expedition withdraws.";
                break;
            case LeavingState.CLEAR:
                reportTitleText.text = "Expedition clears!";
                break;
        }
        heldLeaving = leaving;

        //= dungeonname \n expediton #x
        dungeonAndExpeditionText.text = DungeonManager.heldDun.get_dungeonTitle() + "\nExpedition #" + (DungeonManager.heldDun.expeditionCounter + 1).ToString();

        //= exp, gold, and exploration status
        if (leaving == LeavingState.LOSS)
        {
            goodiesText.text = "The party lost some of what they were carrying in their haste to escape..."
            + "\nXP: " + dman.obtainedXP + " -> " + (dman.obtainedXP / 2).ToString()
            + "\nGold: " + dman.obtainedGold + " -> " + (dman.obtainedGold / 2).ToString()
            + "\nExploration: " + beginningExploration + "/" + DungeonManager.heldDun.totalTiles + " -> " + DungeonManager.heldDun.exploredTiles + "/" + DungeonManager.heldDun.totalTiles;

        }
        else
        {
            goodiesText.text = "The party returns with their spoils intact!"
            + "\nXP: " + dman.obtainedXP
            + "\nGold: " + dman.obtainedGold
            + "\nExploration: " + beginningExploration + "/" + DungeonManager.heldDun.totalTiles + " -> " + DungeonManager.heldDun.exploredTiles + "/" + DungeonManager.heldDun.totalTiles;

        }

        gameObject.SetActive(true);
    }
    public void confirm()
    {
        //triggered by confirmButton pressed, so now we tell dman that really, yes, you can withdraw.

        dman.withdraw(heldLeaving);

        gameObject.SetActive(true);
    }
    public void cancel()
    {
        //triggered by cancelButton press
        gameObject.SetActive(false);
    }

}
