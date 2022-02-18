using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    Boids[] boids;

    Vector2 velocity;

    public float range = 3.0f;

    public float AvoidRange = 0.1f;

    public float speed = 0.009f;

    Vector2 Alignment;
    Vector2 Position;
    Vector2 Separation;
    Vector2 ObstacleAvoidance;

    public float AlignmentWeight;
    public float PositionWeight;
    public float SeparationWeight;
    public Vector2 ObstableWeight;

    public float xMin, xMax, yMin, yMax;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        velocity = velocity.normalized;
        boids = FindObjectsOfType<Boids>();
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
            Debug.DrawLine(transform.position, new Vector3(transform.position.x + Alignment.x, transform.position.y + Alignment.y, 0.0f), Color.red);
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


        velocity += Position * PositionWeight * 0.003f;
        velocity += Separation * SeparationWeight * 0.003f;
        velocity += Alignment * AlignmentWeight * 0.003f;
        velocity += ObstacleAvoidance * ObstableWeight;


        velocity = velocity.normalized;

        transform.position += new Vector3 (velocity.x, velocity.y, 0.0f) * speed * Time.deltaTime;
    }
}
