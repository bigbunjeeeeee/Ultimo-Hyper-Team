using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AI Decision", menuName = "ScriptableObjects/SpawnManagerAI_Decision", order = 1)]
public abstract class AIDecision : ScriptableObject 
{ 
    public abstract void Decide(GameObject enemies);   
}

[CreateAssetMenu(fileName = "AI Decision1", menuName = "ScriptableObjects/SpawnManagerAI_Decision1", order = 2)]
public class DecisionMake : AIDecision
{
    public bool EnemyTeamTag { get; set; }// This will detect , what type of AI has the Player spawned 

    public bool IsOnPTeam { get; set; } // This just checks if the AI is from the Player's side 

    public AIDecision Defend { get; set; } //This is the Decision that the AI will make after the conditions are met
   
    public override void Decide(GameObject enemies)
    {
        
        if (EnemyTeamTag  && IsOnPTeam )// If it is from the enemy team then do the outcome of the decision making, and check what type it is
        {
            this.Defend.Decide(enemies);
        }
       
    }
}
[CreateAssetMenu(fileName = "AI Decision2", menuName = "ScriptableObjects/SpawnManagerAI_Decision2", order = 3)]
public class DecisionResult : AIDecision
{
    //This is the Result which spawns a type of AI that counters the Player's AI
    
    public GameObject allyToSpawn { get; set; }// Parameter for what object to spawn

    public Vector2 offsetBasePosition { get; set; }// Parameter Location to Spawn

    public override void Decide(GameObject enemies)//Output that uses the Parameters
    {
        EnemyValues getIsPteam = allyToSpawn.GetComponent<EnemyValues>();//We get the Object's Component EnemyValue to get the boolean variable PTeam 
        getIsPteam.PTeam = false;// We set it to false because we don't want it to be on the Player's Side
       
        Instantiate(allyToSpawn, offsetBasePosition, Quaternion.identity);
    }
}