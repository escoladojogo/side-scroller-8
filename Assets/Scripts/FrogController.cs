using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public float wait = 5.0f;
    public Rigidbody2D rigidBody;
    public Animator animator;

    private void Start()
    {
        StartCoroutine(WaitAndJump());
    }

    IEnumerator WaitAndJump()
    {
        while (true)
        {
            yield return new WaitForSeconds(wait);
            animator.SetBool("IsJumping", true);
            rigidBody.AddForce(new Vector2(0, 400f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("IsJumping", false);
    }
}
