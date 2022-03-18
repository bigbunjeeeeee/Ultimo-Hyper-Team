using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToDetect : MonoBehaviour
{
    // public Enemies enemy = new Enemies() ;//We can use this to go through a loop and detect which type of enemy is getting closer to the base , and we can spawn an npc that can counter this 
    // public GameObject Base2; // We can compare the AI's base position with the approaching enemies
    public EnemiesList enemy = new EnemiesList();
    public GameObject obj;
    public Vector2 pointOfContact;
    private ContactPoint2D[] contacts = new ContactPoint2D[10];
    public BoxCollider2D coll ;
    
    void Start()
    {
        coll = obj.GetComponent<BoxCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        other.GetContacts(contacts);
        foreach (ContactPoint2D contact in contacts)
        {
            pointOfContact = contact.point;
            Debug.Log("Contact point is: "+ pointOfContact);
        }


        if (other.tag == "PlayerAI")
        {
            Debug.Log("Has Triggered");
            enemy.enemies.Enqueue(other.gameObject);
        }


    }
        
       
   
}



 