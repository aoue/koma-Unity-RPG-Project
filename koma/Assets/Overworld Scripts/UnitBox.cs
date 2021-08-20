using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBox : MonoBehaviour
{
    //a box that you can put a unit's stats in:
    //can be set to empty, in that case, it just shows the empty image image.
    //has an image for the unit

    //in the future:
    // -show icons if the unit in the box does not have gear equipped in all slots
    // -show icon if the unit does not have moves equipped in all slots

    [SerializeField] private int boxID; // 0 - 5
    [SerializeField] private Image unitImage; //dimensions are 500x250. 2:1
    [SerializeField] private Image affOrb; //we change the color depending on unit's aff.
    [SerializeField] private Text nameText;
    [SerializeField] private Text StaminaText;

    public int get_boxID() { return boxID; }
    
    public void fill_unit(Unit u, Sprite affOrbSprite)
    {
        //takes in a playerunit and reads it to fill the unitbox
        
        u.inParty = true; //record keeping
        affOrb.sprite = affOrbSprite;

        //fills:
        // -image
        unitImage.sprite = u.get_boxImg();
        // -name
        nameText.text = u.get_nom() + " Lv " + u.get_level();

        // -stamina
        if (StaminaText != null) StaminaText.text = "Stamina: " + u.get_stamina() + "/" + u.get_staminaMax();

        //fill orb - deprecated
        //affOrb.color = AffKeyWords.get_aff_color(u.get_affinity());

        gameObject.SetActive(true);
        
    }
    public void fill_empty()
    {

        gameObject.SetActive(false);
       
    }

}
