/**
 * Project: CPSC 362 Project (Spring 2022)
 * 
 * File: mouseHover.cs
 * Programmer: Florentino Becerra
 * Date: 04/16/2022
 * 
 * Description: This script handles actions that will take place
 * when the mouse is hovering over controls of the main menu
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseHover : MonoBehaviour
{

	/**  Start
	 * This function ensures that something takes place at the time the game starts
	 * In this case, our main menu will have black colouring
	 * 
	 * @return: Void
	 */

	void Start()
	{
		renderer.material.color = Color.black;
	}  // end of "Start"


	/**  OnMouseEnter
	 * This function is responsible for when the mouse encounters the game element
	 * For the time being, it changes the color of the menu option to red.
	 * 
	 * @return: Void
	 */

	void OnMouseEnter()
	{
		renderer.material.color = Color.red;
	}  // end of "OnMouseEnter"


	/**  OnMouseExit
	 * This function is responsible to take actions when the mouse is no longer interacting with an element
	 * For the time being, it will default the menu option to not being coloured red
	 * 
	 * @return: Void
	 */

void OnMouseExit()
	{
		renderer.material.color = Color.black;
	}  // End of "OnMouseExit"

}  // End of "MouseHover" class
