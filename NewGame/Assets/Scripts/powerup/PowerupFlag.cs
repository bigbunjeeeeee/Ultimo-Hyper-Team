using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupFlag : MonoBehaviour
{
    public bool placed = false;
    public bool active;
    public float time;

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
            if (collision.gameObject.GetComponent<EnemyValues>().PTeam == true)
            {
                collision.gameObject.GetComponent<EnemyValues>().health += 5;
            }

        }
}
