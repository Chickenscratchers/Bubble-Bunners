using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Vector2 speed;
    public float timeTraveled;

    private float direction;
    private float timer;

    private Animator anim;


    void Start()
    {
        direction = 1;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        // track how long we've been moving in this direction
        timer += Time.deltaTime;

        // apply motion based on speed
        Vector2 movement = new Vector2(speed.x * direction, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);

        // turn around if we've hit the set time
        if (timer > timeTraveled)
        {
            timer = 0;
            direction *= -1;
        }
    }
}
