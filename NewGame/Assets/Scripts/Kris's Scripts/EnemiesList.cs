using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemiesList : MonoBehaviour
{
    // ENEMIES NEED BOX COLLIDER WITH A TRIGGER ON
    public Queue<GameObject> enemies { get; set; }
    //Need to Insert Prefabs from foulders
    public GameObject AllyAllrounder;
    public GameObject AllySpeed;
    public GameObject AllyHeavy;
    //For the base just reference it from the World
    public GameObject AllyBase;
    private Vector2 AllyBasePosition;
    private Vector2 TopSpawnOfBase;
    private Vector2 BottomSpawnOfBase;
    private DecisionMake getDecisions;
    private DecisionMake DecisionTree()
    {
        //AllRounder
        var EnemyPushingTopRounder = new DecisionMake {
            positionPlayerNpcPassBy = distBetEnemAndBase(),
            Attack = new DecisionResult { Result = true, allyToSpawn = AllyAllrounder, offsetBasePosition = TopSpawnOfBase },
            Defend = new DecisionResult { Result =  false, allyToSpawn = null,  offsetBasePosition = new Vector2(0, 0) }
        };
        var EnemyPushingBotRounder = new DecisionMake
        {
            positionPlayerNpcPassBy = distBetEnemAndBase(),
            Attack = new DecisionResult { Result = true, allyToSpawn = AllyAllrounder, offsetBasePosition = BottomSpawnOfBase },
            Defend = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) }
        };
        var EnemyNearBaseTopRounder = new DecisionMake
        {
            positionPlayerNpcPassBy = distBetEnemAndBase(),
            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllyAllrounder, offsetBasePosition = TopSpawnOfBase }
        };
        var EnemyNearBaseBotRounder = new DecisionMake
        {
            positionPlayerNpcPassBy = distBetEnemAndBase(),
            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllyAllrounder, offsetBasePosition = BottomSpawnOfBase }
        };

        //Speed
        var EnemyPushingTopSpeedy = new DecisionMake
        {
            positionPlayerNpcPassBy = distBetEnemAndBase(),
            Attack = new DecisionResult { Result = true, allyToSpawn = AllySpeed, offsetBasePosition = TopSpawnOfBase },
            Defend = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) }
        };
        var EnemyPushingBotSpeedy = new DecisionMake
        {
            positionPlayerNpcPassBy = distBetEnemAndBase(),
            Attack = new DecisionResult { Result = true, allyToSpawn = AllySpeed, offsetBasePosition = BottomSpawnOfBase },
            Defend = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) }
        };
        var EnemyNearBaseTopSpeedy = new DecisionMake
        {
            positionPlayerNpcPassBy = distBetEnemAndBase(),
            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllySpeed, offsetBasePosition = TopSpawnOfBase }
        };
        var EnemyNearBaseBotSpeedy = new DecisionMake
        {
            positionPlayerNpcPassBy = distBetEnemAndBase(),
            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllySpeed, offsetBasePosition = BottomSpawnOfBase }
        };

        //Heavy 
        var EnemyPushingTopHeavy = new DecisionMake
        {
            positionPlayerNpcPassBy = distBetEnemAndBase(),
            Attack = new DecisionResult { Result = true, allyToSpawn = AllyHeavy, offsetBasePosition = TopSpawnOfBase },
            Defend = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) }
        };
        var EnemyPushingBotHeavy = new DecisionMake
        {
            positionPlayerNpcPassBy = distBetEnemAndBase(),
            Attack = new DecisionResult { Result = true, allyToSpawn = AllyHeavy, offsetBasePosition = BottomSpawnOfBase },
            Defend = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) }
        };
        var EnemyNearBaseTopHeavy = new DecisionMake
        {
            positionPlayerNpcPassBy = distBetEnemAndBase(),
            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllyHeavy, offsetBasePosition = TopSpawnOfBase }
        };
        var EnemyNearBaseBotHeavy = new DecisionMake
        {
            positionPlayerNpcPassBy = distBetEnemAndBase(),
            Attack = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) },
            Defend = new DecisionResult { Result = true, allyToSpawn = AllyHeavy, offsetBasePosition = BottomSpawnOfBase }
        };

        var NoOnePushes= new DecisionMake
        {
            positionPlayerNpcPassBy = new Vector2(0,0),
            Attack = new DecisionResult { Result = true, allyToSpawn = AllyHeavy, offsetBasePosition = TopSpawnOfBase },
            Defend = new DecisionResult { Result = false, allyToSpawn = null, offsetBasePosition = new Vector2(0, 0) }
        };
        return NoOnePushes;
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
    void PassInDecisionTree(DecisionMake variable)
    {
        Decide(variable);
    }
    void Decide(DecisionMake variable)
    {
        variable.Decide(enemies);
    }
    void Start()
    {
        AllyBasePosition = AllyBase.transform.position;
        TopSpawnOfBase = AllyBasePosition - new Vector2(0, -5);
        BottomSpawnOfBase = AllyBasePosition - new Vector2(0, 5);
        var DoStuff = DecisionTree();
        getDecisions = DoStuff;
    }
    void Update()
    {
       
        foreach (GameObject enemy in enemies)
        {
            Debug.Log(enemy);
            
        }
        
        distBetEnemAndBase();
        getDecisions.Decide(enemies);

    }

    
}
