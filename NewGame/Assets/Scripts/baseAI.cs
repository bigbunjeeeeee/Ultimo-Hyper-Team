using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class baseAI : MonoBehaviour
{
    public float health;
    public float attack;
    public float attackSpd;
    float timer;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        timer = attackSpd;
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Draw();
        }

        if(health <= 0)
        {

         SceneManager.LoadScene("You_lose");

        }
    }

    private void Draw()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var hitCollider in hitColliders)
        {
            if (this.gameObject.CompareTag("EnemyBase"))
            {
                if (hitCollider.GetComponent<EnemyValues>().PTeam == true)
                {
                    timerEnded(hitCollider);

            }
        }
            if (this.gameObject.CompareTag("Base"))
            {
                if (hitCollider.GetComponent<EnemyValues>().PTeam == false)
                {
                    timerEnded(hitCollider);

                }
            }
        }
    }

    void timerEnded(Collider2D target)
    {
        EnemyValues enemy = target.GetComponent<EnemyValues>();
        enemy.health -= attack;
        timer = attackSpd;
        
    }
}
