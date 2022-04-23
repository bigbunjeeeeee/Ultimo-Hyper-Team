using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    public Flock flock;

    public GameObject Target;
    TargetAvoid[] targetsAvoid;

    Vector2 velocity;
    public float speed = 0.009f;

    public float range = 3.0f;
    public float AvoidRange = 0.1f;

    public float AlignmentWeight;
    public float PositionWeight;
    public float SeparationWeight;
    public float targetWeight;
    public float targetAvoidWeight;
    public Vector2 ObstableWeight;

    public bool flockBoid;

    public bool followingTarget;
    public bool avoidingTarget;

    public float xMin, xMax, yMin, yMax;

    List<Boids> boids;

    void Start()
    {
        velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        velocity = velocity.normalized;

        if (Target == null)
        {
            followingTarget = false;
        }

        targetsAvoid = FindObjectsOfType<TargetAvoid>();
        if (targetsAvoid.Length == 0)
        {
            avoidingTarget = false;
        }

        if (!flockBoid)
        {
            boids = new List<Boids>(FindObjectsOfType<Boids>());
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
    }

    List<Boids> closeBoids()
    {
        List<Boids> CloseBoids = new List<Boids>();

        if (flockBoid)
        {
            for (int i = 0; i < flock.boids.Count; i++)
            {
                if (flock.boids[i].gameObject != gameObject)
                {
                    if (Vector3.Distance(flock.boids[i].transform.position, transform.position) < range)
                    {
                        CloseBoids.Add(flock.boids[i]);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < boids.Count; i++)
            {
                if (boids[i].gameObject != gameObject)
                {
                    if (Vector3.Distance(boids[i].transform.position, transform.position) < range)
                    {
                        CloseBoids.Add(boids[i]);
                    }
                }
            }
        }
        
        return CloseBoids;
    }

    Vector2 getAlignment(List<Boids> closeBoids)
    {
        Vector2 Alignment = new Vector2(0.0f, 0.0f);
        foreach (Boids b in closeBoids)
        {
            if (b.gameObject != gameObject)
            {
                Alignment += b.velocity;
            }
        }

        if (closeBoids.Count > 0)
        {
            Alignment = (Alignment / closeBoids.Count);
            Alignment.Normalize();
        }
        return Alignment;
    }

    Vector2 getSeparation(List<Boids> closeBoids)
    {
        Vector2 Separation = new Vector2(0.0f, 0.0f);
        int temp = 0;

        foreach (Boids b in closeBoids)
        {
            if (b.gameObject != gameObject)
            {
                if (Vector3.Distance(b.transform.position, transform.position) < AvoidRange)
                {
                    temp++;
                    Separation += new Vector2(b.transform.position.x, b.transform.position.y);
                }
            }
        }

        if (temp > 0)
        {
            Separation = (new Vector2(transform.position.x, transform.position.y) - (Separation / temp));
            Separation.Normalize();
        }
        return Separation;
    }

    Vector2 getPosition(List<Boids> closeBoids)
    {
        Vector2 Position = new Vector2(0.0f, 0.0f);

        foreach (Boids b in closeBoids)
        {
            if (b.gameObject != gameObject)
            {
                Position += new Vector2(b.transform.position.x, b.transform.position.y);
            }
        }

        if (closeBoids.Count > 0)
        {
            Position = (Position / closeBoids.Count) - new Vector2(transform.position.x, transform.position.y);
            Position.Normalize();
        }
        return Position;
    }

    Vector2 getObstacleAvoidance()
    {
        Vector2 ObstacleAvoidance = new Vector2(0.0f, 0.0f);
        if (followingTarget)
        {
            if ((transform.position.x - Target.transform.position.x) < xMin + AvoidRange)
            {
                ObstableWeight.x = Mathf.Abs((transform.position.x - Target.transform.position.x) - (xMin + AvoidRange)) / AvoidRange;
                ObstacleAvoidance.x = 1.0f;
            }
            else if ((transform.position.x - Target.transform.position.x) > xMax - AvoidRange)
            {
                ObstableWeight.x = Mathf.Abs((transform.position.x - Target.transform.position.x) - (xMax - AvoidRange)) / AvoidRange;
                ObstacleAvoidance.x = -1.0f;
            }

            if ((transform.position.y - Target.transform.position.y) < yMin + AvoidRange)
            {
                ObstableWeight.y = Mathf.Abs((transform.position.y - Target.transform.position.y) - (yMin + AvoidRange)) / AvoidRange;
                ObstacleAvoidance.y = 1.0f;
            }
            else if ((transform.position.y - Target.transform.position.y) > yMax - AvoidRange)
            {
                ObstableWeight.y = Mathf.Abs((transform.position.y - Target.transform.position.y) - (yMax - AvoidRange)) / AvoidRange;
                ObstacleAvoidance.y = -1.0f;
            }
        }
        else
        {
            if (transform.position.x < xMin + AvoidRange)
            {
                ObstableWeight.x = Mathf.Abs(transform.position.x - (xMin + AvoidRange)) / AvoidRange;
                ObstacleAvoidance.x = 1.0f;
            }
            else if (transform.position.x > xMax - AvoidRange)
            {
                ObstableWeight.x = Mathf.Abs(transform.position.x- (xMax - AvoidRange)) / AvoidRange;
                ObstacleAvoidance.x = -1.0f;
            }

            if (transform.position.y < yMin + AvoidRange)
            {
                ObstableWeight.y = Mathf.Abs(transform.position.y- (yMin + AvoidRange)) / AvoidRange;
                ObstacleAvoidance.y = 1.0f;
            }
            else if (transform.position.y > yMax - AvoidRange)
            {
                ObstableWeight.y = Mathf.Abs(transform.position.y- (yMax - AvoidRange)) / AvoidRange;
                ObstacleAvoidance.y = -1.0f;
            }
        }
        
        return ObstacleAvoidance;
    }

    Vector2 getFollowingTarget()
    {
        Vector2 targetFollow = new Vector2(0.0f, 0.0f);

        if (followingTarget)
        {
            float closestTargetDistance = Vector3.Distance(Target.transform.position, transform.position);
            targetFollow = Target.transform.position - transform.position;
            targetWeight = 0.0f;

            if (closestTargetDistance < 5.0f)
            {
                targetWeight = Mathf.Clamp(1 - (closestTargetDistance / 5.0f), 0.3f, 1.0f);
            }
        }
        return targetFollow;
    }

    Vector2 getAvoidingTarget()
    {
        Vector2 targetAvoid = new Vector2(0.0f, 0.0f);

        if (avoidingTarget)
        {
            int closestTargetAvoidIndex = 0;
            float closestTargetAvoidDistance = float.PositiveInfinity;

            for (int i = 0; i < targetsAvoid.Length; i++)
            {
                if (closestTargetAvoidDistance > Vector3.Distance(targetsAvoid[i].transform.position, transform.position))
                {
                    closestTargetAvoidIndex = i;
                    closestTargetAvoidDistance = Vector3.Distance(targetsAvoid[i].transform.position, transform.position);
                }
            }

            targetAvoid = -(targetsAvoid[closestTargetAvoidIndex].transform.position - transform.position);
            targetAvoidWeight = 0.0f;

            if (closestTargetAvoidDistance < 3.0f)
            {
                targetWeight = 0.0f;
                targetAvoidWeight = Mathf.Clamp(1 - (closestTargetAvoidDistance / 3.0f), 0.3f , 1.0f);
            }
        }

        return targetAvoid;
    }

    void Update()
    {
        List<Boids> CloseBoids = closeBoids();

        velocity += getPosition(CloseBoids) * PositionWeight * 0.003f;
        velocity += getSeparation(CloseBoids) * SeparationWeight * 0.005f;
        velocity += getAlignment(CloseBoids) * AlignmentWeight * 0.003f;

        velocity += getObstacleAvoidance() * ObstableWeight;
        velocity += getFollowingTarget() * targetWeight * 0.008f;
        velocity += getAvoidingTarget() * targetAvoidWeight * 0.012f;

        velocity = velocity.normalized;

        transform.rotation = Quaternion.Euler(new Vector3(transform.position.x, transform.position.y, 90.0f + Mathf.Atan2(transform.position.y + velocity.y - transform.position.y, transform.position.x + velocity.x - transform.position.x) * (180 / Mathf.PI)));

        transform.position += new Vector3 (velocity.x, velocity.y, 0.0f) * speed * Time.deltaTime;
    }
}
