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
    public List<AudioSource> bubbleSounds;
    public List<AudioSource> popSounds;
    public AudioSource dropSound;

    private PlayerMovement playerMovement;
    private Animator animator;

    private int direction = 1;
    private float timer;
    private float offset = 0.5f;
    private bool isAlive = true;
    private int DeathLayer;
    private int DefaultLayer;
    private float currentSpeed;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // used to see which direction the player is facing
        playerMovement = player.GetComponent<PlayerMovement>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        DeathLayer = LayerMask.NameToLayer("Death Layer");
        DefaultLayer = LayerMask.NameToLayer("Default");
    }

    void OnEnable()
    {
        resetBubble();
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

    public void popBubble(bool popSound)
    {
        timer = 0;
        animator.SetBool("isAlive", false);
        isAlive = false;
        gameObject.layer = DeathLayer;

        if (popSound)
        {
            int bubbleSoundsIndex = Random.Range(0, 3);
            popSounds[bubbleSoundsIndex].Play();
        }
        else
        {
            dropSound.Play();
        }

        // wait for the animation to end then return the bubble object to the pool
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime >= 0.5f)
        {
            // Animation has ended
            objectPooler.ReturnObjectToPool(gameObject);
        }
    }

    public void resetBubble()
    {
        int bubbleSoundsIndex = Random.Range(0, 4);
        int facingRight = playerMovement.getFacingRight();
        Color currentColor = spriteRenderer.color;

        // set the bubble's position, direction, layer and status to active
        isAlive = true;
        animator.SetBool("isAlive", true);

        currentSpeed = speed;
        gameObject.layer = DefaultLayer;
        transform.position = new Vector3(player.transform.position.x + facingRight * offset, player.transform.position.y, transform.position.z);
        setDirection(facingRight);


        currentColor.a = 1;
        spriteRenderer.color = currentColor;

        bubbleSounds[bubbleSoundsIndex].Play();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isAlive && collider.CompareTag("Enemy"))
        {
            transform.position = new Vector3(collider.gameObject.transform.position.x, collider.gameObject.transform.position.y);
            popBubble(false);
        }
    }

}
