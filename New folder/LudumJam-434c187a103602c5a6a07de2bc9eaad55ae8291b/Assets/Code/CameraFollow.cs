using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float deathOffset = 6f;
    public float maxDistanceToPlayer = 1f;
    public float speed = 0.2f;
    private float currentYVelocity = 0f;
    private float targetY;

    public delegate void OnDeathEvent();
    public static OnDeathEvent OnPlayerWentOutsideScreen;

    float CharY() {
        return Character.currentChild.transform.position.y;
    }

    float CharVelocityY() {
        return Character.currentChild.GetComponent<Rigidbody2D>().velocity.y;
    }

    void Update() {
        float camMove = speed * Time.deltaTime;
        float charMove = transform.position.y - CharY();

        if (camMove > charMove) {
            transform.Translate(new Vector3(0, -camMove, 0));
        } else {
            transform.Translate(new Vector3(0, -charMove, 0));
        }
    }
}