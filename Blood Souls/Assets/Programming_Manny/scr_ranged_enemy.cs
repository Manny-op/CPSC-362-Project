using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ranged_enemy : MonoBehaviour
{
    public float maxTimer;
    public GameObject bullet;
    GameObject player;
    float timer;
    public Animator animator;

    [SerializeField]
    private int healthPoints;
    [SerializeField]
    private int stamina;
    [SerializeField]
    private int movementSpeed;
    [SerializeField]
    private int attackDamage;

    // Start is called before the first frame update
    void Start()
    {
        healthPoints = 16;
        stamina = 10;
        movementSpeed = 5;
        attackDamage = 6;  // Deals out 6 hit points from the looks of it to player

        timer = 0;

        player = GameObject.FindGameObjectWithTag("Player");

        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTimer)
        {
            timer = 0f;
            Shoot();
            //
        }
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(player.transform.position, transform.position) >= 6f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 7f * Time.deltaTime);
        }
    }

    void Shoot()
    {
        animator.SetBool("IsAttack", true);
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        GameObject enemyBullet = Instantiate(bullet, new Vector3(x, y, z), Quaternion.identity);
        //EnemyBullet emb = enemyBullet.GetComponent<EnemyBullet>();
    }
}
