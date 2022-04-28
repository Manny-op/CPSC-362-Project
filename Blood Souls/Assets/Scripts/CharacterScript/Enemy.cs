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

    public bool isParried = false;

    public LayerMask PlayerLayer;
    float nextAttackTime = 0f;
    // Start is called before the first frame update
    public EnemyHealthBar healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        nextAttackTime += Time.deltaTime;
        if (nextAttackTime > 2)
        {
            animator.SetTrigger("Attack");
            nextAttackTime = 0f;
        }
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
        Debug.Log("Enemy died");
        animator.SetBool("isDead", true);
        animator.SetTrigger("Death");

        Destroy(gameObject);

    }
}
