using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolBehaviour : StateMachineBehaviour
{

    private int waitTime;
    private int flipWaitTime;
    public float patrolSpeed;
    enemyController enemy;
    
    
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<enemyController>();
        enemy.RunPatrolWait();
        enemy.RunFlipWait();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.transform.position + groundDetection, patrolSpeed * Time.deltaTime);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, enemy.groundDetection.position, patrolSpeed * Time.deltaTime);
        
        //if (groundInfo.collider == false)
        //{
        //    enemy.Flip();
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

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
