using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float runSpeed;
    public float minJumpHeight = 2;
    public float maxJumpHeight = 5;
    public float pressThreshold = 0.5f; // The threshold for considering the key press as a jump
    public float pressScale = 2f; // Scaling factor for the pressed intensity
    public float knockbackForce;
    public float knockbackTime;
    public float bubbleCooldown;
    public Vector2 size = new Vector2(1f, 1f);
    public ObjectPooler objectPooler;
    public GameManager gameManager;
    public bool isKnockedBack;

    private bool isGrounded = false; // Player is grounded if box collider
    private Rigidbody2D rigidBody;
    private Animator anim;
    private float xLocalScale;
    private float inputX;
    private int facingRight = 1; // start by facing right
    private float bubbleCooldownTimer;
    private float knockbackTimer;
    private KeyCode jumpKey = KeyCode.Space;
    private bool isJumping;
    private float jumpTimer = 0f;

    private Vector2 bottomRightCorner = new Vector2(0.5f, -1f);
    private Vector2 bottomLeftCorner = new Vector2(-0.5f, -1f);
    private Vector2 topRightCorner = new Vector2(0.5f, 1f);
    private Vector2 topLeftCorner = new Vector2(-0.5f, 1f);
    private Vector2 rightDirection = new Vector2(0.5f, 0);
    private Vector2 leftDirection = new Vector2(-0.5f, 0);


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        xLocalScale = transform.localScale.x;
        bubbleCooldownTimer = bubbleCooldown;
        knockbackTimer = knockbackTime;
    }

    private void FixedUpdate()
    {


        // prevent movement while being knocked back
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                isKnockedBack = false;
            }
        }
        else
        {
            // get arrow key input
            inputX = Input.GetAxis("Horizontal");
            rigidBody.velocity = new Vector2(inputX * runSpeed, rigidBody.velocity.y);
        }

        // prevent movement if we're running into the side of a wall
        CheckCornerCollisions();

    }

    void Update()
    {

        bubbleCooldownTimer += Time.deltaTime;

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

        CheckGrounded();

        CheckJump();

        // Shoot a bubble with C key
        if (Input.GetKey(KeyCode.C) && bubbleCooldownTimer > bubbleCooldown)
        {
            // get a bubble from the pool
            // the bubble automatically positions itself and moves
            objectPooler.GetPooledObject("Bubble");
            bubbleCooldownTimer = 0;
        }
    }

    void CheckGrounded()
    {
        // check if we're on the ground
        bool currentIsGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.4f, LayerMask.GetMask("Immovable"));

        if (!isGrounded && currentIsGrounded)
        {
            anim.SetBool("Jump", false);
        }

        isGrounded = currentIsGrounded;
    }

    void CheckJump()
    {
        // Jump only w/ space key down + player is grounded
        if (Input.GetKey(jumpKey) && isGrounded)
        {
            isJumping = true;
            jumpTimer = 0f;

            anim.SetBool("Walk", false);
            anim.SetBool("Jump", true);
        } else if (Input.GetKeyUp(jumpKey) || jumpTimer >= pressThreshold)
        {
            isJumping = false;
        }

        if (isJumping)
        {
            // Increase jump force gradually up to maxJumpForce
            float jumpStrength = minJumpHeight; //Mathf.Lerp(minJumpHeight, maxJumpHeight, jumpTimer / pressThreshold);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpStrength);
            jumpTimer += Time.deltaTime;
        }
    }

    // check for collisions in all directions so that player collider doesn't overlap with wall colliders
    // not my favorite solution but works for now
    void CheckCornerCollisions()
    {
        if (facingRight == 1)
        {
            CheckCollision(bottomRightCorner);
            CheckCollision(topRightCorner);
            CheckCollision(rightDirection);

        }
        else
        {
            CheckCollision(bottomLeftCorner);
            CheckCollision(topLeftCorner);
            CheckCollision(leftDirection);
        }

    }

    void CheckCollision(Vector2 direction)
    {
        // Cast a ray to check for collisions in the specified direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.4f, LayerMask.GetMask("Immovable"));

        // Debug visualization of the corner position
        Debug.DrawRay(transform.position, direction *0.4f, Color.black);

        // Check if the ray hits a collider
        if (hit.collider)
        {
            // prevent horizontal movement
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }

    public void KnockbackPlayer()
    {
        // Apply knockback force, start knockback timer
        rigidBody.velocity = new Vector2(-facingRight * knockbackForce, knockbackForce);
        isKnockedBack = true;
        knockbackTimer = knockbackTime;
    }

    public int getFacingRight()
    {
        return facingRight;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            gameManager.GameOver();
        }

    }
}
