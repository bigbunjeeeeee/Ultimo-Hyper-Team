using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public int x;
    public int y;
    public float cells;
    // Start is called before the first frame update
    void Start()
    {
        Grid grid = new Grid(x, y, cells);
    }
}
