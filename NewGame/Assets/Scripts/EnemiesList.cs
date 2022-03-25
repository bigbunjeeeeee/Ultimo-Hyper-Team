using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemiesList : MonoBehaviour
{

    public Queue<GameObject> enemies { get; set; }
    public GameObject AllyAllrounder;
    public GameObject AllySpeed;
    public GameObject AllyHeavy;
    public GameObject AllyBase;
    private Vector2 AllyBasePosition;
    private Vector2 TopSpawnOfBase;
    private Vector2 BottomSpawnOfBase;
    private DecisionMake DecisionTree()
    {
        var EnemyPushingTop = new DecisionMake {
            positionPlayerNpcPassBy = distBetEnemAndBase(),
            Test = (enemies) => enemies,
            Attack = new DecisionResult { Result = true, allyToSpawn = AllyAllrounder, offsetBasePosition = TopSpawnOfBase },
            Defend = new DecisionResult { Result =  false, allyToSpawn = null,  offsetBasePosition = new Vector2(0, 0) }
        };
        
         
    
    
    
      
     
       return EnemyPushingTop;
    }
    public EnemiesList()
    {
        enemies = new Queue<GameObject>();
    }
    public Vector2 distBetEnemAndBase()
    {
        Vector2 enemyDist = enemies.Peek().transform.position;
        return AllyBasePosition - enemyDist;
    }
    void Start()
    {
        AllyBasePosition = AllyBase.transform.position;
        TopSpawnOfBase = AllyBasePosition - new Vector2(0, -5);
        BottomSpawnOfBase = AllyBasePosition - new Vector2(0, 5);
    }
    void Update()
    {
        foreach (GameObject enemy in enemies)
        {
            Debug.Log(enemy);
        }
        distBetEnemAndBase();


    }

    
}
