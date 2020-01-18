using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    public int score = 10;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidBody;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("AddScore", score);
            Destroy(this.gameObject);
        }
    }

    void JumpRandomly()
    {
        rigidBody.AddForce(new Vector2(((Random.value - 0.5f) * 200), 400f));
    }
}
