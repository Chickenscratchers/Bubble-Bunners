using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : StateMachineBehaviour
{
    private Transform targetTransform;
    private Vector3 endPosition;
    private float moveSpeed;
    private bool isMoving;
    private SpriteRenderer spriteRenderer;
    private ObjectPooler objectPooler;
    private bool isProjectile;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetTransform = animator.gameObject.transform;
        endPosition = new Vector3(targetTransform.position.x, targetTransform.position.y + 1f);
        moveSpeed = 1.1f;
        isMoving = true;
        spriteRenderer = animator.gameObject.GetComponent<SpriteRenderer>();
        isProjectile = animator.gameObject.CompareTag("Player Projectile");

        if (isProjectile)
        {
            objectPooler = animator.gameObject.GetComponent<Bubble>().objectPooler;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isMoving)
        {
            targetTransform.position = Vector3.MoveTowards(targetTransform.position, endPosition, moveSpeed * Time.deltaTime);
            Color spriteColor = spriteRenderer.color;
            spriteColor.a -= 0.003f;
            spriteRenderer.color = spriteColor;
        }


        // reached the end position
        if (Vector3.Distance(targetTransform.position, endPosition) < 0.01f)
        {
            isMoving = false;

            // not great but...
            if (isProjectile)
            {
                objectPooler.ReturnObjectToPool(animator.gameObject);
            }
            else
            {
                animator.gameObject.SetActive(false);
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Do Something");
        //animator.gameObject.GetComponent<Bubble>().objectPooler.ReturnObjectToPool(animator.gameObject);
    }
}
