using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ranged_enem_anim_event : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StopAttack()
    {
        animator.SetBool("IsAttack", false);
    }

    void Destroy()
    {
        this.GetComponentInParent<EnemyRanged>().Destroy();
        Destroy(gameObject);
    }

}
