using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.01f;
    public float slowDownLength = 2f;

    public PlayerCombat playercombatscript;

    void Update()
    {
        Time.timeScale += (1f/ slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        Time.fixedDeltaTime += (0.01f / slowDownLength) * Time.unscaledDeltaTime;
        Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0f, 0.02f);
    }

    public void doSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.unscaledTime * slowDownFactor;
        
    }


}
