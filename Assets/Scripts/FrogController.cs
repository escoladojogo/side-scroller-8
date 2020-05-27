using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public Rigidbody2D frogBody;
    public Animator animator;
    public float waitToJump = 4.0f;

    float secondsPassed = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        secondsPassed = secondsPassed + Time.deltaTime;

        Debug.Log("secondsPassed:" + secondsPassed + " waitToJump:" + waitToJump);

        if (secondsPassed >= waitToJump)
        {
            Debug.Log("Hora de pular!");

            animator.SetBool("IsJumping", true);
            frogBody.AddForce(new Vector2(0, 400));
            secondsPassed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("IsJumping", false);
    }
}
