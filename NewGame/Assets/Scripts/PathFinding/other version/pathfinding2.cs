using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinding2 : MonoBehaviour
{

    grid2 grid;
    public Transform StartPos;
    public Transform EndPos;
    public Vector3 pos;
    EnemyValues stats;


    private void Awake()
    {
        grid = GetComponent<grid2>();
        stats = this.gameObject.GetComponent<EnemyValues>();
    }

    private void Update()
    {
        FindPath(StartPos.position, EndPos.position);
        pos = this.transform.position;
    }
    void FindPath(Vector3 Spos, Vector3 Epos)
    {
        node2 startNode = grid.NodeFromWorldPos(Spos);
        node2 endNode = grid.NodeFromWorldPos(Epos);

        

        List<node2> openlist = new List<node2>();
        HashSet<node2> closedlist = new HashSet<node2>();

        openlist.Add(startNode);

        while (openlist.Count > 0)
        {
            node2 currentnode = openlist[0];
            for(int i = 1; i < openlist.Count; i++)
            {
                if(openlist[i].Fcost <currentnode.Fcost || openlist[i].Fcost == currentnode.Fcost && openlist[i].hcost < currentnode.hcost)
                {
                    currentnode = openlist[i];
                }
            }
            //Vector3.MoveTowards(pos, currentnode.pos, stats.speed * Time.deltaTime);

        
            openlist.Remove(currentnode);
            closedlist.Add(currentnode);

            if (currentnode == endNode)
            {
                GetFinalPath(startNode, endNode);
            }

            foreach(node2 neighborNode in grid.getNeighbors(currentnode))
            {
                if(!neighborNode.IsWall || closedlist.Contains(neighborNode))
                {
                    continue;
                }
                int MoveCost = currentnode.gcost + GetManhattenDist(currentnode, neighborNode);

                if(MoveCost < neighborNode.gcost || !openlist.Contains(neighborNode))
                {
                    neighborNode.gcost = MoveCost;
                    neighborNode.hcost = GetManhattenDist(neighborNode, endNode);
                    neighborNode.Parent = currentnode;

                    if(!openlist.Contains(neighborNode))
                    {
                        openlist.Add(neighborNode);
                    }
                }
            }
        }


    }

    private int GetManhattenDist(node2 currentnode, node2 neighborNode)
    {
        int ix = Mathf.Abs(currentnode.gridX - neighborNode.gridX);
        int iy = Mathf.Abs(currentnode.gridY - neighborNode.gridY);

        return ix + iy;
    }

    private void GetFinalPath(node2 startNode, node2 endNode)
    {
        List<node2> FinalPath = new List<node2>();
        node2 currentNode = endNode;

        while(currentNode != startNode)
        {
            FinalPath.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        FinalPath.Reverse();
        grid.FinalPath = FinalPath;
    }

}

