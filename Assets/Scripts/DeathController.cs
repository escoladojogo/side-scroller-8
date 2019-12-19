using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
