using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewSlot : MonoBehaviour
{
    //for the preview slot. is used to show a whole bunch of stuff when things are hovered. versatile.

    [SerializeField] private Highlighting highlighter;
    [SerializeField] private CombatManager theBoss;

    [SerializeField] private Text previewText;
    [SerializeField] private Text previewTextCol2;

    public void highlight_unitSlot(int which)
    {
        //called when unitSlot is highlighted.
        //shows information in previewSlot about the unit in the slot.
        //PLAYER SELECTING A UNIT TO ORDER
        if (theBoss.get_pTurn() == playerTurnPhase.SELECTUNIT)
        {
            //only highlighht unit IF: unit's ap > 0 && unit.ooa == false
            if (theBoss.get_pl()[which] != null)
            {               
                theBoss.show_active_portrait(theBoss.get_pl()[which].get_activePortrait());

                //if (theBoss.get_pl()[which].get_ap() > 0 && theBoss.get_pl()[which].get_ooa() == false)                
                    highlighter.highlight_user(which);
            }
        }
        //PLAYER SELECTING A TARGET TILE (on the player side of the map)
        else if ( theBoss.get_pTurn() == playerTurnPhase.SELECTTARGET && theBoss.currentUnit != null && theBoss.currentUnit.nextMove != null && theBoss.currentUnit.nextMove.get_isHeal() == true)
        {
            //only highlight if move.musttargetself == false OR musttargetself is true and which == currentunit.place
            if ( theBoss.currentUnit.nextMove.get_mustTargetSelf() == false || (theBoss.currentUnit.nextMove.get_mustTargetSelf() == true && theBoss.currentUnit.place == which) )
                highlighter.highlight_party(which, theBoss.currentUnit.nextMove, true);
        }

        //retrieves information about the unit through theBoss.
        if (theBoss.get_pl()[which] == null || theBoss.get_pl()[which].get_ooa() == true) return;

        //cut off after certain characters and add a "." instead. because some names are too long.
        string nameString = theBoss.get_pl()[which].get_nom();
        if (nameString.Length > 13)
        {
            nameString = nameString.Substring(0, 12);
            nameString += ".";
        }

        previewText.text = nameString + " Lv " + theBoss.get_pl()[which].get_level() + "\nPatk: " + theBoss.get_pl()[which].get_patk() + " (x" + theBoss.get_pl()[which].status.patk + ")"
            + "\nPdef: " + theBoss.get_pl()[which].get_pdef() + " (x" + theBoss.get_pl()[which].status.pdef + ")"
            + "\n" + theBoss.get_pl()[which].status.generate_string_left();


        previewTextCol2.text = "Affinity: " + AffKeyWords.get_affName(theBoss.get_pl()[which].get_affinity());
        previewTextCol2.text += "\nMatk: " + theBoss.get_pl()[which].get_matk() + " (x" + theBoss.get_pl()[which].status.matk + ")"
            + "\nMdef: " + theBoss.get_pl()[which].get_mdef() + " (x" + theBoss.get_pl()[which].status.mdef + ")"
            + "\n" + theBoss.get_pl()[which].status.generate_string_right();

        previewTextCol2.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
    public void highlight_enemySlot(int which)
    {
        //player mousing over an enemy
        if (theBoss.get_pTurn() == playerTurnPhase.SELECTUNIT)
        {
            if (theBoss.get_el()[which] != null)
                theBoss.show_active_portrait(theBoss.get_el()[which].get_activePortrait());           
        }
        //PLAYER SELECTING A TARGET TILE (on the enemy side of the map)
        else if (theBoss.get_pTurn() == playerTurnPhase.SELECTTARGET && theBoss.currentUnit != null && theBoss.currentUnit.nextMove != null && theBoss.currentUnit.nextMove.get_isHeal() == false)
        {
            if (theBoss.currentUnit.nextMove.get_mustTargetSelf() == false)
                highlighter.highlight_enemy(which, theBoss.currentUnit.nextMove, true);
        }

        if (theBoss.get_el()[which] == null) return;

        //highlighted an enemy slot.
        //cut off after certain characters and add a "." instead. because some names are too long.
        string nameString = theBoss.get_el()[which].get_nom();
        if (nameString.Length > 13)
        {
            nameString = nameString.Substring(0, 12);
            nameString += ".";
        }

        previewText.text = nameString + " Lv " + theBoss.get_el()[which].get_level()
            + "\nPatk: " + theBoss.get_el()[which].get_patk_actual() + " (x" + theBoss.get_el()[which].status.patk + ")"
            + "\nPdef: " + theBoss.get_el()[which].get_pdef_actual() + " (x" + theBoss.get_el()[which].status.pdef + ")"
            + "\n" + theBoss.get_el()[which].status.generate_string_left();

        previewTextCol2.text = "Affinity: " + AffKeyWords.get_affName(theBoss.get_el()[which].get_affinity());
        previewTextCol2.text += "\nMatk: " + theBoss.get_el()[which].get_matk_actual() + " (x" + theBoss.get_el()[which].status.matk + ")"
            + "\nMdef: " + theBoss.get_el()[which].get_mdef_actual() + " (x" + theBoss.get_el()[which].status.mdef + ")"
            + "\n" + theBoss.get_el()[which].status.generate_string_right();

        previewTextCol2.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
    public void hide(int which)
    {
        previewTextCol2.gameObject.SetActive(false);
        gameObject.SetActive(false);

        if (which == -1) return;

        //which: 0-12.
        if (theBoss.get_pTurn() == playerTurnPhase.SELECTUNIT && which < 6)
        {
            highlighter.unhighlight_user(which);
        }
        else if (theBoss.get_pTurn() == playerTurnPhase.SELECTTARGET)
        {
            theBoss.unhighlight_move(which);
        }
        
    }

    //PREVIEW MOVES
    public void highlight_previewMove(int which)
    {
        //called when one of the moves held in preview is highlighted.
        //shows information about highlighted move.
        //0: player's scheduled move
        //1: end of round scheduled move
        //2: enemy's scheduled move
        gameObject.SetActive(false);
        switch (which)
        {
            case 0:
                if ( theBoss.playerScheduledMove != null)
                {
                    previewText.text = theBoss.playerScheduledMove.generate_preview_text();
                    gameObject.SetActive(true);
                }                   
                break;
            case 1:
                if (theBoss.eorScheduledMove != null)
                {
                    previewText.text = theBoss.eorScheduledMove.generate_preview_text();
                    gameObject.SetActive(true);
                }                    
                break;
            case 2:
                if (theBoss.enemyScheduledMove != null)
                {
                    previewText.text = theBoss.enemyScheduledMove.generate_preview_text();
                    gameObject.SetActive(true);
                }                   
                break;
        }
        theBoss.highlight_preview_move(which);
    }
    public void unhighlight_previewMove(int which)
    {
        //called when one of the moves held in preview is pointerExit-ed
        //shows information about highlighted move.
        //0: player's scheduled move
        //1: end of round scheduled move
        //2: enemy's scheduled move
        theBoss.unhighlight_preview_move(which);
        gameObject.SetActive(false);
    }


}
