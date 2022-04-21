using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class EnemiesList : MonoBehaviour
{
    // ENEMIES NEED BOX COLLIDER WITH A TRIGGER ON
    public List<GameObject> enemies;
    //Need to Insert Prefabs from foulders
    public GameObject AllyAllrounder;
    public GameObject AllySpeed;
    public GameObject AllyHeavy;
    //For the base just reference it from the World
    public GameObject AllyBase;
    private Vector2 AllyBasePosition;
    private Vector2 TopSpawnOfBase;
    private Vector2 BottomSpawnOfBase;

   
    public GameObject EnemyGoldObj;
    private EnemyGold AIGold;
    private int gold;
    private DecisionMake DecisionTree(GameObject enemy)
    {
        var EnemyNearBaseTopRounder = new DecisionMake
        {

            EnemyTeamTag = enemy.tag == "AllRounder",
            IsOnPTeam = IsPeekQueueEnemyFromThePlayer(enemy),

            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllyAllrounder, offsetBasePosition = TopSpawnOfBase }
        };

        var EnemyNearBaseBotRounder = new DecisionMake
        {

            EnemyTeamTag = enemy.tag == "Speed",
            IsOnPTeam = IsPeekQueueEnemyFromThePlayer(enemy),

            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllySpeed, offsetBasePosition = TopSpawnOfBase }
        };

        var EnemyNearBaseTopHeavy = new DecisionMake
        {

            EnemyTeamTag = enemy.tag == "Giant",
            IsOnPTeam = IsPeekQueueEnemyFromThePlayer(enemy),

            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllyHeavy, offsetBasePosition = TopSpawnOfBase }
        };

       if(enemy.tag == "Speed")
        {
            return EnemyNearBaseBotRounder;
        }

        if (enemy.tag == "AllRounder" )
        {
            return EnemyNearBaseTopRounder;

        }
        if (enemy.tag == "Giant")
        {
            return EnemyNearBaseTopHeavy;
        }
        return null;
    }


    bool IsPeekQueueEnemyFromThePlayer(GameObject enemy)
    {
        EnemyValues getBool = enemy.GetComponent<EnemyValues>();
        return getBool.PTeam;
    }

    GameObject ReturnARandomObject()
    {
        int randomNumber = Random.Range(1, 3);
        if (randomNumber == 1)
        {
            return AllyAllrounder;
        }
        else if (randomNumber == 2)
        {
            return AllySpeed;
        }
        else if (randomNumber == 3)
        {
            return AllyHeavy;
        }
        else
        {
            return null;
        }
       
    }
  
    void Awake()
    {
        AIGold = EnemyGoldObj.GetComponent<EnemyGold>();
       
    }

    void Start()
    {
        AllyBasePosition = AllyBase.transform.position;
        TopSpawnOfBase = AllyBasePosition - new Vector2(0, -0.5f);
        BottomSpawnOfBase = AllyBasePosition - new Vector2(0, 5);
        enemies = new List<GameObject>();
    }

    void Update()
    {
        gold = AIGold.enemyGold;
        // Debug.Log(enemies.Count);
       
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject enemy = enemies[i];
            if(enemy != null)
            {
                EnemyValues getHP = enemy.GetComponent<EnemyValues>();
                if (enemy == null)
                {
                    break;

                }
                else if (getHP.health <= 0)
                {
                    enemies.RemoveAt(i);
                }
            }
            
        }

            if (enemies.Count > 0)
        {
            for (int i = 0; i<enemies.Count;i++)
            {
                GameObject enemy = enemies[i];

                if (enemies.Count == 0)
                {
                    break;

                }
                else if (gold > 4)
                {
                    AIGold.CostPerUnit(DecisionTree(enemy), IsPeekQueueEnemyFromThePlayer(enemy), enemy);
                    if (enemy == null)
                    {
                        i++;
                    }

                }  
            } 
            
        }
       else if (gold > 4 && enemies.Count == 0)
       {
            AIGold.CostPerRandomUnit(ReturnARandomObject(), TopSpawnOfBase);
       }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyValues enemy  ;
        enemy = other.gameObject.GetComponent<EnemyValues>();
        if (enemy.PTeam == true)
        {
           enemies.Add(other.gameObject);//pointing to Enemies's queue "enemies".
        }



    }

}
