using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
	public GameObject bossDeath;
	private Coroutine tickDown;

	[HideInInspector]public BossHealthBar healthBar;
	public int health = 0;
	public int maxHealth = 2000;

	public GameObject deathEffect;

	public bool isInvulnerable = false;

	public bool beheaded = false;

	public bool canExecute = false;

	public void Start()
	{
		health = maxHealth;
		healthBar = this.GetComponentInChildren<BossHealthBar>();
	}
	public void TakeDamage(int damage)
	{
		if(this.GetComponent<Animator>().GetBool("isDead")) { return; }
		if (isInvulnerable)
			return;

		health -= damage;
		if(tickDown!= null) { StopCoroutine(tickDown); }
		StartCoroutine(healthBar.HealthTickDown());
		if(!beheaded) { health = Mathf.Clamp(health, 1, maxHealth); }
		if(health == 1) { canExecute = true;  this.GetComponent<Animator>().SetBool("oneHP",true);}
	}

	void Die()
	{
		bossDeath.SetActive(true);
	}

	public IEnumerator waitToFade()
	{
		yield return new WaitForSeconds(3);
		Die();
	}

}