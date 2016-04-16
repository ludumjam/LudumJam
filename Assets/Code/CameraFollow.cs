using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float maxDistanceToPlayer = 1f;
    public float speed = 0.2f;
    private float currentYVelocity = 0f;

    void FixedUpdate() {
        float x = transform.position.x;
        float y = Character.currentChild.transform.position.y;
        float z = transform.position.z;
        Vector3 newPosition;
        if (transform.position.y > y + maxDistanceToPlayer)
        {
            // Camera is falling behind, time to catch up
            float delta = y - transform.position.y;
            float target = transform.position.y + delta;
            newPosition = new Vector3(x, Mathf.SmoothDamp(transform.position.y, target, ref currentYVelocity, 0.05f), z);
        }
        else
        {
            newPosition = transform.position;
            newPosition.y -= speed;
        }
        transform.position = newPosition;
    }
}