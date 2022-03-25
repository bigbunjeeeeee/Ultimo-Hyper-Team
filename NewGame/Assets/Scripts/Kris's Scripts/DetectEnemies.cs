using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemies : MonoBehaviour
{
    EnemiesList enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerAI")
        {
            Debug.Log("Has Triggered");
            enemies.enemies.Enqueue(other.gameObject);
        }
        //My brain just clicked on this 
        // If other.tag == "PlayerAllRounder" 
        //add it to que, same for the other ""PlayerHeavy" ,"PlayerSpeedy" 
    }
}
