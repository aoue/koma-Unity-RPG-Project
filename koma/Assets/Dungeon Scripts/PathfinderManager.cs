using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathfinderManager
{
    //does actual pathfinding calculations for ai parties.

    private Tuple[,] grid; //represents the dungeon. true: traversable, false: not traversable.
    private List<Tuple> closedList;
    private List<Tuple> openList;
    private int xSize;
    private int ySize;

    //setting up
    public void setup(int x, int y)
    {
        //fill grid
        xSize = x;
        ySize = y;

        closedList = new List<Tuple>();
        openList = new List<Tuple>();
        grid = new Tuple[xSize, ySize];

        //fill grid
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                grid[i, j] = new Tuple(i, j);
            }
        }
    }
    public void tileValidAt(int x, int y)
    {
        grid[x, y].blocked = false;
    }
    public void tileInvalidAt(int x, int y)
    {
        //null tiles.
        grid[x, y].blocked = true;
    }

    //pathfinding
    void reset()
    {
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                grid[i, j].reset();
            }
        }
    }
    public List<Tuple> find_path(Tuple startPos, Tuple dest)
    {
        //finds a pretty good path between startPos and dest.
        //returns a list; the tiles that should be taken in order from startPos to arrive at dest.

        //the A* algorithm works by repeating the following steps:
        // 1. get the square on the openList with the lowest score, call it S.
        // 2. remove S from the open List and add it to the closedList.
        // 3. for each square T in S's walkable adjacent tiles:
        //      1. if T is in the closed list, ignore it.
        //      2. if T is not in the open list, add it and compute its score.
        //      3. if T is already in the open list, check if the F score is lower when we use the current generated path to get there. 
        //         if it is, update its score and update its parent as well.

        //we start at startPos and add all adjacent valid tiles to open list.
        reset();
        //Debug.Log("ai dest: x" + dest.x + ", y" + dest.y);

        startPos.g = 0;
        closedList.Clear();
        openList.Clear();
        closedList.Add(startPos);
        openList = add_adjacent_tiles(startPos, dest);

        bool reached = false;
        while ( !reached && openList.Count > 0 )
        {
            //take the tile with the lowest f score in openList.

            //... at this point, the 4 starting adjacent tiles are in openList, and if they are equal f value, then 
            /*
            Debug.Log("here are my choices. openlist = ");
            for (int i = 0; i < openList.Count; i++)
            {
                Debug.Log("go to x" + openList[i].x + ", y" + openList[i].y + " where f = " + openList[i].f);
            }
            */

            int minValue = openList.Min(tup => tup.f);
            Tuple current = openList.First(x => x.f == minValue);

            //Debug.Log("I choose tile: x" + current.x + ", y" + current.y + " with f-value = " + current.f);

            //we know we're done when current is dest.
            //any tile with h == 0 will be dest.
            if (current.h == 0)
            {
                reached = true;
            }
            else
            {
                openList.AddRange(add_adjacent_tiles(current, dest)); //concats to openList.
                openList.Remove(current); //we've checked this tile, so we don't need to do so again. 
            }
            closedList.Add(current); //make sure we won't go over the tile again.
        }

        if (reached == false) return null; //returns null if destination is unreachable.

        //now, work backwards to fill path.

        //now, start from the back and add tuples to path until tuple.prev == startpos / g = 1
        //and thats the path.
        //the last element in closedList is the destination.

        List<Tuple> path = new List<Tuple>();
        Tuple cur = closedList[closedList.Count - 1];
        while (cur.g != 0) //i.e. grad whole path except startPos
        {
            path.Add(cur);
            cur = cur.prev;                     
        }
        path.Reverse();
        return path;
    }
    private List<Tuple> add_adjacent_tiles(Tuple tup, Tuple dest)
    {
        //examines adjacent tiles to the passed in tuple and returns them all in a list.
        List<Tuple> neighbours = new List<Tuple>();

        //for each tile, add:
        // -(only valid if tile in grid, not blocked, and not in closedList)
        // -parent, g, and h

        int xOffset = 0;
        int yOffset = 0;

        for(int i = 0; i < 4; i++)
        {
            //order: up, right, down, left
            switch (i)
            {
                case 0:
                    xOffset = 0; yOffset = 1;
                    break;
                case 1:
                    xOffset = 1; yOffset = 0;
                    break;
                case 2:
                    xOffset = 0; yOffset = -1;
                    break;
                case 3:
                    xOffset = -1; yOffset = 0;
                    break;
            }

            //first, check map borders.
            if (i == 0 && tup.y == ySize - 1)
            {
                continue;
            }
            if (i == 1 && tup.x == xSize - 1)
            {
                continue;
            }
            if (i == 2 && tup.y == 0)
            {
                continue;
            }
            if (i == 3 && tup.x == 0)
            {
                continue;
            }
            //good to go if we've made it to here.
            Tuple tmp = grid[tup.x + xOffset, tup.y + yOffset];
            if ( tmp.blocked == false && !closedList.Contains(tmp))
            {
                //if tile in openlist already, then try to update it.
                if ( openList.Contains(tmp) )
                {
                    //if the f score is lower through here, then update it.
                    //if it's equal, then random chance to take it.
                    int new_f = tup.g + 1 + tmp.h;
                    if ( new_f < tmp.f || (new_f == tmp.f && UnityEngine.Random.Range(0, 2) == 1))
                    {
                        tmp.prev = tup;
                        tmp.f = new_f;
                        tmp.g = tup.g + 1;
                        //tmp.h does not change. 
                        grid[tup.x + xOffset, tup.y + yOffset] = tmp;
                        
                    }
                }
                else //else, add it as new.
                {
                    tmp.prev = tup;
                    tmp.g = tup.g + 1;
                    tmp.h = get_manhattan(tmp, dest);
                    tmp.f = tmp.g + tmp.h;
                    grid[tup.x + xOffset, tup.y + yOffset] = tmp;
                    neighbours.Add(grid[tup.x + xOffset, tup.y + yOffset]);
                }             
            }

        }       
        return neighbours;
    }
    private int get_manhattan(Tuple start, Tuple end)
    {
        //gets manhattan distance between two coords, each represented by a tuple.
        return Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);
    }

    

}
