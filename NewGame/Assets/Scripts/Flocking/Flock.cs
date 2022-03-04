using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public GameObject prefab;

    public int amount;
    public float radius;
    public float scale;
    public float speed;

    public float xMin, xMax, yMin, yMax;

    public List<Boids> boids;

    void Awake()
    {
        for (int i = 0; i < amount; i++)
        {
            //random position
            Vector2 pos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

            GameObject b = Instantiate(prefab, transform.position, transform.rotation);
            Boids boid = b.GetComponent<Boids>();
            boid.flock = this;
            boid.followingTarget = true;
            boid.avoidingTarget = true;
            boid.Target = gameObject;
            boid.xMin = xMin;
            boid.xMax = xMax;
            boid.yMin = yMin;
            boid.yMax = yMax;

            boids.Add(boid);

        }
    }

}
