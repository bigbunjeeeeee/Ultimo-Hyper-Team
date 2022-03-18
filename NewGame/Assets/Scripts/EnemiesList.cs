using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesList : MonoBehaviour
{

    public List<GameObject> enemies { get; set; }
    
    public EnemiesList()
    {
        enemies = new List<GameObject>();
    }
    void Update()
    {
        foreach (GameObject enemy in enemies)
        {
            Debug.Log(enemy);
        }
    }
}
