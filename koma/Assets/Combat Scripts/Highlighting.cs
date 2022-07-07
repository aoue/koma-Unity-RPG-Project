using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlighting : MonoBehaviour
{
    //handles tile highlighting for combat.
    //highlights tiles regardless of if there is a unit on them.
    //NOTE: images start alpha.

    //two lists, 0-5, of each image which works as the tile highlight for the combat interface.

    [SerializeField] private CombatManager theBoss;

    //the highlights start with alpha = 0.
    //the multTexts start inactive.

    [SerializeField] private Image[] partyHighlights;
    [SerializeField] private Text[] partyMultTexts;

    [SerializeField] private Image[] enemyHighlights;
    [SerializeField] private Text[] enemyMultTexts;
 

    //move selector calls these for aff hovers:
    public void move_hovered(int affVal, bool targetsParty)
    {
        //for each non null unit box, show aff text.
        //if move is a heal, don't show a heal multiplier (because heals are unaffected by affinity.)
        if (targetsParty == false)
        {
            for (int i = 0; i < theBoss.get_el().Length; i++)
            {
                if (theBoss.get_el()[i] != null)
                {
                    enemyMultTexts[i].text = AffKeyWords.affMultTextArray[affVal, theBoss.get_el()[i].get_affinity()];
                    enemyMultTexts[i].gameObject.SetActive(true);
                }
            }
        }
    }
    public void move_unhovered()
    {
        for (int i = 0; i < theBoss.get_pl().Length; i++)
        {
            if (theBoss.get_pl()[i] != null)
            {
                partyMultTexts[i].gameObject.SetActive(false);
                
            }
            if (theBoss.get_el()[i] != null)
            {
                enemyMultTexts[i].gameObject.SetActive(false);
            }
        }
    }

    //box hovers
    public void highlight_user(int which)
    {
        if (theBoss.get_pl()[which] == null || theBoss.get_pl()[which].get_ooa() == true) return;

        //CAVEAT:
        //if spot already green, then make it cyan.
        Color tmp = partyHighlights[which].color;
        tmp.a = 255f;
        if (tmp == Color.green)
        {
            tmp = Color.cyan;
        }
        else
        {
            tmp = Color.blue;
        }
        
        partyHighlights[which].color = tmp;
    }
    public void highlight_e_user(int which)
    {
        //CAVEAT:
        //if spot already green, then make it cyan.       
        Color tmp = enemyHighlights[which].color;
        tmp.a = 255f;
        if (tmp == Color.green)
        {
            tmp = Color.cyan;
        }
        else
        {
            tmp = Color.blue;
        }
        enemyHighlights[which].color = tmp;
    }
    public void unhighlight_user(int which, bool ignoreCondition=false)
    {
        if ( ignoreCondition == false && theBoss.get_pTurn() != playerTurnPhase.SELECTUNIT) return;
        
        Color tmp = partyHighlights[which].color;
        tmp.a = 0f;
        partyHighlights[which].color = tmp;      
    }
    public void unhighlight_e_user(int which, bool ignoreCondition = false)
    {
        if (ignoreCondition == false && theBoss.get_pTurn() != playerTurnPhase.SELECTUNIT) return;

        Color tmp = enemyHighlights[which].color;
        tmp.a = 0f;
        enemyHighlights[which].color = tmp;
    }

    private bool is_spot_occupied(bool isPlayerSide, int which)
    {
        if (isPlayerSide)
        {
            if (theBoss.get_pl()[which] != null) return true;
        }
        else
        {
            if (theBoss.get_el()[which] != null) return true;
        }
        return false;
    }
    public void highlight_enemy(int spot, Move move, bool userIsParty, int userSpot=-1)
    {
        //which tiles are highlighed are controller by which, xSize, and ySize.
        //which colour is controlled by isHeal. (true: green, false: red)

        //if (is_spot_occupied(false, ))

        //color setup
        //color setup
        Color tmp;
        tmp.a = 255f;
        if (move.get_isHeal())
        {
            tmp = Color.green;
        }
        else
        {
            tmp = Color.red;
        }

        int helper = 1;
        if (spot > 2) helper = -1;
        if (spot % 3 == 2 && move.get_xSize() > 1) spot--;

        for (int y = 0; y < move.get_ySize(); y++)
        {
            //do rows by adding 3*y to all calculations.
            switch (move.get_xSize())
            {
                case 1:
                    if (is_spot_occupied(false, spot + (helper * y * 3))) enemyHighlights[spot + (helper * y * 3)].color = tmp;
                    break;

                case 2:
                    //can just check the middle row. nice little cheat.
                    if (is_spot_occupied(false, spot + (helper * y * 3))) enemyHighlights[spot + (helper * y * 3)].color = tmp;
                    if (is_spot_occupied(false, spot + 1 + (helper * y * 3))) enemyHighlights[spot + 1 + (helper * y * 3)].color = tmp;
                    break;

                case 3:
                    //can check any old spot.
                    if (spot % 3 == 0)
                    {
                        if (is_spot_occupied(false, spot + 2 + (helper * y * 3))) enemyHighlights[spot + 2 + (helper * y * 3)].color = tmp;
                    }
                    else
                    {
                        if (is_spot_occupied(false, spot - 1 + (helper * y * 3))) enemyHighlights[spot - 1 + (helper * y * 3)].color = tmp;
                    }

                    if (is_spot_occupied(false, spot + (helper * y * 3))) enemyHighlights[spot + (helper * y * 3)].color = tmp;
                    if (is_spot_occupied(false, spot + 1 + (helper * y * 3))) enemyHighlights[spot + 1 + (helper * y * 3)].color = tmp;
                    break;
            }
        }
        //aff mults setup
        //if move is a heal, don't show mults because they are always x1.
        if (move.get_isHeal() == false)
        {
            for (int i = 0; i < partyHighlights.Length; i++)
            {
                if (enemyHighlights[i].color.a == 1.0f && theBoss.get_el()[i] != null)
                {
                    //if it's an affected tile, then show the aff stuff.
                    enemyMultTexts[i].text = AffKeyWords.affMultTextArray[move.get_affinity(), theBoss.get_el()[i].get_affinity()]; enemyMultTexts[i].gameObject.SetActive(true);
                }
            }
        }
        

        //finally, highlight the move's user. (blend colours if unit is also affected by move)
        //if userSpot == -1, then don't.
        if (userSpot != -1)
        {
            if (userIsParty == true)
            {
                highlight_user(userSpot);
            }
            else
            {
                highlight_e_user(userSpot);
            }
        }
    }
    public void highlight_party(int spot, Move move, bool userIsParty, int userSpot=-1)
    {
        //which tiles are highlighed are controller by which, xSize, and ySize.
        //which colour is controlled by isHeal. (true: green, false: red)

        //if (is_spot_occupied(true, ))

        //color setup
        Color tmp;
        tmp.a = 255f;
        if (move.get_isHeal())
        {
            tmp = Color.green;
        }
        else
        {
            tmp = Color.red;
        }

        int helper = 1;
        if (spot > 2) helper = -1;
        if (spot % 3 == 2 && move.get_xSize() > 1) spot--;

        for (int y = 0; y < move.get_ySize(); y++)
        {
            //do rows by adding 3*y to all calculations.
            switch (move.get_xSize())
            {
                case 1:
                    if (is_spot_occupied(true, spot + (helper * y * 3))) partyHighlights[spot + (helper * y * 3)].color = tmp;
                    break;

                case 2:
                    //can just check the middle row. nice little cheat.
                    if (is_spot_occupied(true, spot + (helper * y * 3))) partyHighlights[spot + (helper * y * 3)].color = tmp;
                    if (is_spot_occupied(true, spot + 1 + (helper * y * 3))) partyHighlights[spot + 1 + (helper * y * 3)].color = tmp;
                    break;

                case 3:
                    //can check any old spot.
                    if (spot % 3 == 0)
                    {
                        if (is_spot_occupied(true, spot + 2 + (helper * y * 3))) partyHighlights[spot + 2 + (helper * y * 3)].color = tmp;
                    }
                    else
                    {
                        if (is_spot_occupied(true, spot - 1 + (helper * y * 3))) partyHighlights[spot - 1 + (helper * y * 3)].color = tmp;
                    }
                    if (is_spot_occupied(true, spot + (helper * y * 3))) partyHighlights[spot + (helper * y * 3)].color = tmp;
                    if (is_spot_occupied(true, spot + 1 + (helper * y * 3))) partyHighlights[spot + 1 + (helper * y * 3)].color = tmp;
                    break;
            }
        }

        //aff mults setup
        if (move.get_isHeal() == false)
        {
            for (int i = 0; i < partyHighlights.Length; i++)
            {
                if (partyHighlights[i].color.a == 1 && theBoss.get_pl()[i] != null)
                {
                    //if it's an affected tile, then show the aff stuff.
                    partyMultTexts[i].text = AffKeyWords.affMultTextArray[move.get_affinity(), theBoss.get_pl()[i].get_affinity()];
                    partyMultTexts[i].gameObject.SetActive(true);
                }
            }
        }
        

        //finally, highlight the move's user. (blend colours if unit is also affected by move)
        //if userSpot == -1, then don't.
        if ( userSpot != -1)
        {
            if ( userIsParty == true)
            {
                highlight_user(userSpot);
            }
            else
            {
                highlight_e_user(userSpot);
            }
        }
    }

    public void restore_party()
    {
        //resets all the colors for party highlights
        foreach (Image img in partyHighlights)
        {
            Color tmp = img.color;
            tmp.a = 0f;
            img.color = tmp;
        }
        //turn off multtext
        foreach (Text t in partyMultTexts)
        {
            t.gameObject.SetActive(false);
        }

    }
    public void restore_enemies()
    {
        //resets all the colors for enemy highlights
        foreach (Image img in enemyHighlights)
        {
            Color tmp = img.color;
            tmp.a = 0f;
            img.color = tmp;
        }
        //turn off multtext
        foreach (Text t in enemyMultTexts)
        {
            t.gameObject.SetActive(false);
        }

    }

}
