using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    private const int Straight = 10;
    private const int Diagonal = 14;

    public static PathFinding Instance { get; private set; }
    private Grid<Node> grid;
    private List<Node> OpenList;
    private List<Node> CloseList;
    Vector3 originpos;
  public PathFinding(int width, int height, Vector3 origin, float cellsize)
    {
        Instance = this;
        grid = new Grid<Node>(width, height, cellsize, origin, (Grid<Node> g, int x, int y) => new Node(g, x, y));
        originpos = origin;
    }

    public List<Vector3> FindPath(Vector3 startworldpos, Vector3 endworldpos)
    {
        
        grid.GetXY(startworldpos, out int startX, out int startY);
        grid.GetXY(endworldpos, out int endX, out int endY);

        
        List<Node> path = FindPath(startX, startY, endX, endY);
        if(path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach(Node node in path)
            {
                vectorPath.Add(new Vector3(node.x, node.y) * grid.GetCellSize() + originpos * grid.GetCellSize());
            }
            return vectorPath;
        }
    }
    public List<Node> FindPath(int startX, int startY, int endX, int endY)
    {
        Node StartNode = grid.GetGridObject(startX, startY);
        Node EndNode = grid.GetGridObject(endX, endY);

        if(StartNode == null || EndNode == null)
        {
            return null;
        }

        OpenList = new List<Node> { StartNode };
        CloseList = new List<Node>();

        for(int x = 0; x < grid.getwidth(); x++)
        {
            for(int y = 0; y < grid.getheight(); y++)
            {
                Node pNode = grid.GetGridObject(x, y);
                pNode.gcost = 99999999;
                pNode.CalculateFCost();
                pNode.prevnode = null;
            }
        }

        StartNode.gcost = 0;
        StartNode.hcost = CalculateDistance(StartNode, EndNode);
        StartNode.CalculateFCost();

        while(OpenList.Count > 0)
        {
            Node currentNode = GetLowestFcost(OpenList);
            if(currentNode == EndNode)
            {
                return CalculatePath(EndNode);
            }

            OpenList.Remove(currentNode);
            CloseList.Add(currentNode);

            foreach(Node Nnode in GetNeighbors(currentNode))
            {
                if (CloseList.Contains(Nnode) || !Nnode.isWalkable)
                {
                    continue;
                }

                int tentGcost = currentNode.gcost + CalculateDistance(currentNode, Nnode);
                if (tentGcost < Nnode.gcost)
                {
                    Nnode.prevnode = currentNode;
                    Nnode.gcost = tentGcost;
                    Nnode.hcost = CalculateDistance(Nnode, EndNode);
                    Nnode.CalculateFCost();

                    if (!OpenList.Contains(Nnode))
                    {
                        OpenList.Add(Nnode);
                    }
                }
            }

        }

        return null;

    }

    private List<Node> GetNeighbors(Node currentNode)
    {
        List<Node> NList = new List<Node>();

        if(currentNode.x - 1 >= 0)
        {
            NList.Add(GetNode(currentNode.x - 1, currentNode.y));

            if (currentNode.y - 1 >= 0) NList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            if (currentNode.y + 1 < grid.getheight()) NList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if(currentNode.x + 1 < grid.getwidth())
        {
            NList.Add(GetNode(currentNode.x + 1, currentNode.y));
            if (currentNode.y - 1 >= 0) NList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            if (currentNode.y + 1 < grid.getheight()) NList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        if (currentNode.y - 1 >= 0) NList.Add(GetNode(currentNode.x, currentNode.y - 1));
        if (currentNode.y + 1 < grid.getheight()) NList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return NList;

    } 

    public Node GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }
    private List<Node> CalculatePath(Node endNode)
    {
        List<Node> path = new List<Node>();
        path.Add(endNode);
        Node currentnode = endNode;
        while(currentnode.prevnode != null)
        {
            path.Add(currentnode.prevnode);
            currentnode = currentnode.prevnode;
        }
        path.Reverse();
        return path;
    }
    private int CalculateDistance(Node a, Node b)
    {
        int Xdistance = Mathf.Abs(a.x - b.x);
        int Ydistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(Xdistance - Ydistance);

        return Diagonal * Mathf.Min(Xdistance, Ydistance) + Straight * remaining;

        //int ix = Mathf.Abs(a.x - b.x);
        //int iy = Mathf.Abs(a.y - b.y);

        //return ix + iy;
    }

    private Node GetLowestFcost(List<Node> Nodelist)
    {
        Node lowestfcost = Nodelist[0];
            for(int i = 1; i < Nodelist.Count; i++)
            {
            if(Nodelist[i].fcost < lowestfcost.fcost)
            {
                lowestfcost = Nodelist[i];
            }
            }
            return lowestfcost;
    }

    public Grid<Node> getGrid()
    {
        return grid;
    }

    }


