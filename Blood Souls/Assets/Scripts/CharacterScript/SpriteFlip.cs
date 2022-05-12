using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlip : MonoBehaviour
{
    public float oldPosition;
    public Rigidbody2D rb;

    private void LateUpdate()
    {
        oldPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        var localVar = transform.InverseTransformDirection(rb.velocity);
        if(localVar.x > 0)
        {
            gameObject.transform.localRotation= new Quaternion(0, 180, 0, 0);
        }
        if (localVar.x < 0)
        {
            gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
