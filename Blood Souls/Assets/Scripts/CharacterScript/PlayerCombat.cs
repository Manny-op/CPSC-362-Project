using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public HealthBarScript Stats;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public int attackDamage = 40;

    public float playerHealth;
    
    public float maxHealth = 100;
    public float playerStamina;

    public float maxStamina = 100;

    void Awake()
    {
        playerHealth = maxHealth;
        playerStamina = maxStamina;
    }

    void Update()
    {
        if(Time.time >= nextAttackTime && playerStamina > 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                Stats.StaminaUse(10);
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDmg(attackDamage);
            Debug.Log("We hit " + enemy.name);
        }
    }

}
