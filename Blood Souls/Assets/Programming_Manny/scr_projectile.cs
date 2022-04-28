using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_projectile : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject player;
    public float bulletSpeed;
    public float timer;

    Vector3 dir;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform.position;
        dir = (target - transform.position).normalized;

        
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
        rb.velocity = new Vector2(dir.x * bulletSpeed * Time.deltaTime, dir.y * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerCombat>().playerHealth -= 5;
            Debug.Log("Take Damage");
            Destroy(this.gameObject);
        }
    }
}
