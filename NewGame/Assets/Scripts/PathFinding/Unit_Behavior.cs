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
    bool poslock = false;
    // Start is called before the first frame update
    void Start()
    {
        stats = this.gameObject.GetComponent<EnemyValues>();
        EBase = GameObject.FindGameObjectWithTag("EnemyBase");
        PBase = GameObject.FindGameObjectWithTag("Base");
        Speed = stats.speed;
        

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
            else
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
        if(pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * Speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if(currentPathIndex >= pathVectorList.Count)
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

        if(pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }
}
