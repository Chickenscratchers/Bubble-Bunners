using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float speed;
    public float range;
    public GameObject player;
    public ObjectPooler objectPooler;

    private PlayerMovement playerMovement;
    private Animator animator;

    private int direction = 1;
    private float timer = 0;
    private float offset = 0.5f;
    private bool isAlive = true;
    private int DeathLayer;
    private int DefaultLayer;

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
        gameObject.layer = DefaultLayer;
        int facingRight = playerMovement.getFacingRight();
        transform.position = new Vector3(player.transform.position.x + facingRight * offset, player.transform.position.y, transform.position.z);
        setDirection(facingRight);
        Debug.Log("Direction: " + facingRight);
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            float currentHorizSpeed = speed * direction;
            Vector2 movement = new Vector2(currentHorizSpeed, 0) * Time.deltaTime;
            transform.Translate(movement);
            Debug.Log(movement);

        }
        else
        {
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
        // disappear after a certain amount of time
        timer += Time.deltaTime;
        if (timer > range)
        {
            timer = 0;
            objectPooler.ReturnObjectToPool(gameObject);
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

    public void removeBubble()
    {
    }

}
