using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValues : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float damage;
    public float aoe;
    float timer;
    public float attackspeed;
    public bool PTeam = false;

    //private void Start()
    //{
    //    timer = attackspeed;
    //}
    public void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    //private void Draw()
    //{
    //    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 2);
    //    foreach (var hitCollider in hitColliders)
    //    {
    //        if (hitCollider.tag != this.tag)
    //        {
    //            timerEnded(hitCollider);

    //        }
    //    }
    //}

    //void timerEnded(Collider2D target)
    //{
    //    EnemyValues enemy = target.GetComponent<EnemyValues>();
    //    enemy.health -= damage;

    //}
}
