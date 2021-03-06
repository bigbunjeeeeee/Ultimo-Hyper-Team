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

    public bool Player;
    // Start is called before the first frame update
    void Start()
    {
        timer = attackSpd;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

       

        Debug.Log(health);

        if(health <= 0)
        {
            Debug.Log("Dead");
            if (Player == true)
            {
                Debug.Log("Change Scene");
                SceneManager.LoadScene("You_Lose");
            }
            else
            {
                if (poweups.FinalLevel == true)
                {
                    SceneManager.LoadScene("You_Win");
                    poweups.FinalLevel = false;
                }
                else
                {
                    SceneManager.LoadScene("Sauls_Shop");
                    poweups.FinalLevel = true;
                }
            }

        }

        if (timer <= 0f)
        {
            Draw();
        }

    }
    bool IsItAnObject(Collider2D other)
    {
        if (other.gameObject.tag == "AllRounder")
        {
            return true;
        }
        else if (other.gameObject.tag == "Speed")
        {
            return true;
        }
        else if (other.gameObject.tag == "Giant")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Draw()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var hitCollider in hitColliders)
        {
            if (this.gameObject.CompareTag("EnemyBase"))
            {
                if (hitCollider != null)
                {
                    if (IsItAnObject(hitCollider))
                    {
                        if (hitCollider.GetComponent<EnemyValues>().PTeam == true)
                        {
                            timerEnded(hitCollider);
                        }
                    }
                   
                }
            
        }
            if (this.gameObject.CompareTag("Base"))
            {
                if (hitCollider != null)
                {
                    if (IsItAnObject(hitCollider))
                    {
                        if (hitCollider.GetComponent<EnemyValues>().PTeam == false)
                        {
                            timerEnded(hitCollider);

                        }
                    }
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
