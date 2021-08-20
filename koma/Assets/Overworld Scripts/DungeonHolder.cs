using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DungeonHolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //A DungeonHolder object holds a dungeon. It is the link from the overworld
    //to a particular dungeon, and controls loading the dungeon from where it is.
    //by convention, they start hidden.

    [SerializeField] private Dungeon dun;
    [SerializeField] private Button buttonIcon;

    //has an icon:
    // -when the icon is clicked, it will show the prep-dungeon menu for this particular dungeon.

    public void set_dungeon(Dungeon d) { dun = d; }
    public Dungeon get_dungeon() { return dun; }

    public void dungeon_decay()
    {
        //called everyday, from overworld's a_new_day()
        //decrements the dungeon's threat by its threatDecay
        if (dun.threat > 0) dun.threat -= System.Math.Max(0, dun.get_threatDecay());

    }

    public void disable_dungeon()
    {
        //sets dungeon to uninteractable
        buttonIcon.interactable = false;
    }
    public void enable_dungeon(bool show, bool interactable)
    {
        //sets gameobject to active
        //and sets dungeon to interactable
        if (show)
        {
            gameObject.SetActive(true);
        }
        
        buttonIcon.interactable = interactable;

    }

    public void open_prepDungeon_menu()
    {
        //called on buttonIcon click.
        //calls the prepDungeon menu manager with 'dun' as an argument
        Overworld.open_dungeonPrepMenu(dun);
    }

    //detect when mouse enters and exits the event icon
    public void OnPointerEnter(PointerEventData eventData)
    {
        //called on pointer enter. tells eventmanager and asks
        //it to show the event preview (which it does, obviously).
        EventManager.dungeon_hovered(dun, gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //called on pointer exit. tells eventmanager to stop showing the preview.
        EventManager.event_unhovered();
    }

}
