using UnityEngine;
using System.Collections;

public class SkeletonWarrior : MonoBehaviour {

    [SerializeField] float      m_speed = 1.0f;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e")) 
            m_animator.SetTrigger("Death");
            
        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        //Attack
        else if(Input.GetMouseButtonDown(0)) {
            m_animator.SetTrigger("Attack");
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }
}
