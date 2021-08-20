using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuple
{
    //my own tuple class.
    //take that bourgeoisie.

    public Tuple(int inX, int inY)
    {
        x = inX;
        y = inY;
    }

    public void reset()
    {
        g = 0;
        h = 0;
        f = 0;
    }

    public int x { get; set; }
    public int y { get; set; }

    //pathfinding
    public bool blocked { get; set; }
    public Tuple prev { get; set; }
    public int g { get; set; }
    public int h { get; set; }
    public int f { get; set; }
     
}
