using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int gridX;
    public int gridY;

    public bool IsWall;
    public Vector2 position;

    public Node parent;

    public int gCost;
    public int hCost;

    public int fCost { get { return gCost = hCost; } }

    public Node(bool _iswall, Vector2 a_pos, int a_gridx, int a_gridy)
        {
        IsWall = _iswall;
        position = a_pos;
        gridX = a_gridx;
        gridY = a_gridy;
        }

   

}