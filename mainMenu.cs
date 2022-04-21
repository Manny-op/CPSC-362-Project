/**
 * Project: CPSC 362 Project (Spring 2022)
 * 
 * File: mainMenu.cs
 * Programmer: Florentino Becerra
 * Date: 04/19/2022
 * 
 * Description: This class is responsible for the buttons available in the main menu
 * Has a method to perform actions when a user clicks on a menu option
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
	// Data fields
	public bool isStartGame;
	public bool isQuitGame;

	/**  OnMouseUp
	 * This function is responsible for invoking an action when the user clicks on a menu option
	 * 
	 * @return: Void
	 */

	void OnMouseUp()
	{
		// Did the user want to start the game?
		if ( isStartGame )
		{
			// Level 1, or scene 1, should be the next one
			// Scene 0, or level 0 I suppose, would be our main menu
			Application.LoadLevel(1);
		}

		// Did the user wish to quit?
		if ( isQuitGame )
		{
			// Keep note of whether we are in Unity's editor mode or if we are in a fully built game
			// use preprocessor directives to make the check and perform the appropriate quit action

			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#endif
			// Else, just quit from the actual application
			Application.Quit();
		}

	}  // End of "OnMouseUp"

}  // End of "MainMenu" class
