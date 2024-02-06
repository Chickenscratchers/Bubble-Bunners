using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public GameObject healthParent;
    public GameObject gameManagerObject;

    private readonly string EnemyTag = "Enemy";
    private bool invincible = false;
    [SerializeField] private float invincibilityDurationSeconds;
    private FlashBehavior flashBehavior;
    private PlayerMovement playerMovement;
    private RenderHealth heartRenderer;
    private GameManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;
        heartRenderer = healthParent.GetComponent<RenderHealth>();
        gameManager = gameManagerObject.GetComponent<GameManager>();
        flashBehavior = GetComponent<FlashBehavior>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // hit an alive enemy
        if (collision.gameObject.tag == EnemyTag && collision.gameObject.GetComponent<BaseEnemy>().isAlive)
        {
            if (!invincible && currentHealth > 0)
            {
                TakeDamage();
            }

            // if you're invincible and not being knocked back
            else if (!playerMovement.isKnockedBack)
            {
                // stop player from pushing enemies here
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
        currentHealth--;
        heartRenderer.LoseHeart(); // toggle off a heart

        // taxes
        if (currentHealth <= 0)
        {
            gameManager.GameOver();
        }
        else
        {
            // knockback, flash, invincibility
            playerMovement.KnockbackPlayer();

            flashBehavior.Flash();

            StartCoroutine(enterTemporaryInvincibility());
        }
    }
}
