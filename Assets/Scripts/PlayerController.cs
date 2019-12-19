using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidBody;
    public Animator animator;
    public float runBoost = 500f;
    public float jumpBoost = 400f;

    float horizontalMove;
    bool jump;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");

        if (horizontalMove < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalMove > 0)
        {
            spriteRenderer.flipX = false;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && animator.GetBool("IsJumping") == false)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector3(horizontalMove * Time.fixedDeltaTime * runBoost, rigidBody.velocity.y, 0);
        if (jump)
        {
            rigidBody.AddForce(new Vector2(0, jumpBoost));
            jump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("IsJumping", false);

        if (collision.name == "Frog")
        {
            jump = true;
            animator.SetBool("IsJumping", true);

            collision.gameObject.SendMessage("JumpedOnYou");
        }
    }
}
