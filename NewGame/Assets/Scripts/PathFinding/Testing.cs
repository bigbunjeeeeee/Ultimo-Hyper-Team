using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private PathFinding pathfinding;
    void Start()
    {
        pathfinding = new PathFinding(30, 10, this.transform.position);
    }
}
