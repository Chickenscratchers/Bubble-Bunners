using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Vector2 speed;
    public float timeTraveled;

    private float direction;
    private float timer;

    void Start()
    {
        direction = 1;
    }


    void Update()
    {
        Vector2 movement = new Vector2(speed.x * direction, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);

        if ()
    }
}
