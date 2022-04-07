using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public abstract class AIDecision : MonoBehaviour//Still need to find a way to know which AllyNpc to spawn , might need another tag to add the npcs
{
    public abstract void Decide(Queue<GameObject> enemies);   
}

public class DecisionMake : AIDecision
{
    public Vector2 positionPlayerNpcPassBy { get; set; }
    public bool IsItEnemy { get; set; }
    
    public AIDecision Attack { get; set; }
    public AIDecision Defend { get; set; }
    //Maybe add gameObj with tag  with a get ; set;
    //Fuck programming , delete if you see this <-

    public override void Decide(Queue<GameObject> enemies)
    {

        //if (positionPlayerNpcPassBy.magnitude >= new Vector2(4, 0).magnitude && ItIsEnemy)
        //{ 
        //    this.Defend.Decide(enemies);
        //}
        //else if(positionPlayerNpcPassBy.magnitude <= new Vector2(4, 0).magnitude && ItIsEnemy)
        //{
        //    this.Attack.Decide(enemies);
        //}
    }
}

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