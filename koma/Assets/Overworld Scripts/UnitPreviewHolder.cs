using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitPreviewHolder : MonoBehaviour
{
    //unit preview holder - holds a preview of hovered units in prep dungeon menu.

    //has got:
    // -name text
    // -stamina deploy cost text
    // -ap text
    // -5 stats text (patk, pdef, matk, mdef, aff)
    // -5 move slots (not interactable)
    // -1 defend move slots (not interactable)
    // -move highlight portion. lets us see what the info of a move when it is highlighted.

    [SerializeField] private PrepDungeonManager prep;

    [SerializeField] private Image[] moveBgs;
    [SerializeField] private Text[] moveText;
    [SerializeField] private Text defendMoveText;

    [SerializeField] private Text nameText;
    [SerializeField] private Text statBlockText;
    [SerializeField] private Text apText;
    [SerializeField] private Text moveHighlightText;

    private Unit lastUnit = null;

    

    public void show(int which)
    {
        Unit unit = prep.get_unit(which);

        if (unit == null || unit == lastUnit) return;
        lastUnit = unit;
        nameText.text = unit.get_nom() + " Lv " + unit.get_level();
        apText.text = "AP: " + unit.get_apMax();

        moveHighlightText.text = "";

        statBlockText.text = "Patk: " + unit.get_patk()
            + "\nPdef: " + unit.get_pdef()
            + "\nMatk: " + unit.get_matk()
            + "\nMdef: " + unit.get_mdef()
            + "\nAff: " + AffKeyWords.get_affName(unit.get_affinity());

        //defend button
        defendMoveText.text = unit.get_defendMove().get_nom();

        //move buttons
        for(int i = 0; i < moveText.Length; i++)
        {
            if (unit.get_moveset()[i] != null)
            {
                moveText[i].text = unit.get_moveset()[i].get_nom();
                moveBgs[i].color = AffKeyWords.get_aff_color(unit.get_moveset()[i].get_affinity());
            }
            else
            {
                moveText[i].text = "";
                moveBgs[i].color = AffKeyWords.get_aff_color(-1);
            }
        }
        gameObject.SetActive(true);
    }
    public void hover_move(int which)
    {
        //0-4: normal moves.
        //5: defend move.
        //show the highlight for lastUnit's move.
        if (which < 5)
        {
            if (lastUnit.get_moveset()[which] != null)
                moveHighlightText.text = lastUnit.get_moveset()[which].generate_preview_text();                      
        }
        else
        {
            moveHighlightText.text = lastUnit.get_defendMove().generate_defend_string();
        }      
    }

}
