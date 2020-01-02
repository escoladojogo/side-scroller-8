using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public PlatformController platform;
    public Sprite notPulledSprite;
    public Sprite pulledSprite;

    bool isPulling;

    void Pull()
    {
        if (isPulling)
            return;

        StartCoroutine(StartPulling());
    }

    IEnumerator StartPulling()
    {
        isPulling = true;

        yield return new WaitForSeconds(0.2f);

        if (spriteRenderer.sprite == notPulledSprite)
        {
            spriteRenderer.sprite = pulledSprite;
            platform.active = true;
        }
        else
        {
            spriteRenderer.sprite = notPulledSprite;
            platform.active = false;
        }

        isPulling = false;
    }
}
