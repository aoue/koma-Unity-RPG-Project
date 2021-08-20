using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{   
    //before an event:
    //- the notifier shows text: '-EVENT-\n[eventname]'
    //before a collision:
    //- the notifier shows text: '-A [hostile/neutral] PARTY APPROACHES-'
    //then the notifier shows buttons to allow the player to proceed: 
    //'proceed', 'talk' (<-- only for parties), 'trade' (<-- only for neutral parties)

    
    [SerializeField] private Text dropDownText;
    [SerializeField] private Button proceedButton;
    [SerializeField] private Button talkButton;
    [SerializeField] private Button tradeButton;

    //returns control to dungeon manager on dismiss button.
    [SerializeField] private DungeonManager dMan;

    //dropdown capabilities
    public void show_dropDown(string h, bool proceedVal, bool talkVal, bool tradeVal)
    {
        //animate the dropDownMenu to come down from the top of the screen.

        dropDownText.text = "- Encounter -\n" + h;
        proceedButton.interactable = proceedVal;
        talkButton.interactable = talkVal;
        tradeButton.interactable = tradeVal;
        gameObject.SetActive(true);
    }
    public void dismiss_dropDown(int whichButtonPressed)
    {
        //0=proceed, 1=talk, 2=trade.
        //hides itself
        gameObject.SetActive(false);

        switch (whichButtonPressed)
        {
            case 0:
                dMan.encounter_proceed(); //does something different depending on current dungeon state.
                break;
            case 1:

                break;
            case 2:

                break;
        }


        //calls the appropriate function from dMan.
    }
}
