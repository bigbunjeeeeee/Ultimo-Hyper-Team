using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTest : MonoBehaviour
{
    public bool placed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (placed)
        {
            transform.position += new Vector3(-1.0f, 0.0f, 0.0f) * Time.deltaTime;
        }
    }
}
