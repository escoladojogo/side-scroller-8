using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossumController : MonoBehaviour
{
    public float maxDist = 5.0f;
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;
    public float speedX = 200f;

    float startX;
    bool movingLeft;


    void Start()
    {
        startX = this.gameObject.transform.position.x;
        movingLeft = true;
    }

    void FixedUpdate()
    {
        if (movingLeft)
        {
            if (this.gameObject.transform.position.x < startX - maxDist)
            {
                movingLeft = false;
                spriteRenderer.flipX = true;
            }
            else
            {
                rigidBody.velocity = new Vector3(-speedX * Time.fixedDeltaTime, rigidBody.velocity.y, 0);
            }
        }
        else
        {
            if (this.gameObject.transform.position.x > startX + maxDist)
            {
                movingLeft = true;
                spriteRenderer.flipX = false;
            }
            else
            {
                rigidBody.velocity = new Vector3(speedX * Time.fixedDeltaTime, rigidBody.velocity.y, 0);
            }
        }
    }
}
