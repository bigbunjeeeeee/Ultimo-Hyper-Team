using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class EnemiesList : MonoBehaviour
{
    
    public List<GameObject> enemies;//We store the Player's Units in a List. 

    public GameObject AllyAllrounder;//These 3 variables get the prefabs to spawn the AI's units
    public GameObject AllySpeed;
    public GameObject AllyHeavy;
   

    public GameObject AllyBase;// Get the AI's base

    private Vector2 AllyBasePosition;// Get the AI's base position

    //Offset of the AI's base position to spawn units
    private Vector2 TopSpawnOfBase;
   
    //Variable to get the AI's gold UI
    public GameObject EnemyGoldObj;

    //Variable from AI's gold
    private EnemyGold AIGold;
    
    //Variable that gets the AI's gold
    private int gold;

    private DecisionMake DecisionTree(GameObject enemy)//This is the Decision Tree
    {
        // In Each Decision We set up the parameters
        // that are required to evaluate the decision and output it 

        var SpawnAllRounder = new DecisionMake//Decision 1
        {
            EnemyTeamTag = enemy.tag == "Speed",
            IsOnPTeam = IsPeekQueueEnemyFromThePlayer(enemy),
            Defend = new DecisionResult {  allyToSpawn = AllyAllrounder, offsetBasePosition = TopSpawnOfBase }
        };

        var SpawnSpeed = new DecisionMake // Decision 2
        {

            EnemyTeamTag = enemy.tag == "Giant",
            IsOnPTeam = IsPeekQueueEnemyFromThePlayer(enemy),

           
            Defend = new DecisionResult {  allyToSpawn = AllySpeed, offsetBasePosition = TopSpawnOfBase }
        };

        var SpawnHeavy = new DecisionMake // Decision 3
        {

            EnemyTeamTag = enemy.tag == "AllRounder",
            IsOnPTeam = IsPeekQueueEnemyFromThePlayer(enemy),

            Defend = new DecisionResult {  allyToSpawn = AllyHeavy, offsetBasePosition = TopSpawnOfBase }
        };

        //Depending on the Player's Unit in the List
        //The output will  be between these 3 decisions
       if(enemy.tag == "Giant")
        {
            return SpawnSpeed;
        }

        if (enemy.tag == "Speed" )
        {
            return SpawnAllRounder;

        }
        if (enemy.tag == "AllRounder")
        {
            return SpawnHeavy;
        }
        return null;
    }

    // Returns the EnemyValues component from the Player's Spawned unit 
    bool IsPeekQueueEnemyFromThePlayer(GameObject enemy)
    {
        EnemyValues getBool = enemy.GetComponent<EnemyValues>();
        return getBool.PTeam;
    }

    // Returns a random object to spawn a random Enemy , if the Player doesn't spawn anything.
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
        //Get the Base's  Spawn at the Start of the Game
        AllyBasePosition = AllyBase.transform.position;
        // Set up Spawn Position
        TopSpawnOfBase = AllyBasePosition - new Vector2(0, -0.5f); 
        enemies = new List<GameObject>();
    }

    void Update()
    {
        gold = AIGold.enemyGold;
        
        //This for loop checks for each object in the list
        //If the list is empty , nothing happens, if there are objects
        //We check for each object if it is NOT dead and if it is , we remove it from the list
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
        // If there are enmies Start making Decisions for each enemy
            if (enemies.Count > 0)
        {
            for (int i = 0; i<enemies.Count;i++)
            {
                GameObject enemy = enemies[i];

                if (enemies.Count == 0)
                {
                    break;

                }
                else if (gold > 4)// If we have enough gold we can spawn
                {
                    //CostPerUnit and CostPerRandomUnit comes from the Enemy Gold Class
                    AIGold.CostPerUnit(DecisionTree(enemy), IsPeekQueueEnemyFromThePlayer(enemy), enemy);
                    if (enemy == null)
                    {
                        i++;
                    }

                }  
            } 
            
        }//If there are no enemies and we have enough gold then Spawn a random Unit
       else if (gold > 4 && enemies.Count == 0)
       {
            AIGold.CostPerRandomUnit(ReturnARandomObject(), TopSpawnOfBase);
       }

    }

    //This functions helps avoid an error that Unity causes 
    //when trying to access PTeam from EnemyValues
    //I was not able to find the cause of the error
    //However this function fixed the issue

    //What this function checks is what type of unit it is 
    bool IsItAnObject(Collider2D other)
    {
        if (other.gameObject.tag == "AllRounder")
        {
            return true;
        }else if(other.gameObject.tag == "Speed")
        {
            return true;
        }else if (other.gameObject.tag == "Giant")
        {
            return true;
        }
        else 
        { 
            return false; 
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(IsItAnObject(other))//if the unity is a type then check if it is on the Player's team
        {
            EnemyValues enemy = other.gameObject.GetComponent<EnemyValues>();//Get the enemyValues

            //If it is on the Player's Team then add it to the list
            if (enemy.GetIsOnTeam == true)
            {
                enemies.Add(other.gameObject);
            }
        }
       
        
    }
}
