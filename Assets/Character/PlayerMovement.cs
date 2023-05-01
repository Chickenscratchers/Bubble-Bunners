using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 speed;
    public Vector2 jumpHeight;

    private bool isGrounded = false; // Plyaer is grounded if box collider
    private Rigidbody2D rigidBody;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private float xLocalScale;
    private float inputX;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        xLocalScale = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(speed.x * inputX, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        
        if (inputX != 0)
        {
            anim.SetBool("Walk", true);
            if (inputX < 0)
            {
                transform.localScale = new Vector2(-1 * xLocalScale, transform.localScale.y);
            } else
            {
                transform.localScale = new Vector2(xLocalScale, transform.localScale.y);
            }
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        // Jump only w/ space key down + player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetBool("Walk", false);
            anim.SetTrigger("Jump");
            rigidBody.AddForce(jumpHeight, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks box collider collisions with tag as "Grounded" for jump enablement
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Checks box collider collisions with tag as "Grounded" for jump enablement
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
