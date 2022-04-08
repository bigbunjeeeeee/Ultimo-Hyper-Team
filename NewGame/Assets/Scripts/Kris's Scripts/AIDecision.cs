using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AI Decision", menuName = "ScriptableObjects/SpawnManagerAI_Decision", order = 1)]
public abstract class AIDecision : ScriptableObject//Still need to find a way to know which AllyNpc to spawn , might need another tag to add the npcs
{
    public abstract void Decide(Queue<GameObject> enemies);   
}

[CreateAssetMenu(fileName = "AI Decision1", menuName = "ScriptableObjects/SpawnManagerAI_Decision1", order = 2)]
public class DecisionMake : AIDecision
{
    public bool EnemyTeamTag { get; set; }
    public bool IsOnPTeam { get; set; }
   
    public bool IsItEnemy { get; set; }

    public AIDecision Attack { get; set; }
    public AIDecision Defend { get; set; }
    //Maybe add gameObj with tag  with a get ; set;
    //Fuck programming , delete if you see this <-

    public override void Decide(Queue<GameObject> enemies)
    {

        if (IsOnPTeam)
        {
            this.Defend.Decide(enemies);
        }
        else if (enemies == null)
        {
            this.Attack.Decide(enemies);
        }
    }
}
[CreateAssetMenu(fileName = "AI Decision2", menuName = "ScriptableObjects/SpawnManagerAI_Decision2", order = 3)]
public class DecisionResult : AIDecision
{
    public bool Result { get; set; }
    public GameObject allyToSpawn { get; set; }
    public Vector2 offsetBasePosition { get; set; }
    public override void Decide(Queue<GameObject> enemies)
    {
        Instantiate(allyToSpawn, offsetBasePosition, Quaternion.identity);
       
    }
}