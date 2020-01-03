using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFrogController : FrogController
{
    public int lives = 3;
    public GameObject explosion;
    public SpriteRenderer spriteRenderer;

    void Die()
    {
        lives--;
        StartCoroutine(ShowDamage());

        if (lives > 0)
        {
            return;
        }

        Instantiate(explosion, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    IEnumerator ShowDamage()
    {
        Color old = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = old;
    }
}
