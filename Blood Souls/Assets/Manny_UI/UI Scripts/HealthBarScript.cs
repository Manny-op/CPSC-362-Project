using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
{
    private int PlayerHealth;
    private int PlayerStam;
    //maybe have the mana count here if we do go about implementing it.


    // Start is called before the first frame update
    void Start()
    {
        //for now we should go off base 100 for health stamina and such. easy to work
        //with percentages
        PlayerHealth = 100;
        PlayerStam = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
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
