using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    Boids[] boids;

    Target[] targets;

    TargetAvoid[] targetsAvoid;

    Vector2 velocity;

    public float range = 3.0f;

    public float AvoidRange = 0.1f;

    public float speed = 0.009f;

    Vector2 Alignment;
    Vector2 Position;
    Vector2 Separation;
    Vector2 ObstacleAvoidance;
    Vector2 targetFollow;
    Vector2 targetAvoid;

    public float AlignmentWeight;
    public float PositionWeight;
    public float SeparationWeight;
    public float targetWeight;
    public float targetAvoidWeight;
    public Vector2 ObstableWeight;

    public bool followingTarget;

    public bool avoidingTarget;

    public float xMin, xMax, yMin, yMax;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        velocity = velocity.normalized;
        boids = FindObjectsOfType<Boids>();
        targets = FindObjectsOfType<Target>();
        targetsAvoid = FindObjectsOfType<TargetAvoid>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Boids> CloseBoids = new List<Boids>();
        Alignment = new Vector2(0.0f, 0.0f);
        Separation = new Vector2(0.0f, 0.0f);
        Position = new Vector2(0.0f, 0.0f);
        ObstacleAvoidance = new Vector2(0.0f, 0.0f);
        for (int i = 0; i < boids.Length; i++)
        {
            if (boids[i].gameObject != gameObject)
            {
                if (Vector3.Distance(boids[i].transform.position, transform.position) < range)
                {
                    Alignment += boids[i].velocity;
                    CloseBoids.Add(boids[i]);
                }
            }
        }

        int temp = 0;
        foreach (Boids b in CloseBoids)
        {
            if (b.gameObject != gameObject)
            {
                Position += new Vector2(b.transform.position.x, b.transform.position.y);
                if (Vector3.Distance(b.transform.position, transform.position) < AvoidRange)
                {
                    temp++;
                    Separation +=  new Vector2(b.transform.position.x, b.transform.position.y);
                }
            }
        }

        if (temp > 0)
        {
            Separation = new Vector2(transform.position.x, transform.position.y) - (Separation / temp);
            Separation.Normalize();
            //Debug.DrawLine(transform.position, new Vector3(transform.position.x + Separation.x, transform.position.y + Separation.y, 0.0f), Color.green);
        }

        if (CloseBoids.Count > 0)
        {
            Position = (Position / CloseBoids.Count) - new Vector2(transform.position.x, transform.position.y);
            Position.Normalize();
            //Debug.DrawLine(transform.position, new Vector3(transform.position.x + Position.x, transform.position.y + Position.y, 0.0f), Color.blue);
        }

        if (CloseBoids.Count > 0)
        {
            Alignment = (Alignment / CloseBoids.Count);
            Alignment.Normalize();
            //Debug.DrawLine(transform.position, new Vector3(transform.position.x + Alignment.x, transform.position.y + Alignment.y, 0.0f), Color.red);
        }

        ObstacleAvoidance = new Vector2(0.0f, 0.0f);

        if (transform.position.x < xMin + AvoidRange)
        {
            ObstableWeight.x = Mathf.Abs(transform.position.x - (xMin + AvoidRange)) / AvoidRange;
            ObstacleAvoidance.x = 1.0f;
        }
        else if (transform.position.x > xMax - AvoidRange)
        {
            ObstableWeight.x = Mathf.Abs(transform.position.x - (xMax - AvoidRange)) / AvoidRange;
            ObstacleAvoidance.x = -1.0f;
        }

        if (transform.position.y < yMin + AvoidRange)
        {
            ObstableWeight.y = Mathf.Abs(transform.position.y - (yMin + AvoidRange)) / AvoidRange;
            ObstacleAvoidance.y = 1.0f;
        }
        else if (transform.position.y > yMax - AvoidRange)
        {
            ObstableWeight.y = Mathf.Abs(transform.position.y - (yMax - AvoidRange)) / AvoidRange;
            ObstacleAvoidance.y = -1.0f;
        }

        int closestTargetIndex = 0;
        float closestTargetDistance = 100000.0f;
        if (followingTarget)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (closestTargetDistance > Vector3.Distance(targets[i].transform.position, transform.position))
                {
                    closestTargetIndex = i;
                    closestTargetDistance = Vector3.Distance(targets[i].transform.position, transform.position);
                }
            }

            targetFollow = targets[closestTargetIndex].transform.position - transform.position;
            targetWeight = 0.0f;

            if (closestTargetDistance < 5.0f)
            {
                targetWeight = Mathf.Max(Mathf.Min(1.0f, 1 - (closestTargetDistance / 5.0f)), 0.3f);
            }
        }

        int closestTargetAvoidIndex = 0;
        float closestTargetAvoidDistance = 100000.0f;
        if (avoidingTarget)
        {
            for (int i = 0; i < targetsAvoid.Length; i++)
            {
                if (closestTargetAvoidDistance > Vector3.Distance(targetsAvoid[i].transform.position, transform.position))
                {
                    closestTargetAvoidIndex = i;
                    closestTargetAvoidDistance = Vector3.Distance(targetsAvoid[i].transform.position, transform.position);
                }
            }

            targetAvoid = -(targetsAvoid[closestTargetIndex].transform.position - transform.position);
            targetAvoidWeight = 0.0f;

            if (closestTargetAvoidDistance < 3.0f)
            {
                targetWeight = 0.0f;
                targetAvoidWeight = Mathf.Max(Mathf.Min(1.0f, 1 - (closestTargetAvoidDistance / 3.0f)), 0.3f);
            }
        }

        if (avoidingTarget)
        {
            velocity += targetAvoid * targetAvoidWeight * 0.012f;
        }

        if (followingTarget)
        {
            velocity += targetFollow * targetWeight * 0.008f;
        }

        velocity += Position * PositionWeight * 0.003f;
        velocity += Separation * SeparationWeight * 0.003f;
        velocity += Alignment * AlignmentWeight * 0.003f;
        velocity += ObstacleAvoidance * ObstableWeight;

        velocity = velocity.normalized;

        transform.rotation = Quaternion.Euler(new Vector3(transform.position.x, transform.position.y, 90.0f + Mathf.Atan2(transform.position.y + velocity.y - transform.position.y, transform.position.x + velocity.x - transform.position.x) * (180 / Mathf.PI)));

        transform.position += new Vector3 (velocity.x, velocity.y, 0.0f) * speed * Time.deltaTime;
    }
}
