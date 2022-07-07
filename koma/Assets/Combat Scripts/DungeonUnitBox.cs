using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUnitBox : MonoBehaviour
{
    [SerializeField] private int boxID; // 0 - 5

    private Image unitImage; //dimensions are 500x250. 2:1
    private Image affOrb; //we change the image depending on the unit's aff.
    private Text nameText;
    private Text apText;
    private Text hpText;
    bool doneSetup = false;

    public int get_boxID() { return boxID; }

    void setup()
    {
        if (doneSetup) return;
        //Debug.Log("dungeon unit box awake!");
        doneSetup = true;
        unitImage = gameObject.GetComponent<Image>();

        nameText = gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        apText = gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
        hpText = gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();
        affOrb = gameObject.transform.GetChild(4).gameObject.GetComponent<Image>();
    }

    public void fill_unit(Unit u, Sprite affOrbSprite, bool showAP = true)
    {
        setup();
        //takes in a playerunit and reads it to fill the unitbox
        //fills:
        // -image
        unitImage.sprite = u.get_boxImg();
        // -name
        nameText.text = u.get_nom();
        // -AP

        // -stat block col 1 (HP and MP)
        if (u.status.hp == 0) hpText.text = u.get_hp() + "/" + u.get_hpMax() + " HP";
        else if (u.status.hp > 0) hpText.text = u.get_hp() + "/<color=green>" + u.get_hpMax_actual() + " HP</color>";
        else hpText.text = u.get_hp() + "/<color=red>" + u.get_hpMax_actual() + " HP</color>";

        //show power/mp. Since max is 100 for all units, no need to show max here.
        hpText.text += "\n" + u.get_mp() + " PW";
        
        //if we're in battle, then show break percentage too.
        if (showAP == true)
        {
            apText.text = u.get_ap() + " AP";
            hpText.text += "\n<color=yellow>(" + u.get_break() + "%)</color>";
        }

        //fill in their orb slot too.
        affOrb.sprite = affOrbSprite;
        affOrb.gameObject.SetActive(true);

        live_unit();
        gameObject.SetActive(true);
    }
    public void hide_stats()
    {
        //used to hide stats at the start of the battle, as they are updated
        affOrb.enabled = false;
        nameText.enabled = false;
        apText.enabled = false;
        hpText.enabled = false;
    }
    public void show_stats()
    {
        //used to show stats once the battle has started
        affOrb.enabled = true;
        nameText.enabled = true;
        apText.enabled = true;
        hpText.enabled = true;
    }


    public void fill_empty()
    {
        gameObject.SetActive(false);               
    }
    public void fill_dummy(Sprite sp)
    {
        setup();
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
