using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float runSpeed;
    public float jumpHeight;
    public ObjectPooler objectPooler;
    public float bubbleCooldown;
    public Vector2 size = new Vector2(1f, 1f);

    private bool isGrounded = false; // Plyaer is grounded if box collider
    private Rigidbody2D rigidBody;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private float xLocalScale;
    private float inputX;
    private int facingRight = 1; // start by facing right
    private float bubbleTimer;
    private Vector2 bottomRightCorner = new Vector2(0.5f, -1f);
    private Vector2 bottomLeftCorner = new Vector2(-0.5f, -1f);

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        xLocalScale = transform.localScale.x;
        bubbleTimer = bubbleCooldown;
    }

    private void FixedUpdate()
    {

        // check if we're on the ground
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.4f, LayerMask.GetMask("Ground"));

        // get arrow key input
        inputX = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(inputX * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = movement;

        CheckCollision(bottomRightCorner);
        CheckCollision(bottomLeftCorner);

    }

    void Update()
    {

        bubbleTimer += Time.deltaTime;

        if (inputX != 0)
        {
            anim.SetBool("Walk", true);
            if (inputX < 0)
            {
                transform.localScale = new Vector2(-1 * xLocalScale, transform.localScale.y);
                facingRight = -1;
            } else
            {
                transform.localScale = new Vector2(xLocalScale, transform.localScale.y);
                facingRight = 1;
            }
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        // Jump only w/ space key down + player is grounded
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            anim.SetBool("Walk", false);
            anim.SetTrigger("Jump");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpHeight);
        }

        // Shoot a bubble with C key
        if (Input.GetKey(KeyCode.C) && bubbleTimer > bubbleCooldown)
        {
            // get a bubble from the pool
            // the bubble automatically positions itself and moves
            objectPooler.GetPooledObject("Bubble");
            bubbleTimer = 0;
        }
    }

    void CheckCollision(Vector2 direction)
    {
        // Cast a ray to check for collisions in the specified direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.4f, LayerMask.GetMask("Ground"));

        // Debug visualization of the corner position
        Debug.DrawRay(transform.position, direction *0.4f, Color.black);

        // Check if there is a collider at the corner position
        if (hit.collider != null)
        {
            Debug.Log("Collision at the corner!");
            // Perform actions based on the corner collision

            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }

    public int getFacingRight()
    {
        return facingRight;
    }

}
