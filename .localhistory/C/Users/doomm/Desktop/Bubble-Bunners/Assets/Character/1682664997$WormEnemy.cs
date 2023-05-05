using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : MonoBehaviour
{

    public float horizSpeed;
    public float timeMoving;
    public int numMoves;
    public AnimationCurve movementSpeedCurve;

    private float direction;
    private float timer;
    private float currentX;
    private int moves;

    private SpriteRenderer sr;

    void Start()
    {
        currentX = transform.position.x;
        direction = 1;
        sr = GetComponent<SpriteRenderer>();
        timer = 0;
        numMoves = 0;
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

        currentX = transform.position.x;

        if (timer > timeMoving)
        {
            timer = 0;
            moves += 1;
        }

        if (moves >= numMoves)
        {
            moves = 0;
            direction *= -1;
            sr.flipX = !sr.flipX;
        }
    }
}
