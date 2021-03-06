using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_melee_Enem : MonoBehaviour
{
    private GameObject player;
    public GameObject attackBox;
    float timer;
    public float maxTimer;
    public Animator animator;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;

    [SerializeField]
    private int healthPoints;
    [SerializeField]
    private int stamina;
    [SerializeField]
    private int movementSpeed;
    [SerializeField]
    private int attackDamage;
    [SerializeField]
    private int charBehavior;


    // Start is called before the first frame update
    void Start()
    {
        healthPoints = 16;
        stamina = 10;
        movementSpeed = 5;
        attackDamage = 6;  // Deals out 6 hit points from the looks of it to player
        charBehavior = 4;
        timer = 0;


        //now get a reference player object.
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= maxTimer)
        {
            timer = 0;
            Attack();
        }
        if (healthPoints <=0)
        {
            Destroy(gameObject);
        }
        
        
    }
    // using fixed update as it will sync better compared to regular update
    private void FixedUpdate()
    {
        if (Vector2.Distance(player.transform.position, transform.position) >= 2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);

        }
    }

    //Create functions for taking damage, will use both melee and ranged for now and create a way to 
    // detect what the enemy is

    void takeDmg(int damage)
    {
        healthPoints -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")   //using Player rn so that the enemy gets damaged when touching player
            //when an attack has been made, will need to switch to the attack collider.
        {
            // eventually change variable from attack to damage to something like
            // collision.GetComponent<Player_Script>.AttackDamage();
            //TakeDamage(attackDamage);
            //will take damage from player attack
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        //GameObject hitBox = Instantiate(attackBox, new Vector3(x, y, z), Quaternion.identity);
    }

}
