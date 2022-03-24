using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node2
{
    public int gridX;
    public int gridY;

    public bool IsWall;
    public Vector3 pos;

    public node2 Parent;

    public int gcost;
    public int hcost;
    public int Fcost { get { return gcost + hcost; } }

        public node2(bool wall, Vector3 a_pos, int a_gridX, int a_gridY)
    {
        this.IsWall = wall;
        this.pos = a_pos;
        this.gridX = a_gridX;
        this.gridY = a_gridY;
    }


}
