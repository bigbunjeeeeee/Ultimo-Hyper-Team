using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public abstract class AIDecision : MonoBehaviour
{
    public abstract void Decide(EnemiesList enemies);   
}

public class DecisionMake : AIDecision
{
    public Vector2 positionPlayerNpcPassBy { get; set; }
    public Func<GameObject,bool> Test { get; set; }
    public AIDecision Attack { get; set; }
    public AIDecision Defend { get; set; }


    public override void Decide(EnemiesList enemies)
    {
        bool result = this.Test(enemies.enemies.Peek());
        if(result)
        {
            this.Defend.Decide(enemies);
        }
        else
        {
            this.Attack.Decide(enemies);
        }
    }
}

public class DecisionResult : AIDecision
{
    public bool Result { get; set; }
    public GameObject allyToSpawn { get; set; }
    public Vector2 offsetBasePosition { get; set; }
    public override void Decide(EnemiesList enemies)
    {
        Instantiate(allyToSpawn, offsetBasePosition, Quaternion.identity);
       
    }
}