using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    private Coroutine tickDown;
    private Coroutine regen;
    public Collider2D parryCollider;
    public GameObject parryLocation;
    public TimeManager timeManager;

    public GameObject deathScene;
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
    public int attackDamage = 40;

    public int executeDamage = 100;

    public float playerHealth;
    
    public float maxHealth = 100;
    
    public float playerStamina;

    public float maxStamina = 100;

    public int potionCount = 3;
    public bool canDrinkPot = true;
    float RiposteTimer = 0f;

    [HideInInspector]
    public bool canReceiveInput;

    public bool canReceiveInputf2;
    [HideInInspector]
    public bool InputReceived;

    public bool InputReceivedf2;

    public bool isInvincible = true;

    public bool airAttackOnce = true;

    public bool killedAll = false;

    public int enemyCount = 0;
    [HideInInspector]public bool gothurt = false;

    [HideInInspector] public UIPot uiPot;


    float newHealth = 0;
    void Awake()
    {
        instance = this;
        playerHealth = maxHealth;
        playerStamina = maxStamina;
        parryCollider.enabled = false;
        newHealth = (0.33f * maxHealth);
        uiPot = this.GetComponentInChildren<UIPot>();
    }

    void Update()
    {
        if (!PauseMenu.gamePaused)
        {


                parryCollider.transform.position = parryLocation.transform.position;
                drinkPot();
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
            if (canReceiveInputf2)
            {
                InputReceivedf2 = true;
                canReceiveInputf2 = false;
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

    public void parrySound()
    {
        FindObjectOfType<AudioManager>().PlaySound("swish");
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

    public void resetParry()
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
                FindObjectOfType<AudioManager>().PlaySound("hitSound");
            }
            if (enemy.gameObject.tag == "RangedEnemy")
            {
                enemy.GetComponent<EnemyRanged>().takeDmg(dmg);
                Debug.Log("Hit " + enemy.name);
                FindObjectOfType<AudioManager>().PlaySound("hitSound");
            }
            else if (enemy.gameObject.tag == "Boss")
            {
                enemy.GetComponent<BossHealth>().TakeDamage(dmg);
                Debug.Log("Hit " + enemy.name);
                FindObjectOfType<AudioManager>().PlaySound("hitSound");
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
                FindObjectOfType<AudioManager>().PlaySound("ExecuteSound");
            }
            else if (enemy.gameObject.tag == "RangedEnemy")
            {
                enemy.GetComponent<EnemyRanged>().takeDmg(dmg);
                Debug.Log("Hit " + enemy.name);
                FindObjectOfType<AudioManager>().PlaySound("ExecuteSound");
            }
            else if(enemy.gameObject.tag == "BossHead")
            {
                if (enemy.GetComponentInParent<BossHealth>().canExecute)
                {
                    enemy.GetComponentInParent<BossHealth>().beheaded = true;
                    enemy.GetComponentInParent<BossHealth>().TakeDamage(executeDamage);
                    FindObjectOfType<AudioManager>().PlaySound("ExecuteSound");
                }
            }

        }
    }

    public void takeDmg(int dmg)
    {
        if (isInvincible || animator.GetBool("deadState")) { return; }
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        playerHealth -= dmg;
        FindObjectOfType<AudioManager>().PlaySound("hitSound");
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
        FindObjectOfType<AudioManager>().PlaySound("Died");
        animator.SetBool("deadState", true);
        animator.SetTrigger("isDead");
        this.GetComponent<PlayerCombat>().enabled = false;
        this.GetComponent<PlayerMovement>().enabled = false;
        this.GetComponent<CharacterController2D>().enabled = false;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    }

    public void drinkPot()
    {
        if (potionCount > 0 && Input.GetKeyDown(KeyCode.Q) && canDrinkPot)
        {
            FindObjectOfType<AudioManager>().PlaySound("drinkPot");
            canDrinkPot = false;
            Debug.Log("drank pot");
            potionCount--;
            if(regen != null) { StopCoroutine(regen); }

            regen = StartCoroutine(this.GetComponentInChildren<HealthBarScript>().RegenHealth(playerHealth + newHealth));
            playerHealth = Mathf.Clamp(playerHealth, 0, maxHealth);
            uiPot.beginCD();
        }
    }

    public void setHurt()
    {
        gothurt = true;
    }

    public void resetHurt()
    {
        gothurt = false;
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
    public void loadDeath()
    {
        StartCoroutine(FadetoBlack());
    }

    public IEnumerator FadetoBlack()
    {
        yield return new WaitForSeconds(1.5f);
        deathScene.SetActive(true);

    }
}
