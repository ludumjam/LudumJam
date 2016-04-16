using UnityEngine;
using System.Collections;

public class WallElement : MonoBehaviour, Obstacle {

    public int frequency = 1;

    public int GetFrequency() {
        return frequency;
    }

    public void Spawn() {

        int r = Random.Range(0, 2);

        if (r == 0) {
            transform.Translate(new Vector3(7f, 0, 0));
        } else {
            transform.Translate(new Vector3(-7f, 0, 0));
        }

        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));

    }

}
