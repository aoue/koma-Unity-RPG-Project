using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AffKeyWords
{
    //holds all the aff stuff in a central place, so we don't have to maintain copies all over.

    //mults (don't forget to change the real values in battle brain too.)
    public static string[,] affMultTextArray = new string[7, 7]
    {
        {"x1.0", "x0.5", "x1.5", "x1.0", "x1.0", "x1.0", "x1.0" },
        {"x1.5", "x1.0", "x1.0", "x0.5", "x1.0", "x1.0", "x1.0" },
        {"x0.5", "x1.0", "x1.0", "x1.5", "x1.0", "x1.0", "x1.0" },
        {"x1.0", "x1.5", "x0.5", "x1.0", "x1.0", "x1.0", "x1.0" },
        {"x1.0", "x1.0", "x1.0", "x1.0", "x0.5", "x2.0", "x1.0" },
        {"x1.0", "x1.0", "x1.0", "x1.0", "x2.0", "x0.5", "x1.0" },
        {"x1.0", "x1.0", "x1.0", "x1.0", "x1.0", "x1.0", "x1.0" },
    };
    
    //coloring
    public static Color get_aff_color(int index)
    {       
        //returns color associated with aff and alpha set to 1.
        switch (index)
        {
            case 0: //earf
                return new Color(84f / 255f, 51f / 255f, 3f / 255f, 1f);
            case 1: //wind
                return new Color(85f / 255f, 224f / 255f, 122f / 255f, 1f);
            case 2: //water
                return new Color(63f / 255f, 160f / 255f, 235f / 255f, 1f);
            case 3: //flame
                return new Color(227f / 255f, 33f / 255f, 13f / 255f, 1f);
            case 4: //light
                return new Color(224f / 255f, 224f / 255f, 29f / 255f, 1f);
            case 5: //dark
                return new Color(83f / 255f, 25f / 255f, 125f / 255f, 1f);
            case 6: //normal
                return new Color(200f / 255f, 200f / 255f, 200f / 255f, 1f);
            default: //null
                return new Color(1f, 1f, 1f, 1f);
        }       
    }

    //aff names
    public static string get_affName(int index)
    {
        switch (index)
        {
            case 0: //earf
                return "earf";
            case 1: //wind
                return "wind";
            case 2: //water
                return "water";
            case 3: //flame
                return "flame";
            case 4: //light
                return "light";
            case 5: //dark
                return "dark";
            case 6: //normal
                return "normal";
            default: //null
                return "null";
        }
    }

}
