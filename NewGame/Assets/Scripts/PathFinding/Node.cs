using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Grid<Node> grid;
    public int x;
    public int y;

    public int gcost;
    public int hcost;
    public int fcost;

    public bool isWalkable;

    public Node prevnode;
    public Node(Grid<Node> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }

    public void CalculateFCost()
    {
        fcost = gcost + hcost;
    }
    public override string ToString()
    {
        return x + "," + y;
    }
}