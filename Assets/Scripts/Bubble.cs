using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float speed;
    public float range;
    public float persistTime;
    public float bobbingMaxSpeed;
    public float bobbingSpeed;
    public float bobbingAccelerator;
    public GameObject player;
    public ObjectPooler objectPooler;
    public AnimationCurve movementSpeedCurve;

    private PlayerMovement playerMovement;
    private Animator animator;

    private int direction = 1;
    private float timer = 0;
    private float offset = 0.5f;
    private bool isAlive = true;
    private int DeathLayer;
    private int DefaultLayer;
    private float currentSpeed;

    void Awake()
    {
        // used to see which direction the player is facing
        playerMovement = player.GetComponent<PlayerMovement>();
        animator = gameObject.GetComponent<Animator>();
        DeathLayer = LayerMask.NameToLayer("Death Layer");
        DefaultLayer = LayerMask.NameToLayer("Default");
    }

    void OnEnable()
    {
        // set the bubble's position, direction, layer and status to active
        isAlive = true;
        currentSpeed = speed;
        gameObject.layer = DefaultLayer;
        int facingRight = playerMovement.getFacingRight();
        transform.position = new Vector3(player.transform.position.x + facingRight * offset, player.transform.position.y, transform.position.z);
        setDirection(facingRight);
    }

    void FixedUpdate()
    {

        if (isAlive)
        {
            // set the horizontal speed based on the AnimationCurve
            float currentHorizSpeed = movementSpeedCurve.Evaluate(timer) * currentSpeed * direction;
            Vector2 movement = new Vector2(currentHorizSpeed, bobbingSpeed) * Time.deltaTime;
            transform.Translate(movement);

        }
        else
        {
            // wait for the animation to end then return the bubble object to the pool
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.normalizedTime >= 0.6f)
            {
                // Animation has ended
                objectPooler.ReturnObjectToPool(gameObject);
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > range + persistTime)
        {
            // disappear after a certain amount of time
            timer = 0;
            objectPooler.ReturnObjectToPool(gameObject);
        }
        else if (timer > range)
        {
            // stop moving horizontally after a set time
            currentSpeed = 0.5f;
        }

        // bob the bubble up and down
        bobbingSpeed += bobbingAccelerator;
        if (Mathf.Abs(bobbingSpeed) >= bobbingMaxSpeed)
        {
            bobbingAccelerator = -bobbingAccelerator;
        }            
    }

    public void setDirection(int d)
    {
        direction = d;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAlive && collision.gameObject.CompareTag("Enemy"))
        {
            isAlive = false;
            animator.SetBool("isAlive", isAlive);
            transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
            gameObject.layer = DeathLayer;
            //objectPooler.ReturnObjectToPool(gameObject);
        }
    }

}
