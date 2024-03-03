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
    //private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Awake()
    {
        // used to see which direction the player is facing
        playerMovement = player.GetComponent<PlayerMovement>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        DeathLayer = LayerMask.NameToLayer("Death Layer");
        DefaultLayer = LayerMask.NameToLayer("Default");
        //originalSprite = spriteRenderer.sprite;
        originalColor = spriteRenderer.color;
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

    // handle sounds and animation of bubble popping
    public void popBubble(bool popSound)
    {
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
    }

    public void resetBubble()
    {
        timer = 0;
        int bubbleSoundsIndex = Random.Range(0, 4);
        int facingRight = playerMovement.getFacingRight();
        
        // set the bubble's position, direction, layer and status to active
        isAlive = true;
        animator.SetBool("isAlive", true);

        currentSpeed = speed;
        gameObject.layer = DefaultLayer;
        transform.position = new Vector3(player.transform.position.x + facingRight * offset, player.transform.position.y, transform.position.z);
        setDirection(facingRight);

        //spriteRenderer.sprite = originalSprite;
        spriteRenderer.color = originalColor;

        bubbleSounds[bubbleSoundsIndex].Play();

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isAlive && collider.CompareTag("Enemy"))
        {
            transform.position = Vector3.MoveTowards(transform.position, collider.transform.position, 50 * Time.deltaTime);
            popBubble(true);
        }
    }

}
