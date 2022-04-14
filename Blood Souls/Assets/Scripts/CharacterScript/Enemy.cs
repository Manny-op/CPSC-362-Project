using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    // Start is called before the first frame update
    public EnemyHealthBar healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    
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

    void Die()
    {
        Debug.Log("Enemy died");
        animator.SetBool("isDead", true);
        animator.SetTrigger("Death");

        Destroy(gameObject);

    }
}
