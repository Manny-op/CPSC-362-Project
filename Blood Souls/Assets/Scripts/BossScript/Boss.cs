using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = true;

    public Vector2 initLocation; 

    public Animator animator;

   [HideInInspector] public BossHealth Health;

    [HideInInspector] public Boss_Attack ba; 

    public void lookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= 1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }

    }

    public void startIdleTime()
    {
        StartCoroutine(idleTime());
    }
    IEnumerator idleTime()
    {
        yield return new WaitForSeconds(2);
        animator.SetTrigger("walk");
    }

    public void startInitialTime()
    {
        StartCoroutine(initialTime());
    }
    IEnumerator initialTime()
    {
        yield return new WaitForSeconds(1);
        animator.SetTrigger("skill_2");
    }

    public void chooseAttack(int choice)
    {
        Debug.Log(choice);
        if (choice == 0) { animator.SetTrigger("skill_1"); }
        else if (choice == 1) { animator.SetTrigger("skill_2"); }
    }

    // Start is called before the first frame update
    void Awake()
    {
        initLocation = new Vector2(this.transform.position.x, this.transform.position.y);
        Health = this.GetComponent<BossHealth>();
        ba = this.GetComponent<Boss_Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health.health <= 800)
		{
			animator.SetBool("isEnraged", true);
		}
        if (Health.health <= 0)
		{
             animator.SetBool("oneHP", false);
			animator.SetBool("isDead", true);
		}
    }
}
