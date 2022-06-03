using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPortal : MonoBehaviour
{
    [HideInInspector] public PlayerCombat playercombat;
    [HideInInspector] public PlayerMovement playerMovement;

    [HideInInspector] public CharacterController2D controller2D;

    [HideInInspector] public Animator animator;

    Color temp;
    GameObject portal;
    GameObject portalEntrance;

    GameObject[] portalFog;
    void Start()
    {
        playercombat = this.GetComponentInParent<PlayerCombat>();
        playerMovement = this.GetComponentInParent<PlayerMovement>();
        controller2D = this.GetComponentInParent<CharacterController2D>();
        animator = this.GetComponentInParent<Animator>();
        
        portal = GameObject.FindGameObjectWithTag("Entrance");
        portalEntrance = GameObject.FindGameObjectWithTag("InsidePortal");
        portalFog = GameObject.FindGameObjectsWithTag("PortalFog");
        GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach(GameObject go in gos)
        {
            if(go.layer == 6 && (go.tag == "Enemy" || go.tag== "RangedEnemy"))
            {
                playercombat.enemyCount++;
            }
        } 
        foreach(GameObject fog in portalFog)
            {
                temp = fog.GetComponent<SpriteRenderer>().color;
                temp.a = 0f;
                fog.GetComponent<SpriteRenderer>().color = temp;
            }
        temp.a = 255;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playercombat.enemyCount);
        if(playercombat.enemyCount <= 0 )
        {
            playercombat.killedAll = true;
        }
        checkEnablePortal();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BoxCollider2D[] array;

        if (other.gameObject.tag == "Entrance") { Debug.Log("inRange"); }
        if (other.gameObject.tag == "Entrance" && controller2D.m_Grounded && controller2D.m_FacingRight && playercombat.killedAll)
        {
            playercombat.enabled = false;
            playerMovement.enabled = false;
            controller2D.enabled = false;
            array = other.GetComponents<BoxCollider2D>();

            foreach(BoxCollider2D collider in array)
            {
                collider.enabled = false;
            }
            StartCoroutine(freezeX());
        }
    }

    public IEnumerator freezeX()
    {
        animator.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetFloat("Speed", 0);

        yield return new WaitForSeconds(0.8f);

        animator.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.SetTrigger("EnterPortal");
    }
    void checkEnablePortal()
    {

        if(!playercombat.killedAll)
        {
            return;
        }
        foreach(GameObject fog in portalFog)
        {
            fog.GetComponent<SpriteRenderer>().color = temp;
        }
    }
}
