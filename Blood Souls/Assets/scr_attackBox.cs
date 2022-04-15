using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_attackBox : MonoBehaviour
{
    GameObject player;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerCombat>().playerHealth -= 5;
            Debug.Log("Take Damage");
        }
    }
}
