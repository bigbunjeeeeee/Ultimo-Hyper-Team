using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class EnemiesList : MonoBehaviour
{
    // ENEMIES NEED BOX COLLIDER WITH A TRIGGER ON
    public Queue<GameObject> enemies;
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
    private DecisionMake DecisionTree()
    {
        var EnemyNearBaseTopRounder = new DecisionMake
        {

            EnemyTeamTag = enemies.Peek().tag == "AllRounder",
            IsOnPTeam = IsPeekQueueEnemyFromThePlayer(),

            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllyAllrounder, offsetBasePosition = TopSpawnOfBase }
        };

        var EnemyNearBaseBotRounder = new DecisionMake
        {

            EnemyTeamTag = enemies.Peek().tag == "Speed",
            IsOnPTeam = IsPeekQueueEnemyFromThePlayer(),

            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllySpeed, offsetBasePosition = BottomSpawnOfBase }
        };

        var EnemyNearBaseTopHeavy = new DecisionMake
        {

            EnemyTeamTag = enemies.Peek().tag == "Giant",
            IsOnPTeam = IsPeekQueueEnemyFromThePlayer(),

            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllyHeavy, offsetBasePosition = TopSpawnOfBase }
        };

        var NoEnemy = new DecisionMake
        {
            EnemyTeamTag = false,
            IsOnPTeam = false,

            Attack = new DecisionResult { Result = true, allyToSpawn = ReturnARandomObject(), offsetBasePosition = TopSpawnOfBase },
            Defend = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) }
        };
        return NoEnemy;
    }

    //public EnemiesList()
    //{
    //    enemies = new Queue<GameObject>();
    //}

    bool IsPeekQueueEnemyFromThePlayer()
    {
        EnemyValues getBool = enemies.Peek().GetComponent<EnemyValues>();
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
        TopSpawnOfBase = AllyBasePosition - new Vector2(0, -1.5f);
        BottomSpawnOfBase = AllyBasePosition - new Vector2(0, 5);
        enemies = new Queue<GameObject>();

       
        
        
    }
    void Update()
    {
        gold = AIGold.enemyGold;
       
        if (enemies.Count != 0)
        {
            
            if (gold >4)
            {
                var GetDecisions = DecisionTree();
                AIGold.CostPerUnit(DecisionTree(), IsPeekQueueEnemyFromThePlayer(), enemies);


            }
           
            //foreach (GameObject enemy in enemies)
            //{
                
               
            //    Debug.Log(enemy);


            //}
            
        }
        else
        {
            //Instantiate(AllyAllrounder, TopSpawnOfBase, Quaternion.identity);

        }



    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyValues enemy  ;
        enemy = other.gameObject.GetComponent<EnemyValues>();
        if (enemy.PTeam == true)
        {

            
           enemies.Enqueue(other.gameObject);//pointing to Enemies's queue "enemies".
            //Debug.Log(enemies.Count);
        }



    }

}
