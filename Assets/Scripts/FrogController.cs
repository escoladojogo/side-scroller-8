using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public Rigidbody2D frogBody;

    float secondsPassed = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        secondsPassed += Time.deltaTime;

        if (secondsPassed >= 2.0f)
        {
            frogBody.AddForce(new Vector2(0, 400));
            secondsPassed = 0;
        }
    }
}
