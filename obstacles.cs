/**
 * Project: CPSC 362 Project
 * 
 * file: obstacle.cs
 * Date: 03/06/2022
 * Revised: 03/14/2022
 * Programmer: Florentino Becerra
 * 
 * Description: A class to work with game elements
 * The elements represented by this class are obstacles
 * For this class, it will only deal with giving damage to the player
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacles : MonoBehaviour
{
	// Private data fields
	private const int DAMAGE_DEALT = 5;

	// Public member functions

	/** OnTriggerEnter
	 * This function is responsible for detecting when a player encounters
	 * a particular obstacle
	 * 
	 * @return: Void
	 */

	void OnTriggerEnter(Collider other)
	{

		// Is the collider a player?
		if ( other.gameObject.CompareTag( "Player" ) )
		{
			DealDamage( other );
		}

	}  // End of "OnTriggerEnter"


	/**  DealDamage
	 * This function is responsible for dealing damage to the player
	 * when the obstacle is interacted with
	 * 
	 * @return: void
	 */

	public void DealDamage( Collider player )
	{
		int currentHealth = player.GetHealth();  // Theoretically this player object has the health we need

		if ( currentHealth >= 0 )
		{
			currentHealth -= DAMAGE_DEALT;
			player.SetHealth(currentHealth);
		}

	}  // End of "DealDamage"

}  // end of "Obstacle" class
