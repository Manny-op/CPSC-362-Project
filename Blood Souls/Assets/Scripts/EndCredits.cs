using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndCredits : MonoBehaviour
{
    public void transitionEnd()
    {
        SceneManager.LoadScene("EndCredits");
    }
}
