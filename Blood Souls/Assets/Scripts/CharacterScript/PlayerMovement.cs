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

    bool crouch = false;

    public bool isInteracting = false;

    public float dashDistance = 15f;
    public float vdashDistance = 15f;
    public float rollDist = 10f;

    float gravityfordash = 0f;
    bool isDashing;
    bool isRolling;
    bool dashOnce = true;
    bool RollOnce = true;
   public bool isParrying = false;
    float doubleTapTime;
    KeyCode lastKeyCode;
    private Rigidbody2D m_Rigidbody2D;

    bool isAttacking = false;

    // Update is called once per frame

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        gravityfordash = m_Rigidbody2D.gravityScale;
    }
    void Update()
    {
        Interact();
        if(isAttacking || isParrying)
        {
            horizontalMove = 0;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        else if(!isAttacking || !isParrying)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        if (Input.GetButtonDown("Jump") && !isParrying && !isRolling)
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }
        if (Input.GetKeyDown(KeyCode.S) && !isParrying)
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.S) && !isParrying)
        {
            crouch = false;
        }
        if (dashOnce && !isRolling && !isParrying){
        //dash left
            if (Input.GetKeyDown(KeyCode.A) && (Input.GetAxisRaw("Horizontal") < 0 ) && !crouch && !isAttacking)
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
            if (Input.GetKeyDown(KeyCode.D) && (Input.GetAxisRaw("Horizontal") > 0 ) && !crouch && !isAttacking)
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

        if (RollOnce && !isDashing && !isParrying && !isAttacking && PlayerCombat.instance.playerStamina > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && (Input.GetAxisRaw("Horizontal") < 0) && controller.m_Grounded)
            {
                FindObjectOfType<AudioManager>().PlaySound("Roll");
                animator.SetBool("isRolling", true);
                RollOnce = false;
                StartCoroutine(Roll(-1f));
                StartCoroutine(RollCD());

            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && (Input.GetAxisRaw("Horizontal") > 0) && controller.m_Grounded)
            {
                FindObjectOfType<AudioManager>().PlaySound("Roll");
               animator.SetBool("isRolling", true);
               RollOnce = false;
               StartCoroutine(Roll(1f));
               StartCoroutine(RollCD());
            }
            
        }
        animator.SetFloat("yVel", m_Rigidbody2D.velocity.y);
    }

    public void Interact()
    {
        if(Input.GetKeyDown(KeyCode.F)) { isInteracting = true; Debug.Log(isInteracting);}
        else if (Input.GetKeyUp(KeyCode.F)) {isInteracting = false;}
    }

    public void setIsAttacking()
    {
        isAttacking = true;
    }

    public void resetAttack()
    {
        isAttacking = false;
    }

    public void swish()
    {
        FindObjectOfType<AudioManager>().PlaySound("swish");
    }

    public void slash()
    {
        FindObjectOfType<AudioManager>().PlaySound("slash");
    }

    public void onLanding()
    {
        animator.SetBool("isJumping", false);
    }

    public void onCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        //Move character
       if (!isDashing || !isRolling || !isParrying || !isAttacking){
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
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

    public void DashUp()
    {
        m_Rigidbody2D.velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.AddForce(new Vector2(0f, vdashDistance), ForceMode2D.Impulse);
        m_Rigidbody2D.gravityScale = 0;
    }

    public void DashDown()
    {
        m_Rigidbody2D.AddForce(new Vector2(0f, -vdashDistance), ForceMode2D.Impulse);
        m_Rigidbody2D.gravityScale = gravityfordash;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        dashOnce = true;
    }

    IEnumerator Roll(float direction)
    {
        isRolling = true;
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
        m_Rigidbody2D.AddForce(new Vector2(rollDist * direction, 0f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        isRolling = false;
        animator.SetBool("isRolling", false);
    }
    IEnumerator RollCD()
    {
        yield return new WaitForSeconds(1f);
        RollOnce = true;
    }

}
