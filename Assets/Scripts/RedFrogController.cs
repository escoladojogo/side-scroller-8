using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFrogController : FrogController
{
    public GameObject fireball;
    public float waitToShoot = 2.0f;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(WaitAndShootFireball());
    }

    private IEnumerator WaitAndShootFireball()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitToShoot);
            Instantiate(fireball, this.transform.position, Quaternion.identity);
        }
    }
}
