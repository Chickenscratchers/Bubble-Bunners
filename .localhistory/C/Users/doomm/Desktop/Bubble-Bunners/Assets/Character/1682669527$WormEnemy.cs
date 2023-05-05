using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : MonoBehaviour
{

    public int numMoves;
    public AnimationCurve movementSpeedCurve;

    private float direction;
    private float timer;
    private float currentX;
    private int moves;
    private Vector2 speed;
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
        speed.x = direction * movementSpeedCurve.Evaluate(timer);

        // apply motion based on horizontal speed
        rb.velocity = new Vector2(speed.x, 0);
        timer += Time.deltaTime;

        Debug.Log(rb.velocity);

        if (timer > curveTime)
        {
            timer = 0;
            moves += 1;
            Debug.Log("Restart timer: " + curveTime);
        }

        //if (moves >= numMoves)
        //{
        //    moves = 0;
        //    direction *= -1;
        //    sr.flipX = !sr.flipX;
        //    Debug.Log("Turn around");
        //}
    }
}
