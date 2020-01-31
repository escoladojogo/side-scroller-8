using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossumController : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(-5, rigidBody.velocity.y);
    }
}
