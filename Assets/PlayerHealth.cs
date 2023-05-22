using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health;

    private readonly string EnemyTag = "Enemy";
    private bool invincible = false;
    [SerializeField] private float invincibilityDurationSeconds;
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!invincible) 
        { 
            if (collision.gameObject.tag == EnemyTag)
            {
                health--;
                if (health <= 0)
                {
                    Debug.Log("Player died");
                }
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
