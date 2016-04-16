using UnityEngine;
using System.Collections;

public class WallBobble : MonoBehaviour, Obstacle {

    public int frequeny = 1;

    public int GetFrequency() {
        return frequeny;
    }

    public void Spawn() {

        int r = Random.Range(0, 2);

        if (r == 0) {
            transform.Translate(new Vector3(4f, 0, 0));
        } else {
            transform.Translate(new Vector3(-4f, 0, 0));
        }

        float s = Random.Range(1f, 7f);
        transform.localScale = new Vector3(s, s, s);


    }

}
