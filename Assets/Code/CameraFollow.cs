using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    void Update() {
        float x = transform.position.x;
        float y = Character.currentChild.transform.position.y;
        float z = transform.position.z;

        transform.position = new Vector3(x, y, z);
    }
}