using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{
	public int attackDamage = 10;
	public int enragedAttackDamage = 20;

	public Vector3 attackOffset;
	public float attackRange = 10f;
	public LayerMask attackMask;

	public void Attack(int dmg)
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerCombat>().takeDmg(dmg);
		}
	}

	public void EnragedAttack(int dmg)
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerCombat>().takeDmg(dmg);
		}
	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}

	public void setAttackOffset(float Offset)
	{
		attackOffset.x = Offset;
	}

	public void setAttackRange(float range)
	{
		attackRange = range;
	}
}