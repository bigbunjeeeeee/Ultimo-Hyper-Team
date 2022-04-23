using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempWall : MonoBehaviour
{
    public bool placed = false;

    float timer = 0.0f;

    float duration = 7.5f;

    SpriteRenderer[] walls;

    Testing t;

    Unit_Behavior[] units;

    bool updatedWalls = false;

    // Start is called before the first frame update
    void Start()
    {
        walls = GetComponentsInChildren<SpriteRenderer>();
        t = FindObjectOfType<Testing>();
    }

    // Update is called once per frame
    void Update()
    {
        if (placed)
        {
            if (!updatedWalls)
            {
                for (int i = 0; i < walls.Length; i++)
                {
                    walls[i].gameObject.tag = "Wall";
                }
                units = FindObjectsOfType<Unit_Behavior>();
                foreach (Unit_Behavior unit in units)
                {
                    unit.poslock = false;
                }
                updatedWalls = true;
            }
            
            timer += Time.deltaTime;
            if (timer >= duration)
            {

                foreach (SpriteRenderer wall in walls)
                {
                    t.pathfinding.getGrid().GetXY(wall.transform.position, out int x, out int y);
                    t.pathfinding.GetNode(x, y).SetIsWalkable(t.pathfinding.GetNode(x, y).isWalkable = true);
                }
                units = FindObjectsOfType<Unit_Behavior>();
                foreach (Unit_Behavior unit in units)
                {
                    unit.poslock = false;
                }

                Destroy(gameObject);
            }
        }
    }
}
