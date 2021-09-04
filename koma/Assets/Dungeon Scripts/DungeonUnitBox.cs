using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUnitBox : MonoBehaviour
{
    [SerializeField] private int boxID; // 0 - 5

    [SerializeField] private Image unitImage; //dimensions are 500x250. 2:1
    [SerializeField] private Image affOrb; //we change the color depending on the unit's aff.
    [SerializeField] private Text nameText;
    [SerializeField] private Text apText;
    [SerializeField] private Text hpText;

    public int get_boxID() { return boxID; }

    public void fill_unit(Unit u, Sprite affOrbSprite, bool showAP = true)
    {
        //takes in a playerunit and reads it to fill the unitbox
        //fills:
        // -image
        unitImage.sprite = u.get_boxImg();
        // -name
        nameText.text = u.get_nom();
        // -AP

        // -stat block col 1 (HP and MP)
        if (u.status.hp == 0) hpText.text = "HP: " + u.get_hp() + " / " + u.get_hpMax();
        else if (u.status.hp > 0) hpText.text = "HP: " + u.get_hp() + " / <color=green>" + u.get_hpMax_actual() + "</color>";
        else hpText.text = "HP: " + u.get_hp() + " / <color=red>" + u.get_hpMax_actual() + "</color>";

        //if we're dealing with a player unit, then show mp / mpmax too.
        if (!(u is Enemy))
        {
            hpText.text += "\nMP: " + u.get_mp() + " / " + u.get_mpMax();
        }

        //if we're in battle, then show break percentage too.
        if (showAP == true)
        {
            apText.text = "AP: " + u.get_ap();
            hpText.text += "\n<color=yellow>(" + u.get_break() + "%)</color>";

        }


        

        //fill in their orb slot too.
        //affOrb.color = AffKeyWords.get_aff_color(u.get_affinity());

        affOrb.sprite = affOrbSprite;
        affOrb.gameObject.SetActive(true);

        live_unit();
        gameObject.SetActive(true);
    }
    public void fill_empty()
    {
        gameObject.SetActive(false);               
    }
    public void fill_dummy(Sprite sp)
    {
        //used in combat Manager.
        //we fill it with a blank image.
        nameText.text = "";
        apText.text = "";
        hpText.text = "";
        affOrb.gameObject.SetActive(false);
        unitImage.sprite = sp;
        live_unit();
    }

    public void kill_unit()
    {
        //sets alpha low
        Color c = unitImage.color;
        c.a = 0.5f;
        unitImage.color = c;
        affOrb.gameObject.SetActive(false);
    }
    public void live_unit()
    {
        //set alpha back to normal
        Color c = unitImage.color;
        c.a = 1.0f;
        unitImage.color = c;
    }

}
