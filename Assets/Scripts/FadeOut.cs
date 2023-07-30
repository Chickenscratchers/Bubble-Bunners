using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : StateMachineBehaviour
{
    private Transform targetTransform;
    private Vector3 endPosition;
    private float moveSpeed;
    private bool isMoving;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetTransform = animator.gameObject.transform;
        endPosition = new Vector3(targetTransform.position.x, targetTransform.position.y + 1f);
        moveSpeed = 1.1f;
        isMoving = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isMoving)
        {
            targetTransform.position = Vector3.MoveTowards(targetTransform.position, endPosition, moveSpeed * Time.deltaTime);
        }


        if (Vector3.Distance(targetTransform.position, endPosition) < 0.01f)
        {
            isMoving = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    Debug.Log("Do Something");
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
