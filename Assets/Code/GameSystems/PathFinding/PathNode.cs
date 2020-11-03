using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private int x;
    private int y;

    public int gCost = 0;
    public int hCost;
    public int fCost;

    public PathNode cameFromNode;

    public PathNode(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void CalculateFValue ()
    {
        fCost = gCost + hCost;
    }

    public Vector2Int Cords ()
    {
        return new Vector2Int(x, y);
    }
}
