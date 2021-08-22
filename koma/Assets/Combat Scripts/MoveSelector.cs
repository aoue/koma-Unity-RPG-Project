using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSelector : MonoBehaviour
{
    //the move selection interface

    [SerializeField] private CombatManager theBoss;
    [SerializeField] private Highlighting highlighter;

    [SerializeField] private Image[] chevronSpots;
    [SerializeField] private Sprite upChevronImage;
    [SerializeField] private Sprite downChevronImage;

    [SerializeField] private Button[] moveButtons;
    [SerializeField] private Button[] swapButtons;
    [SerializeField] private Button defendMoveButton;

    [SerializeField] private Text nameText;
    [SerializeField] private Text statBlockText;
    [SerializeField] private Text moveHighlightText;

    private string[] affStringArray = new string[6]
    { "earth", "wind", "water", "flame", "light", "dark" };

    public void show(Unit unit, bool isEORempty, int indexInPl)
    {
        moveHighlightText.text = "";
        //goes and sets up the move interface with information read from unit.

        //fill in unit breakdown:
        //unit name, AP / APmax
        nameText.text = unit.get_nom() + " Lv " + unit.get_level();

        //stats (in columns, probably). patk, pdef, matk, mdef, aff
        statBlockText.text = "Patk: " + unit.get_patk_actual()
            + "\nPdef: " + unit.get_pdef_actual()
            + "\nMatk: " + unit.get_matk_actual()
            + "\nMdef: " + unit.get_mdef_actual();

        //fill in swap buttons:
        //there are 4: forward, back, left, right.
        //validate them:
        // valid: there is a non-ooa unit in the slot you want to swap with

        //forward swap button [0]    
        if ( indexInPl > 2 && theBoss.get_pl()[indexInPl - 3] != null && theBoss.get_pl()[indexInPl - 3].get_ooa() == false)
        {
            swapButtons[0].interactable = true;
        }
        else
        {
            swapButtons[0].interactable = false;
        }

        //back swap button [1]
        if (indexInPl < 3 && theBoss.get_pl()[indexInPl + 3] != null && theBoss.get_pl()[indexInPl + 3].get_ooa() == false)
        {
            swapButtons[1].interactable = true;
        }
        else
        {
            swapButtons[1].interactable = false;
        }

        //left swap button [2]
        if (indexInPl % 3 != 0 && theBoss.get_pl()[indexInPl - 1] != null && theBoss.get_pl()[indexInPl - 1].get_ooa() == false)
        {
            swapButtons[2].interactable = true;
        }
        else
        {
            swapButtons[2].interactable = false;
        }

        //right swap button [2]
        if (indexInPl % 3 != 2 && theBoss.get_pl()[indexInPl + 1] != null && theBoss.get_pl()[indexInPl + 1].get_ooa() == false)
        {
            swapButtons[3].interactable = true;
        }
        else
        {
            swapButtons[3].interactable = false;
        }

        //defend move slot
        defendMoveButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = unit.get_defendMove().get_nom();

        //fill in unit moves:
        //5 move slots + 1 defend slot. colors based on move affinity, row, and availability.
        int mpToSpare = unit.get_mp();
        for (int i = 0; i < 5; i++)
        {
            if ( unit.get_moveset()[i] != null)
            {
                if (mpToSpare >= (int)(unit.get_moveset()[i].get_mpDrain() * unit.status.trance) && unit.get_ap() >= unit.get_moveset()[i].get_apDrain())
                {
                    if (unit.get_moveset()[i].get_phase() != executionTime.ENDOFROUND)
                    {
                        moveButtons[i].interactable = true;                     
                    }
                    else if (unit.get_moveset()[i].get_phase() == executionTime.ENDOFROUND && isEORempty == true)
                    {
                        moveButtons[i].interactable = true;
                    }
                    else
                    {
                        moveButtons[i].interactable = false;
                        
                    }
                }
                moveButtons[i].GetComponent<Image>().color = AffKeyWords.get_aff_color(unit.get_moveset()[i].get_affinity()); //colour based on move's affinity
                moveButtons[i].transform.GetChild(1).gameObject.GetComponent<Text>().text = unit.get_moveset()[i].get_nom();

                //if not interactable, then set alpha to 0.6 to show this visibly.
                if (moveButtons[i].interactable == false)
                {
                    Color tmp = moveButtons[i].GetComponent<Image>().color;
                    tmp.a = 0.3f;
                    moveButtons[i].GetComponent<Image>().color = tmp;
                }

                //chevron
                if ( unit.get_moveset()[i].get_preferredRow() == preferredRow.FRONT ) chevronSpots[i].sprite = upChevronImage;
                else if (unit.get_moveset()[i].get_preferredRow() == preferredRow.BACK) chevronSpots[i].sprite = downChevronImage;
                else chevronSpots[i].gameObject.SetActive(false);
                if ( unit.get_moveset()[i].get_preferredRow() != preferredRow.AMBI ) chevronSpots[i].gameObject.SetActive(true);
            }
            else
            {
                chevronSpots[i].gameObject.SetActive(false);
                moveButtons[i].interactable = false;
                moveButtons[i].GetComponent<Image>().color = AffKeyWords.get_aff_color(-1); //null moves get gray
                moveButtons[i].transform.GetChild(1).gameObject.GetComponent<Text>().text = "";
            }
        }

        gameObject.SetActive(true);
    }

    public void highlight_moveButton(int which)
    {
        //which: 0-4. 5=defend move
        //called when one of the move buttons is highlighted, and displays move information.
        if (theBoss.currentUnit != null && which == 5)
        {
            moveHighlightText.text = theBoss.currentUnit.get_defendMove().generate_defend_string();
        }
        else if (theBoss.currentUnit != null && theBoss.currentUnit.get_moveset()[which] != null)
        {
            moveHighlightText.text = theBoss.currentUnit.get_moveset()[which].generate_preview_text();
            highlighter.move_hovered(theBoss.currentUnit.get_moveset()[which].get_affinity(), theBoss.currentUnit.get_moveset()[which].get_isHeal());
        }

    }
    public void unhover_moveButton()
    {
        //clears the affmult text from highlighters.
        highlighter.move_unhovered();
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }

}
