using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public abstract class AIDecision : MonoBehaviour
{
    public GameObject Rounder;
    public GameObject Giant;
    public GameObject SpeedEnemy;
    public abstract void Decide(EnemiesList enemies);
    
}

public class DecisionMake : AIDecision
{
    public Collision CollisionBox { get; set; }
    public AIDecision Attack { get; set; }
    public AIDecision Defend { get; set; }
    public Func<EnemiesList, bool,bool> Test { get; set; }
    
    public override void Decide(EnemiesList enemies)
    {
        IsEnemyInBase isTheEnemyInBaseCheck = new IsEnemyInBase();
        bool result = this.Test(enemies, isTheEnemyInBaseCheck.isEnemyInBase);
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

    public override void Decide(EnemiesList enemies)
    {
        Instantiate(Rounder, new Vector2(0, 0), Quaternion.identity);
    }
}