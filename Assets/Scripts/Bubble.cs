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
    private int direction = 1;
    private float timer = 0;
    private float offset = 0.5f;

    void Awake()
    {
        // used to see which direction the player is facing
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void OnEnable()
    {
        // set the position and direction of the bubble
        int facingRight = playerMovement.getFacingRight();
        transform.position = new Vector3(player.transform.position.x + facingRight * offset, player.transform.position.y, player.transform.position.z);
        setDirection(facingRight);
    }

    void FixedUpdate()
    {
        float currentHorizSpeed = speed * direction;
        Vector2 movement = new Vector2(currentHorizSpeed, 0) * Time.deltaTime;
        transform.Translate(movement);
    }

    void Update()
    { 
        // disappear after a certain amount of time
        timer += Time.deltaTime;
        if (timer > range)
        {
            objectPooler.ReturnObjectToPool(gameObject);
            timer = 0;
        }
    }

    public void setDirection(int d)
    {
        direction = d;
    }

}
