using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform player;

    void Update() {
        float x = transform.position.x;
        float y = player.position.y;
        float z = transform.position.z;

        transform.position = new Vector3(x, y, z);
    }
}