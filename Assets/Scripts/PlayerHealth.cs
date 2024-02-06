using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    public GameObject healthParent;

    private readonly string EnemyTag = "Enemy";
    private bool invincible = false;
    [SerializeField] private float invincibilityDurationSeconds;
    private FlashBehavior flashBehavior;
    private PlayerMovement playerMovement;
    private RenderHealth heartRenderer;

    void Start()
    {
        heartRenderer = healthParent.GetComponent<RenderHealth>();
        flashBehavior = GetComponent<FlashBehavior>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!invincible) 
        { 
            if (collision.gameObject.tag == EnemyTag && collision.gameObject.GetComponent<BaseEnemy>().isAlive)
            {
                TakeDamage();
            }
        }
    }

    private IEnumerator enterTemporaryInvincibility()
    {
        invincible = true;
        //Debug.Log("Is invincible");

        yield return new WaitForSeconds(invincibilityDurationSeconds);

        invincible = false;
        //Debug.Log("No longer invincible");
    }

    private void TakeDamage()
    {
        health--;
        heartRenderer.LoseHeart();
        if (health <= 0)
        {
            //Debug.Log("Player died");
        }

        playerMovement.KnockbackPlayer();

        //Debug.Log("Start flash");
        flashBehavior.Flash();

        //Debug.Log("Start invincible");
        StartCoroutine(enterTemporaryInvincibility());
    }
}
