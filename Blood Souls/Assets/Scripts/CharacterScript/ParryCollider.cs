using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryCollider : MonoBehaviour
{
    public PlayerCombat playerCombatScript;
    public PlayerMovement playerMovementScript;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "EnemyAttack")
        {
            var target = collider.transform.parent.gameObject;
            Enemy EnemyCharacter = target.GetComponent<Enemy>();

            if (playerMovementScript.isParrying && EnemyCharacter.canBeParried)
            {
                FindObjectOfType<AudioManager>().PlaySound("Parry");
                Debug.Log("Parry");
                EnemyCharacter.animator.SetBool("Parried", true);
                EnemyCharacter.isParried = true;
                playerCombatScript.timeManager.doSlowMotion();
                playerCombatScript.activateRiposteWindow = true;
                StartCoroutine(resetEnemyParry(EnemyCharacter));
                return;
            }
        }
    }

    public IEnumerator resetEnemyParry(Enemy enemy)
    {
        yield return new WaitForSeconds(1f);

        enemy.animator.SetBool("Parried", false);
        enemy.isParried = false;
    }
}
