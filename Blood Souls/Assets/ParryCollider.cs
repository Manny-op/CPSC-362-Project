using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryCollider : MonoBehaviour
{
    public PlayerCombat playerCombatScript;
    public PlayerMovement playerMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "EnemyAttack")
        {
            var target = collider.transform.parent.gameObject;
            Enemy EnemyCharacter = target.GetComponent<Enemy>();

            if (playerMovementScript.isParrying && EnemyCharacter.canBeParried)
            {
                Debug.Log("Parry");
                EnemyCharacter.animator.SetBool("Parried", true);
                EnemyCharacter.isParried = true;
                playerCombatScript.timeManager.doSlowMotion();
                playerCombatScript.activateRiposteWindow = true;
                return;
            }
        }
    }
}
