using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemies : MonoBehaviour
{
    EnemiesList enemies;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyValues enemy;
        enemy = other.gameObject.GetComponent<EnemyValues>();
        if (enemy.PTeam == true)
        {

            //Debug.Log("Has Triggered");
            //enemies.enemies.Enqueue(other.gameObject);//pointing to Enemies's queue "enemies".

        }



    }
}