using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public GameObject explosion;

    // Update is called once per frame
    void Update()
    {

    }

    void JumpedOnYou()
    {
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
