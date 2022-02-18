using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseAI : MonoBehaviour
{
    public float health;
    public float attack;
    public float attackSpd;
    public float timer;
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
    }

    private void Draw()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag != this.tag)
            {
                timerEnded(hitCollider);

            }
        }
    }

    void timerEnded(Collider2D target)
    {
        EnemyValues enemy = target.GetComponent<EnemyValues>();
        enemy.health -= attack;
        
    }
}
