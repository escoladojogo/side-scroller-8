using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBossController : BlueFrogController
{
    public GameObject fireball;
    public float waitToShoot = 2.0f;
    public GameObject diamond;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitAndShootFireballs());
    }

    IEnumerator WaitAndShootFireballs()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitToShoot);

            for (int i = 0; i < 3; i++)
            {
                Instantiate(fireball, this.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
            }

        }
    }

    protected override void Die()
    {
        base.Die();

        if (lives <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject clone = Instantiate(diamond, this.transform.position, Quaternion.identity);
                clone.SendMessage("JumpRandomly");
            }
        }
    }
}
