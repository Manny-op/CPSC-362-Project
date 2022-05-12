using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlip : MonoBehaviour
{
    public float oldPosition;
    

    private void LateUpdate()
    {
        oldPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > oldPosition)
        {
            gameObject.transform.localRotation= new Quaternion(0, -180, 0, 0);
        }
        if (transform.position.x < oldPosition)
        {
            gameObject.transform.localRotation = new Quaternion(0, 180, 0, 0);
        }
    }
}
