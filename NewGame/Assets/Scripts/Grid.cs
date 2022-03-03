using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform StartPos;
    public LayerMask wallmask;
    public Vector2 worldsize;
    public float noderadius;
    public float distance;

    Node[,] grid;
    public List<Node> final_path;

    float NodeDiamater;
    int gridsizex, gridsizey;

    private void Start()
    {
        NodeDiamater = noderadius * 2;
        gridsizex = Mathf.RoundToInt(worldsize.x / NodeDiamater);
        gridsizey = Mathf.RoundToInt(worldsize.y / NodeDiamater);

        
    }
    void creategrid()
    {
        grid = new Node[gridsizex, gridsizey];
        //Vector2 bottomleft = transform.position - Vector2.right * worldsize.x / 2 - Vector2.up * worldsize.y / 2;
        for (int y = 0; y < gridsizey; y++)
        {
            for(int x = 0; x < gridsizex; x++)
            {
                Vector2 worldPoint = 
            }
        }
    }
}
