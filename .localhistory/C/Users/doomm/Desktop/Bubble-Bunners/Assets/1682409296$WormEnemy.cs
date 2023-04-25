using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : MonoBehaviour
{

    public float horizSpeed;
    public float timeTraveled;

    private float direction;
    private float timer;

    private SpriteRenderer sr;

    void Start()
    {
        direction = 1;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentHorizSpeed = direction * Math.Abs(Mathf.Sin(Time.time * horizSpeed));
        // track how long we've been moving in this direction
        timer += Time.deltaTime;

        Vector2 movement = new Vector2(horizSpeed * direction )
    }
}
