using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    public float dashDistance = 15f;
    bool isDashing;
    bool dashOnce = true;
    float doubleTapTime;
    KeyCode lastKeyCode;
    private Rigidbody2D m_Rigidbody2D;
    // Update is called once per frame

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }
        if (dashOnce){

        //dash left
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
                {
                    animator.SetBool("isDashing", true);
                    dashOnce = false;
                    StartCoroutine(Dash(-1f));
                    StartCoroutine(DashCooldown());
                    
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }
                lastKeyCode = KeyCode.A;
            }
            //dash right
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
                {   
                    animator.SetBool("isDashing", true);
                    dashOnce = false;
                    StartCoroutine(Dash(1f));
                    StartCoroutine(DashCooldown());
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }
                lastKeyCode = KeyCode.D;
            }
        }

        animator.SetFloat("yVel", m_Rigidbody2D.velocity.y);
    }

    public void onLanding()
    {
        animator.SetBool("isJumping", false);
    }

    void FixedUpdate()
    {
        //Move character
       if (!isDashing){
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
       }
    }

    IEnumerator Dash(float direction)
    {
        isDashing = true;
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
        m_Rigidbody2D.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = m_Rigidbody2D.gravityScale;
        m_Rigidbody2D.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        animator.SetBool("isDashing", false);
        m_Rigidbody2D.gravityScale = gravity;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        dashOnce = true;
    }
}
