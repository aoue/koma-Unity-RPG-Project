using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitLibrary : MonoBehaviour
{
    //the portrait library.
    //holds all the portraits in the game, all accessible with an int.
    // CHAR INDEX LEGEND:
    // 0: yve
    // 1: friday
    // 2: mueler
    // 3: maddy
    //etc

    // EXPRESSION INDEX LEGEND:
    // 0: neutral
    // 1: 
    // 2: 
    //etc
    [SerializeField] private Sprite[] eventBgs;
    [SerializeField] private Sprite[] boxSprites;
    [SerializeField] private Sprite[] yve;
    [SerializeField] private Sprite[] friday;
    [SerializeField] private Sprite[] mueler;
    [SerializeField] private Sprite[] maddy;

    public Sprite retrieve_eventBg(int id)
    {
        return eventBgs[id];
    }

    public Sprite retrieve_boxp(int index)
    {
        //used for event previews. nothing major.
        //only a handful of box sprites, anyway.
        //returns according to char index legend.
        return boxSprites[index];
    }


    public Sprite retrieve_fullp(int index)
    {
        //the way this one works is a bit more complicated.
        //all characters have multiple full portraits, which may change during the dialogue.

        //the index passed in is subdivided into 2:
        int char_index = index / 100; //e.g. 502 -> 5
        int expression_index = index % 100; //e.g. 502 -> 2

        //now, char_index is used to select the array we want.
        //then, expression_index is used to select an element (which is a sprite) from the array.
        switch (char_index)
        {
            case 0: //yve
                return yve[expression_index];
            case 1: //friday
                return friday[expression_index];
            case 2: //mueler
                return mueler[expression_index];
            case 3: //maddy
                return maddy[expression_index];

            //etc.
        }

        return null;
    }
}
