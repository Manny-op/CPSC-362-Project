using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat instance;
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

    public bool canReceiveInput;
    public bool InputReceived;

    void Awake()
    {
        instance = this;
        playerHealth = maxHealth;
        playerStamina = maxStamina;
    }

    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (Input.GetButtonDown("Fire1") && playerStamina > 0)
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

    public void dealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDmg(attackDamage);
            Debug.Log("We hit " + enemy.name);
        }
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
