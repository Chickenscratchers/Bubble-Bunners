using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : BaseEnemy
{

    public float horizSpeed;
    public float vertSpeed;
    public float vertHeight;
    public float timeTraveled;

    private float direction;
    private float timer;

    private SpriteRenderer sr;

    void Start()
    {
        direction = 1;
        sr = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        if (isAlive)
        {
            float currentHorizSpeed = horizSpeed * direction;
            float currentVertSpeed = Mathf.Sin(Time.time * vertSpeed) * vertHeight;

            // apply motion based on horizontal and vertical speed
            Vector2 movement = new Vector2(currentHorizSpeed, currentVertSpeed) * Time.deltaTime;
            transform.Translate(movement);


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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player Projectile"))
        {
            DeathSequence(collision);
        }
    }
}
