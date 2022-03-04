using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    int width;
    int height;
    int[,] gridArray;
    float cellsize;

    public Grid(int width, int height, float cellsize)
    {
        this.width = width;
        this.height = height;
        this.cellsize = cellsize;

        gridArray = new int[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }
    Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellsize;
    }

    void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPos.x / cellsize);
        y = Mathf.FloorToInt(worldPos.y / cellsize);
    }
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }

    }
    void SetValue(Vector3 worldpos, int value)
    {
        int x, y;
        GetXY(worldpos, out x, out y);
        SetValue(x, y, value);
    }

}