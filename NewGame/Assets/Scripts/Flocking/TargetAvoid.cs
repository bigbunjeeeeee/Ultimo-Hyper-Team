using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAvoid : MonoBehaviour
{
    bool goingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goingRight)
        {
            transform.position += new Vector3(2.0f, 0.0f, 0.0f) * Time.deltaTime;
            if (transform.position.x > 8.0f)
            {
                goingRight = false;
            }
        }
        else
        {
            transform.position += new Vector3(-2.0f, 0.0f, 0.0f) * Time.deltaTime;
            if (transform.position.x < -8.0f)
            {
                goingRight = true;
            }
        }
    }
}
