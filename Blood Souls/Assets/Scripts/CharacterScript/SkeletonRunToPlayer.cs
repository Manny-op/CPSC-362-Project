using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRunToPlayer : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 1f;
    Transform player;
    Rigidbody2D rb;

    Transform attackPoint;

    Enemy enemy;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       attackPoint = animator.GetComponentInChildren<APTransform>().attackPoint.transform;
       enemy = animator.GetComponent<Enemy>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Vector2 target = new Vector2(player.position.x, rb.position.y);
       Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        Debug.Log(Vector2.Distance(player.position, rb.position));
        rb.MovePosition(newPos);
        if(Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            Debug.Log(Vector2.Distance(player.position, rb.position));
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
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
