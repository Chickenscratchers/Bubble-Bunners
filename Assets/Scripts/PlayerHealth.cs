using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health;

    private readonly string EnemyTag = "Enemy";
    private bool invincible = false;
    [SerializeField] private float invincibilityDurationSeconds;
    private FlashBehavior flashBehavior;

    void Start()
    {
        flashBehavior = GetComponent<FlashBehavior>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!invincible) 
        { 
            if (collision.gameObject.tag == EnemyTag && collision.gameObject.GetComponent<BaseEnemy>().isAlive)
            {
                health--;
                if (health <= 0)
                {
                    Debug.Log("Player died");
                }
                Debug.Log("Start flash");
                flashBehavior.Flash();

                Debug.Log("Start invincible");
                StartCoroutine(enterTemporaryInvincibility());
                
            }
        }
    }

    private IEnumerator enterTemporaryInvincibility()
    {
        invincible = true;
        Debug.Log("Is invincible");

        yield return new WaitForSeconds(invincibilityDurationSeconds);

        invincible = false;
        Debug.Log("No longer invincible");
    }
}
