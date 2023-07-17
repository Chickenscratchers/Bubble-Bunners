using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : MonoBehaviour
{

    public int numMoves;
    public AnimationCurve movementSpeedCurve;
    public ObjectPooler objectPooler;

    private float direction;
    private float timer;
    private float currentX;
    private int moves;
    private float curveTime;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    void Start()
    {
        direction = 1;
        timer = 0;
        moves = 0;
        curveTime = movementSpeedCurve[movementSpeedCurve.length - 1].time;

        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // determine how fast we're going based on animation curve
        float currentHorizSpeed = movementSpeedCurve.Evaluate(timer) * direction;

        // apply motion based on horizontal speed and animation curve
        Vector2 movement = new Vector2(currentHorizSpeed, 0) * Time.deltaTime;
        transform.Translate(movement);

        timer += Time.deltaTime;
           
        // reset the timer to go back to the beginning of the animation curve
        if (timer > curveTime)
        {
            timer = 0.01f;
            moves += 1;
        }

        // turn around after a set number of repetitions
        if (moves >= numMoves)
        {
            moves = 0;
            direction *= -1;
            sr.flipX = !sr.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player Projectile"))
        {
            objectPooler.ReturnObjectToPool(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
