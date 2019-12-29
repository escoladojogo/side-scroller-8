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
    public CapsuleCollider2D capsuleCollider;

    float horizontalMove;
    bool jump;
    int jumpCount;
    float verticalMove;
    float startColliderOffsetY;

    private void Start()
    {
        startColliderOffsetY = capsuleCollider.offset.y;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (horizontalMove < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalMove > 0)
        {
            spriteRenderer.flipX = false;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        verticalMove = Input.GetAxisRaw("Vertical");

        if (verticalMove < 0)
        {
            animator.SetBool("IsCrouching", true);
            capsuleCollider.offset = new Vector2(capsuleCollider.offset.x, startColliderOffsetY + 0.3f);
        }
        else
        {
            animator.SetBool("IsCrouching", false);
            capsuleCollider.offset = new Vector2(capsuleCollider.offset.x, startColliderOffsetY);
        }

        if (Input.GetButtonDown("Jump") && (jumpCount < 2))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
            jumpCount++;
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
        jumpCount = 0;

        if (collision.gameObject.tag == "Enemy")
        {
            jump = true;
            animator.SetBool("IsJumping", true);

            collision.gameObject.SendMessage("JumpedOnYou");
        }
    }
}
