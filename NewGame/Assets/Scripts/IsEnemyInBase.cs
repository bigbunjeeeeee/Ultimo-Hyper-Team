using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEnemyInBase : MonoBehaviour
{
    public bool isEnemyInBase = false;
    public IsEnemyInBase()
    {
        isEnemyInBase = new bool();
    }
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
        if(other.tag == "PlayerAI")
        {
            isEnemyInBase = true;
            Debug.Log("It has Invaded our base");
        }
    }
}
