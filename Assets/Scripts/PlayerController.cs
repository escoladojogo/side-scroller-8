using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidBody;
    public Animator animator;
    public float runBoost = 500f;
    public float jumpBoost = 400f;
    public CapsuleCollider2D capsuleCollider;
    public GameObject groundTrigger;
    public float invincibilityTime = 5.0f;
    public Text livesCountText;
    public int livesCount = 3;
    public Text scoreText;

    int score;
    Vector3 startPosition;
    float horizontalMove;
    bool jump;
    int jumpCount;
    float verticalMove;
    float startColliderOffsetY;
    bool canClimb;
    bool climb;
    bool die;
    bool invincible;
    float invincibilityTimeLeft;
    Collider2D[] overlapColliders = new Collider2D[3];

    private void Start()
    {
        startPosition = this.gameObject.transform.position;
        startColliderOffsetY = capsuleCollider.offset.y;
        livesCountText.text = livesCount.ToString();
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

            if (verticalMove > 0)
            {
                if (canClimb)
                {
                    climb = true;
                    animator.SetBool("IsClimbing", true);
                }
                else
                {
                    capsuleCollider.OverlapCollider(new ContactFilter2D().NoFilter(), overlapColliders);

                    foreach (Collider2D col in overlapColliders)
                    {
                        if (col != null && col.tag == "Lever")
                        {
                            col.SendMessage("Pull");
                        }
                    }
                }
            }
            else
            {
                climb = false;
                animator.SetBool("IsClimbing", false);
            }
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
        float vy = 0;

        if (climb)
        {
            vy = verticalMove * Time.fixedDeltaTime * 500f;
        }
        else
        {
            vy = rigidBody.velocity.y;
        }


        rigidBody.velocity = new Vector3(horizontalMove * Time.fixedDeltaTime * runBoost, vy, 0);


        if (jump)
        {
            rigidBody.AddForce(new Vector2(0, jumpBoost));
            jump = false;
        }

        if (invincible)
        {
            invincibilityTimeLeft -= Time.fixedDeltaTime;

            if (invincibilityTimeLeft <= 0)
            {
                invincible = false;
                spriteRenderer.color = Color.white;
            }
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

            collision.gameObject.SendMessage("Die");
        }

        if (collision.gameObject.tag == "Stairs")
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stairs")
        {
            canClimb = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && die == false && jump == false && invincible == false)
        {
            StartCoroutine(MakePlayerDie());
        }

        if (collision.gameObject.tag == "Cherry")
        {
            collision.gameObject.SendMessage("Die");
            invincible = true;
            invincibilityTimeLeft = invincibilityTime;
            spriteRenderer.color = new Color(1.0f, 0, 0, 1.0f);
        }
    }

    void LoseALife()
    {
        if (die == false && invincible == false)
        {
            StartCoroutine(MakePlayerDie());
        }
    }

    private IEnumerator MakePlayerDie()
    {
        die = true;
        rigidBody.AddForce(new Vector2(0, jumpBoost));
        capsuleCollider.enabled = false;
        groundTrigger.SetActive(false);
        animator.SetBool("IsDying", true);

        yield return new WaitForSeconds(1.0f);

        if (livesCount > 0)
        {
            livesCount--;
            livesCountText.text = livesCount.ToString();

            animator.SetBool("IsDying", false);
            capsuleCollider.enabled = true;
            groundTrigger.SetActive(true);
            this.gameObject.transform.position = startPosition;
            die = false;
        }
    }

    void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }
}
