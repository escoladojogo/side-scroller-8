using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public GameObject explosion;

    void Die()
    {
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
