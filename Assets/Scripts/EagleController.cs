using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour
{
    public float attackDistance = 1.0f;
    public float diveSpeed = 0.1f;

    float startHeight;
    GameObject player;
    bool attacking;
    float diveDepth;

    // Start is called before the first frame update
    void Start()
    {
        startHeight = this.gameObject.transform.position.y;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (player.transform.position.x > this.gameObject.transform.position.x - attackDistance &&
            player.transform.position.x < this.gameObject.transform.position.x + attackDistance &&
            attacking == false)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        attacking = true;
        diveDepth = player.transform.position.y;

        while (this.gameObject.transform.position.y > diveDepth)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - diveSpeed, 0);
            yield return new WaitForSeconds(0.01f);
        }

        while (this.gameObject.transform.position.y < startHeight)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + diveSpeed, 0);

            if (this.gameObject.transform.position.y > startHeight)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, startHeight, 0);
            }

            yield return new WaitForSeconds(0.01f);
        }

        attacking = false;
    }
}
