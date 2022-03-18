using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<TGridObject>
{
    int width;
    int height;
    TGridObject[,] gridArray;
    Vector3 originPos;
    float cellsize;

    public Grid(int width, int height, float cellsize, Vector3 originPos, System.Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.originPos = originPos;
        this.width = width;
        this.height = height;
        this.cellsize = cellsize;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }

        }
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

    public float GetCellSize()
    {
        return cellsize;
    }

    public int getwidth()
    {
        return width;
    }
    public int getheight()
    {
        return height;
    }
    Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellsize + originPos;
    }

    public void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos - originPos).x / cellsize);
        y = Mathf.FloorToInt((worldPos - originPos).y / cellsize);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }

    }
    public TGridObject GetGridObject(Vector3 worldpos)
    {
        int x, y;
        GetXY(worldpos, out x, out y);
        return GetGridObject(x, y);
    }

}