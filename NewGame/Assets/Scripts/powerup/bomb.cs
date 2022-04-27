using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public bool placed = false;
    public bool active;
    public float time;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (placed == true)
        {
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    Debug.Log("destroy");
                    Destroy(this.gameObject);
                }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(time <= 0)
        {
            if (collision.gameObject.GetComponent<EnemyValues>().PTeam == false)
            {
                Destroy(collision.gameObject);
            }

        }
    }
}
