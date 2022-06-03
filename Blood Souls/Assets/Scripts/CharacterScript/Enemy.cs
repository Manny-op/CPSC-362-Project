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

    public bool isDetected = false;
    private GameObject playerposition;
    public LayerMask PlayerLayer;

    public bool Poise = false;
    bool doOnce = true;

    // Start is called before the first frame update
    public EnemyHealthBar healthBar;
    void Start()
    {
        playerposition = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame

    public void takeDmg(int dmg)
    {
        currentHealth -= dmg;
        healthBar.SetHealth(currentHealth,maxHealth);
        if(!Poise)
        {
            enablePoise();
            animator.SetTrigger("Hurt");
        }
        //play hurt anim

        if(currentHealth <= 0 && doOnce)
        {
            doOnce = false;
            
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
        PlayerCombat.instance.enemyCount--;
        animator.SetBool("Parried", false);
        Debug.Log("Enemy died");
        animator.SetBool("isDead", true);
        animator.SetTrigger("Death");

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void SkeletonFootstep()
    {
        FindObjectOfType<AudioManager>().PlaySound("SkelFootstep");
    }

    public void SkeletonHit()
    {
        FindObjectOfType<AudioManager>().PlaySound("SHurt");
    }
    public void SkeletonDeath()
    {
        FindObjectOfType<AudioManager>().PlaySound("SDeath");
    }
    public void SkeletonSword()
    {
        FindObjectOfType<AudioManager>().PlaySound("SkelSword");
    }

    public void banditSword()
    {
        FindObjectOfType<AudioManager>().PlaySound("BanditSword");
    }

    public void enablePoise()
    {
        Poise = true;

        StartCoroutine(PoiseTimer());
    }

    IEnumerator PoiseTimer()
    {
        yield return new WaitForSeconds(2f);

        Poise = false;
    }

}
