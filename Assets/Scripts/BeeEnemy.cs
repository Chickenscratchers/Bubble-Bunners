using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : BaseEnemy
{

    public float horizSpeed;
    public float vertSpeed;
    public float vertHeight;
    public float timeTraveled;

    private float sinWaveTimer = 0;
    private float currentHorizSpeed = 0;
    private float currentVertSpeed = 0;

    void Start()
    {
        direction = 1;
    }


    void FixedUpdate()
    {
        if (isAlive)
        {
            sinWaveTimer += Time.deltaTime;
            currentHorizSpeed = horizSpeed * direction;
            currentVertSpeed = Mathf.Sin(sinWaveTimer * vertSpeed) * vertHeight;

            // apply motion based on horizontal and vertical speed
            rb.velocity = new Vector2(currentHorizSpeed, currentVertSpeed);

            // track how long we've been moving in this direction
            timer += Time.deltaTime;


            // turn around if we've hit the set time
            if (timer > timeTraveled)
            {
                timer = 0;
                direction *= -1;
                sr.flipX = !sr.flipX;
            }
        }
    }

    // reset the position along the sin wave this bee is in
    protected override void ChildReset()
    {
        sinWaveTimer = 0;
    }
}
