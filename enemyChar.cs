/**
 * Project: Blood souls (CPSC 362 Semester Project)
 * 
 * File: enemy.cs
 * Date: 02/17/2022
 * Revised: 02/20/2022
 * Programmer: florentino Becerra
 * 
 * Description: Base class for enemy characters
 */

using System;  // Seems essential for now, like using namespace std


public class EnemyChar
{
	// Private data fields
	private int healthPoints;
	private int stamina;
	private int movementSpeed;
	private int attackDamage;
	private int charBehavior;

	// Constructors

	/**  Default constructor
	 * Default values for an enemy character
	 * 
	 */

	public EnemyChar()
		{
		healthPoints = 16;
		stamina = 10;
		movementSpeed = 5; 
		attackDamage = 6;  // Deals out 6 hit points from the looks of it to player
		charBehavior = 4;
		}

	// Accessor and mutator functions

	public void SetHealthPoints(int hp)
		{

		// prevent negative values
		if ( 0 > hp )
			{
			Math.Abs( hp );
			healthPoints = hp;
			}
		else if ( 0 == hp )
			{
			// Give them a fighting chance...
			hp += 5;
			healthPoints = hp;
			}
		else
			{
			healthPoints = hp;
			}

		}

	public void SetStamina( int st )
		{
		stamina = Math.Abs( st );
		}

	public void SetCharMovementSpeed( int speed )
		{
		movementSpeed = Math.Abs( speed );
		}

	public void SetAttackDamage( int damageVal )
		{
		attackDamage = Math.Abs( damageVal );
		}

	public void setCharBehavior( int behaviorVal )
	{
		charBehavior = Math.Abs( behaviorVal );
		}

	public int etHealth()
		{
		return healthPoints;
		}

	public int getStamina()
		{
		return stamina;
		}

	public int getMovementSpeed()
	{
		return movementSpeed;
		}

	public int getAttackDamage()
		{
		return attackDamage;
		}

	public int getCharBehavior()
		{
		return charBehavior;
		}

}  // End of class block
