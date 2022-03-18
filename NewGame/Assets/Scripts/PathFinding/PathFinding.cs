using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    private const int StraightCost = 10;
    private const int DiagonalCost = 14;

    public static PathFinding Instance { get; private set; }

    private Grid<Node> grid;
    private List<Node> openlist;
    private List<Node> closelist;

public PathFinding(int width, int height, Vector3 Origin)
    {
        Instance = this;
        grid = new Grid<Node>(width, height, 1f, Origin, (Grid<Node> g, int x, int y) => new Node(g, x, y));
    }

    public Grid<Node> GetGrid()
    {
        return grid;
    }

    public List<Vector3> FindPath(Vector3 startWorldPos, Vector3 endWorldPos)
    {
        grid.GetXY(startWorldPos, out int startX, out int startY);
        grid.GetXY(endWorldPos, out int endX, out int endY);

        List<Node> path = FindPath(startX,startY, endX, endY);
        if(path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorpath = new List<Vector3>();
            foreach (Node node in path)
            {
                vectorpath.Add(new Vector3(node.x, node.y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f);

            }
            return vectorpath;
        }
    }

    private List<Node> FindPath(int startx, int starty, int endx, int endy)
    {
        Node startnode = grid.GetGridObject(startx, starty);
        Node endnode = grid.GetGridObject(endx, endy);

        openlist = new List<Node> { startnode };
        closelist = new List<Node>();

        for (int x = 0; x < grid.getwidth(); x++)
        {
            for(int y = 0; y < grid.getheight(); y++)
            {
                Node node = grid.GetGridObject(x, y);
                node.gcost = int.MaxValue;
                node.CalculateFCost();
                node.prevnode = null;

                startnode.gcost = 0;
                startnode.hcost = CalculateDist(startnode, endnode);
                startnode.CalculateFCost();

                while(openlist.Count > 0)
                {
                    Node currentnode = LowestFcostNode(openlist);
                    if(currentnode == endnode)
                    {
                        return CalculatePath(endnode);
                    }

                    openlist.Remove(currentnode);
                    closelist.Add(currentnode);

                    foreach(Node neighborNode in GetNeighbors(currentnode))
                    {
                        if (closelist.Contains(neighborNode)) continue;
                        //if(!neighborNode.isWalkable)
                        //{
                        //    closelist.Add(neighborNode);
                        //    continue;

                        //}

                        int tempGcost = currentnode.gcost + CalculateDist(currentnode, neighborNode);
                        if(tempGcost < neighborNode.gcost)
                        {
                            neighborNode.prevnode = currentnode;
                            neighborNode.gcost = tempGcost;
                            neighborNode.hcost = CalculateDist(neighborNode, endnode);
                            neighborNode.CalculateFCost();

                            if(!openlist.Contains(neighborNode))
                            {
                                openlist.Add(neighborNode);
                            }
                        }
                    }
                }
                //escaped while
                
            }
        }
        return null;
    }
    private List<Node> GetNeighbors(Node currentnode)
    {
        List<Node> neighborList = new List<Node>();
        if (currentnode.x - 1 >= 0)
        {

            neighborList.Add(GetNode(currentnode.x - 1, currentnode.y));

            if (currentnode.y - 1 >= 0) neighborList.Add(GetNode(currentnode.x - 1, currentnode.y - 1));
            if (currentnode.y + 1 < grid.getheight()) neighborList.Add(GetNode(currentnode.x - 1, currentnode.y + 1));

        }
        if (currentnode.x + 1 < grid.getwidth())
        {
            neighborList.Add(GetNode(currentnode.x + 1, currentnode.y));
            if (currentnode.y - 1 >= 0) neighborList.Add(GetNode(currentnode.x + 1, currentnode.y - 1));
            if (currentnode.y + 1 < grid.getheight()) neighborList.Add(GetNode(currentnode.x + 1, currentnode.y + 1));
        }

        if (currentnode.y - 1 >= 0) neighborList.Add(GetNode(currentnode.x, currentnode.y - 1));
        if (currentnode.y + 1 < grid.getheight()) neighborList.Add(GetNode(currentnode.x, currentnode.y + 1));

        return neighborList;

    }

    private Node GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private List<Node> CalculatePath(Node endnode)
    {
        List<Node> path = new List<Node>();
        path.Add(endnode);
        Node currentnode = endnode;
        while(currentnode.prevnode != null)
        {
            path.Add(currentnode.prevnode);
            currentnode = currentnode.prevnode;
        }
        path.Reverse();
        return path;
    }
    private int CalculateDist(Node a, Node b)
    {
        int xDist = Mathf.Abs(a.x - b.x);
        int yDist = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDist - yDist);
        return DiagonalCost * Mathf.Min(xDist, yDist) + StraightCost + remaining;
    }
    private Node LowestFcostNode(List<Node> nodelist)
    {
        Node LowestFcostNode = nodelist[0];
        for (int i = 1; i < nodelist.Count; i++)
        {
            if(nodelist[i].fcost < LowestFcostNode.fcost)
            {
                LowestFcostNode = nodelist[i];
            }
        }
        return LowestFcostNode;
    }

    }


