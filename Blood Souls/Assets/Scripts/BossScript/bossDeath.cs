using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDeath : MonoBehaviour
{
    public GameObject playerUI;
    public GameObject bossUI;

    public AudioSource bgm;
    // Start is called before the first frame update
    void Start()
    {
        playerUI.SetActive(false);
        bossUI.SetActive(false);
        
        StartCoroutine(bgmfadeout());
    }

    IEnumerator bgmfadeout()
    {
        float startVolume = bgm.volume;
        while (bgm.volume > 0) {
            bgm.volume -= startVolume * Time.deltaTime / 10f;
 
            yield return null;
        }

        bgm.Stop();
        bgm.volume = startVolume;
    }
}
