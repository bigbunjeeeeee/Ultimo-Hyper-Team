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
    private EnemyValues getHealth;

    private int gold;

    private DecisionMake DecisionTree()
    {
        //DecisionMake EnemyNearBaseTopRounder = (DecisionMake)ScriptableObject.CreateInstance(typeof(DecisionMake));
        //EnemyNearBaseTopRounder.EnemyTeamTag = enemies.Peek().tag == "AllRounder";
        //EnemyNearBaseTopRounder.IsOnPTeam = IsPeekQueueEnemyFromThePlayer();
        //DecisionResult EnemyNearBaseTopRounderResult = (DecisionResult)ScriptableObject.CreateInstance(typeof(DecisionResult));
        //EnemyNearBaseTopRounderResult.Result = true;
        //EnemyNearBaseTopRounderResult.allyToSpawn = AllyAllrounder;
        //EnemyNearBaseTopRounderResult.offsetBasePosition = TopSpawnOfBase;
        //EnemyNearBaseTopRounder.Defend defend = EnemyNearBaseTopRounderResult;
        // EnemyNearBaseTopRounder.Defend = 

        var EnemyNearBaseTopRounder = new DecisionMake()
        {
            EnemyTeamTag = enemies.Peek().tag == "AllRounder",
            IsOnPTeam = IsPeekQueueEnemyFromThePlayer(),
            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllyAllrounder, offsetBasePosition = TopSpawnOfBase }
        };

        var EnemyNearBaseBotRounder = new DecisionMake()
        {

            EnemyTeamTag = enemies.Peek().tag == "Speed",
            IsOnPTeam = IsPeekQueueEnemyFromThePlayer(),

            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllySpeed, offsetBasePosition = TopSpawnOfBase }
        };

        var EnemyNearBaseTopHeavy = new DecisionMake()
        {

            EnemyTeamTag = enemies.Peek().tag == "Giant",
            IsOnPTeam = IsPeekQueueEnemyFromThePlayer(),

            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllyHeavy, offsetBasePosition = TopSpawnOfBase }
        };

       if(enemies.Peek().tag == "Speed")
        {
            return EnemyNearBaseBotRounder;
        }

        if (enemies.Peek().tag == "AllRounder" )
        {
            return EnemyNearBaseTopRounder;

        }
        if (enemies.Peek().tag == "Giant")
        {
            return EnemyNearBaseTopHeavy;
        }
        return null;
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
        int randomNumber = Random.Range(1, 4);
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
    void RemoveFromQueIfDead()
    {
        float getFirstInQueHealth = getHealth.health;
        if (getFirstInQueHealth <=0 )
        {
            enemies.Dequeue();
        }
    }
    void Update()
    {
        gold = AIGold.enemyGold;
       
        if (enemies.Count != 0 && enemies.Peek() != null)
        {
            getHealth = enemies.Peek().GetComponent<EnemyValues>();
            RemoveFromQueIfDead();
        }
        if (enemies.Count != 0)
        {

            if ( gold > 4)
            {
                AIGold.CostPerUnit(DecisionTree(),  enemies);
            }
        }
        else
        {
            if (gold > 4)
            {
                
                AIGold.RandomUnit(ReturnARandomObject(),TopSpawnOfBase);
            }
        }
        Debug.Log(enemies.Count);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyValues enemy  ;
        enemy = other.gameObject.GetComponent<EnemyValues>();
        if (enemy.PTeam == true)
        {

            
           enemies.Enqueue(other.gameObject);//pointing to Enemies's queue "enemies".
            
        }

    }
}
