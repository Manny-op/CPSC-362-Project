using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk_Back : StateMachineBehaviour
{
    public float speed = 2.5f;
    Transform player;
    Rigidbody2D rb;

    Boss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 newPos = Vector2.MoveTowards(rb.position, boss.initLocation, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if(Vector2.Distance(rb.position, boss.initLocation) <= 2)
        {
            animator.SetTrigger("idle_1");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

}
