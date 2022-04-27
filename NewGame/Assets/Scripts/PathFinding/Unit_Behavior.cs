using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Behavior : MonoBehaviour
{
    int currentPathIndex;
    List<Vector3> pathVectorList;
    float Speed;
    EnemyValues stats;
    GameObject EBase;
    GameObject PBase;
    public bool poslock = false;
    public float starttime;
    float time;
    public Vector3 testpos;
    public Vector2 size;

    // Start is called before the first frame update
    void Start()
    {
        stats = this.gameObject.GetComponent<EnemyValues>();
        EBase = GameObject.FindGameObjectWithTag("EnemyBase");
        PBase = GameObject.FindGameObjectWithTag("Base");
        Speed = stats.speed;
        time = starttime;


        //if (stats.PTeam == true)
        //{
        //    SetTargetPosition(EBase.transform.position);
        //}
        //else
        //{
        //    SetTargetPosition(PBase.transform.position);
        //}

    }
    void Update()
    {
        if (poslock != true)
        {
            if (stats.PTeam == true)
            {
                SetTargetPosition(EBase.transform.position);
            }
            else if(stats.PTeam == false)
            {
                SetTargetPosition(PBase.transform.position);
            }
            poslock = true;
        }

        HandleMovement();

    }

    void StopMoving()
    {
        pathVectorList = null;
    }



    void HandleMovement()
    {

        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                //Debug.Log(targetPosition);
                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position += moveDir * Speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = PathFinding.Instance.FindPath(GetPos(), targetPosition);

        //Debug.Log(targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

    //public void EnemyAttack()
    //{
    //    Collider2D HitCollider = Physics2D.OverlapBox(transform.position, size, 0f);
    //    if (HitCollider.GetComponent<EnemyValues>().PTeam != this.GetComponent<EnemyValues>().PTeam)
    //    {

    //    }
    //}

    void OnTriggerStay2D(Collider2D collision)
    {

        //Debug.Log(collision);



        if (gameObject.GetComponent<EnemyValues>().PTeam != collision.GetComponent<baseAI>().Player)
        {
            StopMoving();

            poslock = false;
            bool Engage = true;
            while (Engage == true)
            {
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    collision.gameObject.GetComponent<baseAI>().health -= this.gameObject.GetComponent<EnemyValues>().damage;
                    time = starttime;
                }
                if (collision.gameObject.GetComponent<baseAI>().health <= 0)
                {
                    Engage = false;
                    poslock = false;
                }
            }
        }
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "BottomBridgeCollision" && collision.GetComponent<EnemyValues>().PTeam != this.GetComponent<EnemyValues>().PTeam)
        {
            StopMoving();

            poslock = false;
            bool Engage = true;
            while (Engage == true)
            {
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    collision.gameObject.GetComponent<EnemyValues>().health -= this.gameObject.GetComponent<EnemyValues>().damage;
                    time = starttime;
                }
                if (collision.gameObject.GetComponent<EnemyValues>().health <= 0)
                {
                    Engage = false;
                    poslock = false;
                }
            }
        }
    }
}




