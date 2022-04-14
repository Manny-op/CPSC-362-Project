/**
 * Project: CPSC 362 Project (Spring 2022)
 * 
 * File: gameHandler.cs
 * Programmer: Florentino Becerra
 * Date: 04/13/2022
 * 
 * Description: A basic handler to deal with the coins that can be interacted with
 * throughout the game by the player
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameHandler : MonoBehaviour
{
	public Text coinText;
	public int coins = 0;


	/**  Update
	 * This function is called once per frame
	 * Upon this call, it will contain the output of the number of coins
	 * 
	 * @return: Void
	 */

	void Update()
	{
		coinText.text = "Coins : " + coins;
	}  // End of "Update"

}  // End of "GameHandler" class
