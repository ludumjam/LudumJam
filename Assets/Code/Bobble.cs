using UnityEngine;
using System.Collections;

public class Bobble : MonoBehaviour, Obstacle {

    public int frequency = 1;

    public int GetFrequency() {
        return frequency;
    }

    public void Spawn() {

        float x = Random.Range(-2f, 2f);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

    }

}
