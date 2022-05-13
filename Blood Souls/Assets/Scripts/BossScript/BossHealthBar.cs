using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealthBar : MonoBehaviour
{

    private Coroutine regen;
    private WaitForSeconds regenTick = new WaitForSeconds(0.01f);
    public BossHealth bossHealth;
    private float maxHealth;

    //Use the Sliders UI so create public objects
    [HideInInspector] public Slider healthBar;
    private float hurtSpeed = 0.005f;

    public Image HEffectImage;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = this.GetComponent<Slider>();
        maxHealth = bossHealth.maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = bossHealth.health / maxHealth;
    }

    public IEnumerator HealthTickDown()
    {
        yield return new WaitForSeconds(2);

        while(HEffectImage.fillAmount > healthBar.value)
        {
            HEffectImage.fillAmount -= hurtSpeed;
            yield return regenTick;
        }
    }
}
