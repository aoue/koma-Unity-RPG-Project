using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonNotifier : Notifier
{
    //notifier, but in the dungeon.
    //subtly different.
    [SerializeField] private DungeonManager dMan;
    //old notifier capabilities - adapted
    public override void show_note(string titleNote, string bodyNote, string summaryNote)
    {
        //three parts:
        // - title note, goes at the top. the event's name.
        // - body note, it's the flavour text.
        // - summary note, it's the tldr; what this really means.

        set_title(titleNote);
        set_body(bodyNote);
        set_sum(summaryNote);
        get_mainObj().SetActive(true);
    }
    public override void dismiss()
    {
        //dismisses the notification.
        get_mainObj().SetActive(false);
        get_diaCanvas().SetActive(false);

        dMan.return_control(0);
    }


    

}
