using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public bool isAlive = true;

    protected Animator an;
    protected int DeathLayer;
    protected Rigidbody2D rb;

    private void Awake()
    {
        an = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        DeathLayer = LayerMask.NameToLayer("Death Layer");
    }

    protected void DeathSequence(Collision2D collision)
    {
        // stop movement, play animation, change layer to prevent anymore collision, destroy
        isAlive = false;
        an.SetBool("isAlive", isAlive);
        gameObject.layer = DeathLayer;
        rb.gravityScale = 0;
        Destroy(gameObject, 0.5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        rb.velocity = Vector2.zero;
    }
}
