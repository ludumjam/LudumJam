using UnityEngine;

public class SquareObstacle : MonoBehaviour, Obstacle {

    public int frequency = 1;

    public void Spawn() {
        float rot = Random.Range(0, 360);
        float pos = Random.Range(-5f, 5f);

        transform.Translate(new Vector3(pos, 0, 0));
        transform.Rotate(new Vector3(0, 0, rot));
    }

    public int GetFrequency() {
        return frequency;
    }

}
