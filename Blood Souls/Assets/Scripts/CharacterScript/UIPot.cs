using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPot : MonoBehaviour
{
    // Start is called before the first frame update
    private WaitForSeconds CDTick = new WaitForSeconds(0.01f);
    public Slider sliderCD;
    public Text potCount;

    public Image OnCD;

    public Image cdfill;

    PlayerCombat player;

    void Start()
    {
        var tempcdfill = cdfill.color;
        var tempOnCD = OnCD.color;
        sliderCD.value = 0;
        
        tempcdfill.a = 0f;
        tempOnCD.a = 0f;

        cdfill.color = tempcdfill;
        OnCD.color = tempOnCD;
        player = this.GetComponentInParent<PlayerCombat>();
        potCount.text = player.potionCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        potCount.text = player.potionCount.ToString();
    }

    public void beginCD()
    {
        var tempcdfill = cdfill.color;
        var tempOnCD = OnCD.color;
        tempcdfill.a = .51f;
        tempOnCD.a = .40f;

        cdfill.color = tempcdfill;
        OnCD.color = tempOnCD;
        StartCoroutine(coolDownTimer());

    }

    public IEnumerator coolDownTimer()
    {
        var tempcdfill = cdfill.color;
        var tempOnCD = OnCD.color;
        tempcdfill.a = 0f;
        tempOnCD.a = 0f;
        

        while(sliderCD.value < 1)
        {
            sliderCD.value += Time.fixedDeltaTime / 3;
            yield return CDTick;
        }
        
        cdfill.color = tempcdfill;
        OnCD.color = tempOnCD;
        sliderCD.value = 0;
        player.canDrinkPot = true;
    }

}
