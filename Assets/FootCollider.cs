using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollider : MonoBehaviour
{
    Rigidbody2D parentRigidBody;
    public float bubbleBounceHeight;

    private void Start()
    {
        parentRigidBody = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player Projectile" && parentRigidBody.position.y > collider.gameObject.transform.position.y && parentRigidBody.velocity.y < 0)
        {
            parentRigidBody.velocity = new Vector2(transform.localScale.x, bubbleBounceHeight);
            collider.GetComponent<Bubble>().popBubble();
        }
    }
}
