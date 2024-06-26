using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public bool isAlive = true;

    protected Animator an;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected int DeathLayer;
    protected int DefaultLayer;
    protected Vector3 startPosition;
    protected float timer = 0;
    protected float direction = 1;
    private float gravityScale;
    private Color originalColor;

    private void Awake()
    {
        an = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        DeathLayer = LayerMask.NameToLayer("Death Layer"); // a layer that does not collide with anything
        DefaultLayer = LayerMask.NameToLayer("Default");
        startPosition = gameObject.transform.position;
        gravityScale = rb.gravityScale;
        an.SetBool("isAlive", isAlive);
        originalColor = sr.color;
    }

    // when an enemy is brought back from being dead, reset import variables
    private void OnEnable()
    {
        isAlive = true;
        an.SetBool("isAlive", isAlive);
        gameObject.layer = DefaultLayer;
        rb.gravityScale = gravityScale;
        sr.color = originalColor;
    }

    protected void DeathSequence()
    {
        // stop movement, play animation, change layer to prevent anymore collision, deactivate
        isAlive = false;
        an.SetBool("isAlive", false);
        gameObject.layer = DeathLayer;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        timer = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isAlive && collider.CompareTag("Player Projectile"))
        {
            DeathSequence();
        }
    }

    // reset the enemy back to original position and enable any defeated enemies
    public void Reset()
    {
        gameObject.transform.position = startPosition;
        timer = 0;
        direction = 1;
        ChildReset();
        sr.flipX = false;
        gameObject.SetActive(true);
    }

    // in case this enemy has specific variables to reset
    protected virtual void ChildReset() { }
}
