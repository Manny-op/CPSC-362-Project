/**
 * Project: CPSC 362 Project (Spring 2022)
 * 
 * File: powerUp.cs
 * Date: 03/13/2022
 * Revised: 03/14/2022
 * Programmer: Florentino Becerra
 * 
 * Description: A simple class to work with power-up elements
 * Will ensure that the object is encountered and that the player will receive health
 * for the time being
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Seems like everything inherits from MonoBehavior to some degree
public class PowerUps : MonoBehaviour
{
	// Private data fields
	private const int HEALTH_BOOST = 5;

	/**  OnTriggerEnter
	 * This function is responsible for checking
	 * whether or not the player has interacted with a game element
	 * In this case, the power-up
	 * 
	 * @return: void
	 */

	void OnTriggerEnter( Collider other )
	{
		// Is the collider the player?
		if ( other.gameObject.CompareTag( "Player" ) )
		{
			PickUp( other );
		}

	}  // End of "OnTriggerEnter"


	/**  PickUp
	 * This function will be responsible for working with game power-ups
	 * For now it makes calls to the IncreaseHealth and Destroy methods
	 * 
	 * @param: A collider object that represents the player
	 * 
	 * @return: Void
	 */

	public void PickUp( Collider player )
	{
		IncreaseHealth( player );
		Destroy( gameObject );
	}  // End of "PickUp"


	/**  IncreaseHealth
	 * This function will increase the health of the player
	 * 
	 * @param: A collider object said to be the player
	 * 
	 * @return: void
	 */

	public void IncreaseHealth( Collider player )
	{
		// Assume there is a player object we can grab the health info from
		int currentHealth = player.GetHealth();

		if ( currentHealth < 100 )
		{
			currentHealth += HEALTH_BOOST;
			player.SetHealth(currentHealth);  // And symmetrically, to set new health
		}

	}  // End of "IncreaseHealth"

}  // End of "PowerUps" class
