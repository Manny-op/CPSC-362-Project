using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlip : MonoBehaviour
{
    public Transform player;
    public float oldPosition;
    public bool isFlipped = true;
    public Rigidbody2D rb;

    private void LateUpdate()
    {
        oldPosition = transform.position.x;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= 1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
