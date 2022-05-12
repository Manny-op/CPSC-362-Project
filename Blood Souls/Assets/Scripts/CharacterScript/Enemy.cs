using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerCombat player;
    public Animator animator;

    public bool isRiposted = false;
    public bool canBeParried = false;
    public int maxHealth = 100;
    public int currentHealth;

    public Transform attackPoint;
    public float attackRange = 0.5f;

    public int attackDamage = 15;
    private int movementSpeed = 5;
    public bool isParried = false;

    public bool isDetected = false;
    private GameObject playerposition;
    public LayerMask PlayerLayer;

    private float timer = 0;
    float nextAttackTime = 0f;
    // Start is called before the first frame update
    public EnemyHealthBar healthBar;
    void Start()
    {
        playerposition = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Vector2.Distance(player.transform.position, transform.position) >= 3f && !isParried && isDetected && !animator.GetBool("isDead"))
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);

        }
    }

        void Update()
    {

        timer += Time.deltaTime;
        if (timer >= 1 && !isParried && isDetected)
        {
            timer = 0;
            Attack();
        }
        
        
    }
        void Attack()
    {
        animator.SetTrigger("Attack");
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        //GameObject hitBox = Instantiate(attackBox, new Vector3(x, y, z), Quaternion.identity);
    }

    public void takeDmg(int dmg)
    {
        currentHealth -= dmg;
        healthBar.SetHealth(currentHealth,maxHealth);
        animator.SetTrigger("Hurt");
        //play hurt anim

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void dealDamage()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, PlayerLayer);

        foreach(Collider2D player in hitPlayer)
        {
            if (!isParried && player.tag == "Player"){
                player.GetComponent<PlayerCombat>().takeDmg(attackDamage);
                Debug.Log("Enemy hit " + player.name);
            }
        }
    }

    void enableBeParried()
    {
        canBeParried = true;
    }

    void disableBeParried()
    {
        canBeParried = false;
    }
    void Die()
    {
        animator.SetBool("Parried", false);
        Debug.Log("Enemy died");
        animator.SetBool("isDead", true);
        animator.SetTrigger("Death");

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
