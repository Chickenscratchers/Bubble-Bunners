using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 speed;
    public Vector2 jumpHeight;

    private bool isGrounded = false; // Plyaer is grounded if box collider
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        
        if (inputX != 0)
        {
            anim.SetBool("Walk", true);
            if (inputX < 0)
            {
                sr.flipX = true;
            } else
            {
                sr.flipX = false;
            }
        }
        else
        {
            anim.SetBool("Walk", false);
        }
        Vector2 movement = new Vector2(speed.x * inputX, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);

        // Jump only w/ space key down + player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetBool("Walk", false);
            anim.SetTrigger("Jump");
            rb.AddForce(jumpHeight, ForceMode2D.Impulse);
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
