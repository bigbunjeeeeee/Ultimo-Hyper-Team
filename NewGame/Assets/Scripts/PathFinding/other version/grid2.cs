using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid2 : MonoBehaviour
{

    public Transform StartPos;
    public LayerMask WallMask;
    public Vector2 GridWorldSize;
    public float NodeSize;
    public float Distance;

    node2[,] grid;
    public List<node2> FinalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;


    private void Start()
    {
        nodeDiameter = NodeSize * 2;
        gridSizeX = Mathf.RoundToInt(GridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(GridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new node2[gridSizeX, gridSizeY];
        Vector3 BottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;
        for(int y = 0; y < gridSizeX; y++)
        {
            for(int x = 0; x < gridSizeY; x++)
            {
                Vector3 worldPoint = BottomLeft + Vector3.right * (x * nodeDiameter + NodeSize) + Vector3.forward * (y * nodeDiameter + NodeSize);
                bool Wall = false;

                if(Physics.CheckSphere(worldPoint, NodeSize, WallMask))
                {
                    Wall = true;
                }

                grid[x, y] = new node2(Wall, worldPoint, x, y);
            }
        }
    }

    public List<node2> getNeighbors(node2 node)
    {
        List<node2> NList = new List<node2>();
        int xcheck;
        int ycheck;

        //right
        xcheck = node.gridX + 1;
        ycheck = node.gridY;
        if(xcheck >= 0 && xcheck < gridSizeX)
        {
            if(ycheck >= 0 && ycheck < gridSizeY)
            {
                NList.Add(grid[xcheck, ycheck]);
            }
        }
        //Left
        xcheck = node.gridX - 1;
        ycheck = node.gridY;
        if (xcheck >= 0 && xcheck < gridSizeX)
        {
            if (ycheck >= 0 && ycheck < gridSizeY)
            {
                NList.Add(grid[xcheck, ycheck]);
            }
        }
        //Top
        xcheck = node.gridX;
        ycheck = node.gridY + 1;
        if (xcheck >= 0 && xcheck < gridSizeX)
        {
            if (ycheck >= 0 && ycheck < gridSizeY)
            {
                NList.Add(grid[xcheck, ycheck]);
            }
        }
        //Bottom
        xcheck = node.gridX;
        ycheck = node.gridY - 1;
        if (xcheck >= 0 && xcheck < gridSizeX)
        {
            if (ycheck >= 0 && ycheck < gridSizeY)
            {
                NList.Add(grid[xcheck, ycheck]);
            }
        }

        return NList;
    }

    public node2 NodeFromWorldPos(Vector3 worldpos)
    {
        float xpoint = ((worldpos.x + GridWorldSize.x / 2) / GridWorldSize.x);
        float ypoint = ((worldpos.y + GridWorldSize.y / 2) / GridWorldSize.y);

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return grid[x, y];  
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x,GridWorldSize.y, 1));

        if(grid !=null)
        {
            foreach(node2 node  in grid)
            {
                if(node.IsWall)
                {
                    Gizmos.color = Color.yellow;
                }
                else
                {
                    Gizmos.color = Color.white;
                }

                if (FinalPath != null)
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawCube(node.pos, Vector3.one * (nodeDiameter - Distance));
            }

        }
    }
}
