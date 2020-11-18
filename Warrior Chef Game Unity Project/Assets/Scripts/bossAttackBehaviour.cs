using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttackBehaviour : StateMachineBehaviour
{
    public int atkNum;
    bossController boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<bossController>();
        atkNum = animator.GetInteger("attackNum");
        boss.isAttacking = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (atkNum == 0)
        {
            if (animator.GetBool("isEnraged") == true)
            {
                boss.AnimDrop(20f);
                animator.SetInteger("attackNum", 5);
            }
            else
            boss.AnimDrop(15f);
        }
        else if (atkNum == 1)
        {
            //slow peck
        }
        else if (atkNum == 2)
        {
            //kick
            boss.AnimPlayerKnockback(15f);
            if (animator.GetBool("isEnraged") == true)
            {
                animator.SetInteger("attackNum", 3);
            }
        }
        else if (atkNum == 3)
        {
            //roll leap roll
            boss.AnimJump(15f);
            animator.SetInteger("attackNum", 0);
        }
        else if (atkNum == 4)
        {
            //fast peck
        }
        else if (atkNum == 5)
        {
            //roll
            boss.AnimDash(20f);
            if (animator.GetBool("isEnraged") == true)
            {
                animator.SetInteger("attackNum", 5);
            }
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.isAttacking = false;
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
