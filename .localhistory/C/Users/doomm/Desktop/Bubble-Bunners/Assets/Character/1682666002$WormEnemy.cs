using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : MonoBehaviour
{

    public float horizSpeed;
    public float secondsMoving;
    public int numMoves;
    public AnimationCurve movementSpeedCurve;

    private float direction;
    private float timer;
    private float currentX;
    private int moves;

    private SpriteRenderer sr;

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
        // going to change this out for an animation curve instead
        float currentHorizSpeed = direction * movementSpeedCurve.Evaluate(timer);

        // apply motion based on horizontal speed
        Vector2 movement = new Vector2(currentHorizSpeed, 0) * Time.deltaTime;
        transform.Translate(movement);

        timer += Time.deltaTime;

        Debug.Log(timer);

        if (timer > secondsMoving * 1000)
        {
            timer = 0;
            moves += 1;
            Debug.Log("Restart timer");
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
