using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : BaseEnemy
{

    public int numMoves;
    public AnimationCurve movementSpeedCurve;

    private int moves;
    private float curveTime;

    void Start()
    {
        moves = 0;
        curveTime = movementSpeedCurve[movementSpeedCurve.length - 1].time;

        an = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            // determine how fast we're going based on animation curve
            float currentHorizSpeed = movementSpeedCurve.Evaluate(timer) * direction;

            // apply motion based on horizontal speed and animation curve
            rb.velocity = new Vector2(currentHorizSpeed, 0);

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

    }
}
