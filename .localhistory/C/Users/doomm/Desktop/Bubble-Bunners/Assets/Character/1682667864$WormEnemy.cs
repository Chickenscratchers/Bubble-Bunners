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

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    void Start()
    {
        direction = 1;
        sr = GetComponent<SpriteRenderer>();
        timer = 0;
        moves = 0;
    }

    // Update is called once per frame
    void Update()
    {
        speed.x = direction * movementSpeedCurve.Evaluate(timer);

        // apply motion based on horizontal speed
        rb.velocity = speed;
        timer += Time.deltaTime;

        if (timer > movementSpeedCurve.length)
        {
            timer = 0;
            moves += 1;
            Debug.Log("Restart timer: " + movementSpeedCurve.length);
        }

        if (moves >= numMoves)
        {
            moves = 0;
            direction *= -1;
            sr.flipX = !sr.flipX;
            Debug.Log("Turn around");
        }
    }
}
