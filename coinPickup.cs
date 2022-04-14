/**
 * Project: CPSC 362 Project (Spring 2022)
 * 
 * File: coinPickup.cs
 * Programmer: Florentino Becerra
 * Date: 04/13/2022
 * 
 * Description: A basic script to work with coin collection
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinPickup : MonoBehaviour
{
	// Data fields
	public GameHandler coinHandler;


	/**  Update
	 * This function is called once per frame
	 * Upon each call, it will attempt to search for a coin, perhaps, within the scene
	 * 
	 * @return: void
	 */

	void Update()
	{
		// TODO: This may prove problematic, but for a prototype, it will suffice
		// Performance considerations may need to be explored

		coinHandler = GameObject.Find("Canvas").GetComponent<GameHandler>();
	}  // End of "Update"


	/**  OnTriggerEnter
	 * this function is responsible for updating our coin counter for the time being
	 * 
	 * @param: A collider object that we assume to be the player
	 * If not the player, we will simply make the coin disappear from the scene
	 * 
	 * @return: Void
	 */

	void OnTriggerEnter( Collider other )
	{
		// Check for the player tag
		if ( other.GameObject.CompareTag( "Player" ) )
		{
			coinHandler.coins++;
			Destroy( GameObject );
		}
		else
		{
			// No coins for anybody else, just make it disappear
			Destroy( GameObject );
		}

	}  // End of "OnTriggerEnter"

}  // End of "CoinPickup" class
