using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    private Coroutine tickDown;
    public Collider2D parryCollider;
    public GameObject parryLocation;
    public TimeManager timeManager;

    public bool isStanding;
    public bool canRiposte = false;
    public bool activateRiposteWindow = false;
    Enemy enemyCharacter;
    public static PlayerCombat instance;

    public PlayerMovement movement; 
    public HealthBarScript Stats;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;

    public LayerMask parryLayers;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public int attackDamage = 40;

    public int executeDamage = 100;

    public float playerHealth;
    
    public float maxHealth = 100;
    public float playerStamina;

    public float maxStamina = 100;
    float RiposteTimer = 0f;

    [HideInInspector]
    public bool canReceiveInput;
    [HideInInspector]
    public bool InputReceived;

    bool parrySuccess = false;

    public bool isInvincible = true;

    public bool airAttackOnce = true;
    void Awake()
    {
        instance = this;
        playerHealth = maxHealth;
        playerStamina = maxStamina;
        parryCollider.enabled = false;
    }

    void Update()
    {
        parryCollider.transform.position = parryLocation.transform.position;
        Attack();
        AirAttack();
        resetAirAttack();
        Parry();
        Riposte();
        if(activateRiposteWindow)
        {
            Debug.Log("Riposte Available");
            RiposteTimer += Time.deltaTime;
            if(RiposteTimer < 1.5)
            {
                canRiposte = true;
            }
            else if( RiposteTimer > 1.5)
            {
                Debug.Log("Riposte Unavailable");
                RiposteTimer = 0;
                activateRiposteWindow = false;
                canRiposte= false;
            }
        }
    }

    public void Attack()
    {
        if (Input.GetButtonDown("Fire1") && playerStamina > 0 && !movement.isParrying && !animator.GetBool("isJumping"))
        {
            if (canReceiveInput)
            {
                InputReceived = true;
                canReceiveInput = false;
            }
            else
            {
                return;
            }
        }

    }

    public void AirAttack()
    {
        if (airAttackOnce && Input.GetButtonDown("Fire2") && playerStamina > 0 && !movement.isParrying && animator.GetBool("isJumping"))
        {
            if (canReceiveInput)
            {
                InputReceived = true;
                canReceiveInput = false;
            }
            else
            {
                return;
            }
        }

    }

    public void resetAirAttack()
    {
        if(animator.GetComponent<CharacterController2D>().m_Grounded)
            {airAttackOnce = true;}
    }

    public void Parry()
    {
        if (Input.GetButtonDown("Fire2") && playerStamina > 0 && !animator.GetBool("isJumping"))
        {
            animator.SetTrigger("Parry");
        }
    }

    void Riposte()
    {
        if (Input.GetKeyDown(KeyCode.E) && canRiposte)
        {
            Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0.02f, 0.02f);
            Debug.Log("Riposted");
            animator.SetTrigger("Riposte");
            RiposteTimer = 0;
            activateRiposteWindow = false;
            canRiposte= false;
            resetParry();
        }
    }

    void enableParry()
    {
        movement.isParrying = true;
        parryCollider.enabled = true;
    }

    void resetParry()
    {
        parryCollider.enabled = false;
        movement.isParrying = false;
    }

    public void dealDamage(int dmg)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                enemy.GetComponent<Enemy>().takeDmg(dmg);
                Debug.Log("Hit " + enemy.name);
            }
            else if (enemy.gameObject.tag == "Boss")
            {
                enemy.GetComponent<BossHealth>().TakeDamage(dmg);
                Debug.Log("Hit " + enemy.name);
            }
        }
    }

    public void Execute(int dmg)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                enemy.GetComponent<Enemy>().takeDmg(dmg);
                Debug.Log("Executed " + enemy.name);
            }
            else if(enemy.gameObject.tag == "BossHead")
            {
                if (enemy.GetComponentInParent<BossHealth>().canExecute)
                {
                    enemy.GetComponentInParent<BossHealth>().beheaded = true;
                    enemy.GetComponentInParent<BossHealth>().TakeDamage(executeDamage);
                }
            }

        }
    }

    public void takeDmg(int dmg)
    {
        if (isInvincible) { return; }
        playerHealth -= dmg;
        
        animator.SetTrigger("Hurt");
        //play hurt anim

        if(tickDown!= null) { StopCoroutine(tickDown); }

        tickDown = StartCoroutine(Stats.HealthTickDown());
        if(playerHealth <= 0)
        {
            Die();
        }
    }

    public void enableIFrame()
    {
        isInvincible = true;
    }

    public void disableIframe()
    {
        isInvincible = false;
    }

    void Die()
    {
        Debug.Log("Player died");
        animator.SetTrigger("isDead");
        animator.SetBool("deadState", true);

    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    public void InputManager()
    {
        if (!canReceiveInput)
        {
            canReceiveInput = true;
        }
        else{
            canReceiveInput = false;
        }
    }


}
