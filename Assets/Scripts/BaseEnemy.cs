using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public bool isAlive = true;
    public ObjectPooler objectPooler;

    protected Animator an;

    protected void DeathSequence(Collision2D collision)
    {
        // play animation, stop moving, remove collider, destroy
        isAlive = false;
        an.SetBool("isAlive", isAlive);
        Destroy(gameObject, 0.5f);
        objectPooler.ReturnObjectToPool(collision.gameObject);
    }
}
