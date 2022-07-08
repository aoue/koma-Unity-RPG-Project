using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReserveUnitBox : MonoBehaviour
{
    //like a unit box, but less detailed.
    //has a 90x90 image slot for the unit's box portrait 
    //has a textbox for the unit's name above that
    //has an int id. (that is +6 it's actual position)

    //[SerializeField] private Text nameText;
    //[SerializeField] private Text stamCostText;
    [SerializeField] private Image boxImage;
    [SerializeField] private int id;
    public bool canDrag { get; set; } //is allowed to drag, really.

    public int get_id() { return id; }


    public void fill_unit(Unit u)
    {
        //boxImage.sprite = u.get_boxImg();
        boxImage.sprite = u.get_activePortrait();
        //nameText.text = u.get_nom();
        //stamCostText.text = "MP: " + u.get_mp() + "/" + u.get_mpMax();

        //set the image to partially alpha if canDrag = false as a visual cue to the player.
        Color c = boxImage.color;
        if (u.inParty)
        {
            canDrag = false;
            c.a = 0.6f;
        }
        else
        {
            canDrag = true;           
            c.a = 1f;          
        }
        boxImage.color = c;
        gameObject.SetActive(true);
    }

}
