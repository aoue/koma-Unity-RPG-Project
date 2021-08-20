using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExpManager
{
    //manages exp gaining/spending, and the cap -- things like that.

    //if you gain xp but are already at capXP, then you gain nothing.

    public static int currentXP { get; set; } //the current amount of xp. never decreases.
    public static int distroXP { get; set; } //the amount gained during a dungeon. increased alongside currentXP. distributed to each unit upon return to overworld.
    public static int capXP { get; set; } //the cap. currentXP cannot go over this.

    public static void setup()
    {
        currentXP = 0;
        distroXP = 0;
        capXP = 1000;
    }

    public static void add_exp(int toAdd)
    {
        int difference = capXP - currentXP;

        if (toAdd > difference) toAdd = difference;

        distroXP = toAdd;
        Debug.Log("adding " + toAdd + "exp to party.");
    }

    public static void distribute_xp(List<Unit> party)
    {
        Debug.Log("distributing xp: " + distroXP);
        foreach(Unit p in party)
        {
            p.inc_exp(distroXP);
        }
    }



}


