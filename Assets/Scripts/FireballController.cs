using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float lifetime = 3.0f;
    public GameObject explosion;
    public float speed = 5f;

    private void FixedUpdate()
    {
        rigidBody.MovePosition(new Vector2(transform.position.x - (speed * Time.deltaTime), transform.position.y));
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            SelfDestroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "GroundTrigger")
        {
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("LoseALife");
        }

        SelfDestroy();
    }

    void SelfDestroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
