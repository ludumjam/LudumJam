using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float maxDistanceToPlayer = 1f;
    private float currentYVelocity = 0f;

    void FixedUpdate() {
        float x = transform.position.x;
        float y = Character.currentChild.transform.position.y;
        float z = transform.position.z;

        if (transform.position.y > y + maxDistanceToPlayer)
        {
            // Camera is falling behind, time to catch up
            float delta = y - transform.position.y;
            float target = transform.position.y + delta;
            Vector3 newPosition = new Vector3(x, Mathf.SmoothDamp(transform.position.y, target, ref currentYVelocity, 0.05f), z);
            transform.position = newPosition;
        }
    }
}