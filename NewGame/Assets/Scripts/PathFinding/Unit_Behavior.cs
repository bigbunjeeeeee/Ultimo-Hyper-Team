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
    GameObject Target;
    bool Engage;

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
            else if (stats.PTeam == false)
            {
                SetTargetPosition(PBase.transform.position);
            }
            poslock = true;
        }

        if (Engage == false)
        {
            HandleMovement();
        }

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

    void OnTriggerStay2D(Collider2D collision)
    {

        //Debug.Log(collision);

        //if(IsItAnObject(collision))
        //{
        if (gameObject.GetComponent<EnemyValues>().PTeam != collision.GetComponent<baseAI>().Player)
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<baseAI>().health -= 25;
        }



    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsItAnObject(collision))
        {
            if (collision.gameObject.tag != "BottomBridgeCollision" && collision.GetComponent<EnemyValues>().PTeam != this.GetComponent<EnemyValues>().PTeam)
            {
                StopMoving();

                poslock = false;
                Engage = true;
                Target = collision.gameObject;

                InvokeRepeating("Damage", 0f, gameObject.GetComponent<EnemyValues>().attackspeed);
                
            }
        }
    }

    void Damage()
    {
       Target.GetComponent<EnemyValues>().health -= this.gameObject.GetComponent<EnemyValues>().damage;

        if (Target.GetComponent<EnemyValues>().health <= 0 || Target == null)
        {
            Engage = false;
            poslock = false;
        }

    }

}





