using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private PathFinding pathfinding;
    public float nodesize;
    GameObject ebase;
    GameObject[] walls;
    void Start()
    {
        pathfinding = new PathFinding(30, 10, transform.position, nodesize);
        walls = GameObject.FindGameObjectsWithTag("Wall");
    }

    private void Update()
    {
        //pathfinding.getGrid().GetXY(ebase.transform.position, out int x, out int y);
        List<Node> path = pathfinding.FindPath(8, 4, 22, 5);
        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * nodesize + transform.position * 1, new Vector3(path[i + 1].x, path[i + 1].y) * nodesize + transform.position * 1, Color.green, .5f);
            }
        }

        foreach (GameObject wall in walls)
        {
            pathfinding.getGrid().GetXY(wall.transform.position, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        }
    }
}
