using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkIntoPortalBehavior : StateMachineBehaviour
{

    float speed = 8f;

    Rigidbody2D rb;

    float isInPortal = 1f;

    Transform insidePortal;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       insidePortal = GameObject.FindGameObjectWithTag("InsidePortal").transform;
       rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Vector2 target = new Vector2(insidePortal.position.x, rb.position.y);
       Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed*Time.deltaTime);
       rb.MovePosition(newPos);

       if(Vector2.Distance(insidePortal.position, rb.position) <= isInPortal)
       {
           SceneManager.LoadScene("BossCutscene");
       }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
       
    // }

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
