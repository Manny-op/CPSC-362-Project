using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Collider2D parryCollider;
    public GameObject parryLocation;
    public TimeManager timeManager;

    public bool canRiposte = false;
    public bool activateRiposteWindow = false;
    Enemy enemyCharacter;
    public static PlayerCombat instance;

    public PlayerMovement movement; 
    public HealthBarScript Stats;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
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
        if (Input.GetButtonDown("Fire1") && playerStamina > 0 && !movement.isParrying)
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

    public void Parry()
    {
        if (Input.GetButtonDown("Fire2") && playerStamina > 0)
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

    public void dealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDmg(attackDamage);
            Debug.Log("We hit " + enemy.name);
        }
    }

    public void Execute()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDmg(executeDamage);
            Debug.Log("Executed " + enemy.name);
        }
    }

    public void takeDmg(int dmg)
    {
        playerHealth -= dmg;
        animator.SetTrigger("Hurt");
        //play hurt anim

        if(playerHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died");
        animator.SetTrigger("isDead");

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
