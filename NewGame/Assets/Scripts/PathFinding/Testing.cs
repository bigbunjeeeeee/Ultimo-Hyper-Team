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

    }

    private void Update()
    {
        //pathfinding.getGrid().GetXY(ebase.transform.position, out int x, out int y);
        List<Node> path = pathfinding.FindPath(0, 0, 22, 5);
        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * nodesize + transform.position, new Vector3(path[i + 1].x, path[i + 1].y) * nodesize + transform.position, Color.green, .5f);
            }
        }
        walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {
            pathfinding.getGrid().GetXY(wall.transform.position, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(pathfinding.GetNode(x, y).isWalkable = false);
        }
    }
}
