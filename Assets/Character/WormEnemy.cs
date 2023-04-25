using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : MonoBehaviour
{

    public float horizSpeed;
    public float turnaroundDistance;

    private float direction;
    private float distanceTraveled;
    private float currentX;

    private SpriteRenderer sr;

    void Start()
    {
        currentX = transform.position.x;
        direction = 1;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // going to change this out for an animation curve instead
        float currentHorizSpeed = direction * Mathf.Abs(Mathf.Sin(Time.time * horizSpeed));

        // apply motion based on horizontal speed
        Vector2 movement = new Vector2(currentHorizSpeed, 0) * Time.deltaTime;
        transform.Translate(movement);

        // track how long we've been moving in this direction
        distanceTraveled += Mathf.Abs(transform.position.x - currentX);

        currentX = transform.position.x;

        if (distanceTraveled > turnaroundDistance)
        {
            distanceTraveled = 0;
            direction *= -1;
            sr.flipX = !sr.flipX;
        }
    }
}
