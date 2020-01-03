using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public GameObject startMarker;
    public GameObject endMarker;
    public float timeBetweenPositions = 1.0f;
    public bool active = true;

    Vector3 startPosition;
    Vector3 endPosition;
    bool increasing = true;
    float accumTime;

    private void Start()
    {
        startPosition = startMarker.transform.position;
        endPosition = endMarker.transform.position;
    }

    private void FixedUpdate()
    {
        if (!active)
            return;

        accumTime += Time.fixedDeltaTime;

        if (accumTime > timeBetweenPositions)
        {
            accumTime = timeBetweenPositions;
        }

        if (increasing)
        {
            rigidBody.MovePosition(Vector3.Lerp(startPosition, endPosition, accumTime / timeBetweenPositions));
        }
        else
        {
            rigidBody.MovePosition(Vector3.Lerp(startPosition, endPosition, 1.0f - (accumTime / timeBetweenPositions)));
        }

        if (accumTime >= timeBetweenPositions)
        {
            increasing = !increasing;
            accumTime = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0);
        Gizmos.DrawLine(startMarker.transform.position, endMarker.transform.position);
        Gizmos.DrawSphere(startMarker.transform.position, 0.1f);
        Gizmos.DrawSphere(endMarker.transform.position, 0.1f);
    }
}
