using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    public float riseY = 0.5f;
    public float speed = 0.02f;

    float startY;

    // Start is called before the first frame update
    void Start()
    {
        startY = this.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Rise());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("LoseALife");
        }
    }

    private IEnumerator Rise()
    {
        yield return new WaitForSeconds(0.2f);

        while (this.gameObject.transform.position.y < startY + riseY)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + speed, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
