using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    Vector2 movepoint;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = worldPos;
        if (worldPos.x <= -1 && worldPos.y <= 5 && worldPos.y >= -5)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(prefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
