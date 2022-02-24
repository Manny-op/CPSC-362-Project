using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
{
    private int PlayerHealth;
    private int PlayerStam;
    //maybe have the mana count here if we do go about implementing it.

    //Use the Sliders UI so create public objects
    public Slider healthBar;
    public Slider staminaBar;

    //here would be where to use class from player.

    // Start is called before the first frame update
    void Start()
    {
        //for now we should go off base 100 for health stamina and such. easy to work
        //with percentages
        //after player is done, will just pull from the player class values
        PlayerHealth = 100;
        PlayerStam = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = PlayerHealth;
        staminaBar.value = PlayerStam;

        //make test inputs to ensure the slider for health and stamina work
        if(Input.GetKeyDown(KeyCode.D)) //D for damage
        {
            HealthChange(25); //Using 25 as an arbitray number, when properly done will use the attack damage from enemy class
        }
        if(Input.GetKeyDown(KeyCode.E)) //E for exhaustion
        {
            StaminaUse(25);  
        }
        if(Input.GetKeyDown (KeyCode.H)) // H for heal
        {
            HealthChange(-25);
        }
        if(Input.GetKeyDown(KeyCode.R)) // R for rest
        {
            StaminaUse(-25);
        }
        


    }

    int HealthChange(int dam)
    {   
        //if damaging we do - the change
        // if we were to implement a healing mechanic we could do + or - negative damage (same thing but one would be easier to look out)
        //for now will leave all in one funtion that is currently set for damage but will easily be changed for helaing if neceessary

        PlayerHealth -= dam;
        return PlayerHealth;
    }

    int StaminaUse(int use)
    {
        //will essentially work the same as the health fucntion but for stamina, if possible could have both be 1 funcion and it makes checks for if its health or stamina
        //though this could be less efficient
        PlayerStam -= use;
        return PlayerStam;
    }
}
