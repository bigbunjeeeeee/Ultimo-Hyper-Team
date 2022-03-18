using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToDetect : MonoBehaviour
{
    // public Enemies enemy = new Enemies() ;//We can use this to go through a loop and detect which type of enemy is getting closer to the base , and we can spawn an npc that can counter this 
    // public GameObject Base2; // We can compare the AI's base position with the approaching enemies
    public EnemiesList enemy = new EnemiesList();
    public BoxCollider2D collider;
     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PlayerAI")
        {
            Debug.Log("Has Triggered");
            enemy.enemies.Add(other.gameObject);
        }
        
        
    }
}
