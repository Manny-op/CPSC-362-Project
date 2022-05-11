using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
{
    public static HealthBarScript instance;
    private Coroutine tickDown;
    private Coroutine regen;
    private WaitForSeconds regenTick = new WaitForSeconds(0.01f);
    public PlayerCombat player;
    private float maxHealth;
    private float maxStamina;
    //maybe have the mana count here if we do go about implementing it.

    //Use the Sliders UI so create public objects
    public Slider healthBar;
    public Slider staminaBar;

    public Image HfillImage;
    public Image SfillImage;

    private float hurtSpeed = 0.005f;

    public Image HEffectImage;

    public Image SEffectImage;

    //here would be where to use class from player.

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = player.maxHealth;
        maxStamina = player.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // if (healthBar.value <= healthBar.minValue)
        // {
        //     HfillImage.enabled = false;
        // }
        // if (healthBar.value > healthBar.minValue && !HfillImage.enabled)
        // {
        //     HfillImage.enabled = true;
        // }

        // if (staminaBar.value <= staminaBar.minValue)
        // {
        //     SfillImage.enabled = false;
        // }
        // if (staminaBar.value > staminaBar.minValue && !SfillImage.enabled)
        // {
        //     SfillImage.enabled = true;
        // }

        float HfillValue = player.playerHealth / maxHealth;
        float SfillValue = player.playerStamina / maxStamina;

        healthBar.value = player.playerHealth / maxHealth;
        staminaBar.value = player.playerStamina / maxStamina;

        // if(HfillValue <= healthBar.maxValue / 3)
        // {
        //     HfillImage.color = Color.white;
        // }
        // else if(HfillValue > healthBar.maxValue / 3)
        // {
        //     HfillImage.color = Color.red;
        // }

        // if(SfillValue <= staminaBar.maxValue / 3)
        // {
        //     SfillImage.color = Color.white;
        // }
        // else if(SfillValue > staminaBar.maxValue / 3)
        // {
        //     SfillImage.color = Color.red;
        // }
        


    }
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while(player.playerStamina < maxStamina)
        {
            player.playerStamina += maxStamina / 100;
            SEffectImage.fillAmount = staminaBar.value;
            yield return regenTick;
        }
        regen = null;
    }

    public IEnumerator HealthTickDown()
    {
        yield return new WaitForSeconds(1);

        while(HEffectImage.fillAmount > healthBar.value)
        {
            HEffectImage.fillAmount -= hurtSpeed;
            yield return regenTick;
        }
    }
    public IEnumerator StaminaTickDown()
    {
        yield return new WaitForSeconds(1);

        while(SEffectImage.fillAmount > staminaBar.value)
        {
            SEffectImage.fillAmount -= hurtSpeed;
            yield return regenTick;
        }
    }
    

    // public float HealthChange(int dam)
    // {   
    //     //if damaging we do - the change
    //     // if we were to implement a healing mechanic we could do + or - negative damage (same thing but one would be easier to look out)
    //     //for now will leave all in one funtion that is currently set for damage but will easily be changed for helaing if neceessary

    //     PlayerHealth -= dam;
    //     return PlayerHealth;
    // }

    public void StaminaUse(int amt)
    {
        //will essentially work the same as the health fucntion but for stamina, if possible could have both be 1 funcion and it makes checks for if its health or stamina
        //though this could be less efficient
        if(player.playerStamina - amt >= 0)
        {
            player.playerStamina -= amt;

            if(tickDown!= null) { StopCoroutine(tickDown); }

            StartCoroutine(StaminaTickDown());

            if(regen != null) { StopCoroutine(regen); }

            regen = StartCoroutine(RegenStamina());
        }
    }
}
