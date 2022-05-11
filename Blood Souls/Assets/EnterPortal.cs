using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPortal : MonoBehaviour
{
    [HideInInspector] public PlayerCombat playercombat;
    [HideInInspector] public PlayerMovement playerMovement;

    [HideInInspector] public CharacterController2D controller2D;

    [HideInInspector] public Animator animator;
    void Start()
    {
        playercombat = this.GetComponentInParent<PlayerCombat>();
        playerMovement = this.GetComponentInParent<PlayerMovement>();
        controller2D = this.GetComponentInParent<CharacterController2D>();
        animator = this.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BoxCollider2D[] array;

        if (other.gameObject.tag == "Entrance") { Debug.Log("inRange"); }
        if (other.gameObject.tag == "Entrance" && controller2D.m_Grounded && controller2D.m_FacingRight)
        {

            playercombat.enabled = false;
            playerMovement.enabled = false;
            controller2D.enabled = false;
            array = other.GetComponents<BoxCollider2D>();

            foreach(BoxCollider2D collider in array)
            {
                collider.enabled = false;
            }
            animator.SetTrigger("EnterPortal");
        }
    }
}
