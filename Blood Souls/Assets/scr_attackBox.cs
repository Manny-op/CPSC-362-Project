using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_attackBox : MonoBehaviour
{
    GameObject player;
    public float timer;
    Rigidbody2D rb;
    Vector3 dir, target;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
        target = player.transform.position;
        dir = (target - transform.position).normalized;
        rb = GetComponent<Rigidbody2D>();
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

   private void FixedUpdate()
    {
        rb.velocity = new Vector2(dir.x * 200f * Time.deltaTime, dir.y * 200f * Time.deltaTime);
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
