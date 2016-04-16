using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float deathOffset = 6f;
    public float maxDistanceToPlayer = 1f;
    public float speed = 0.2f;
    private float currentYVelocity = 0f;
    private float targetY;
    private Coroutine cameraShakeRoutine;
    private bool isShaking = false;

    public delegate void OnDeathEvent();
    public static OnDeathEvent OnPlayerWentOutsideScreen;

    void Start()
    {
        cameraShakeRoutine = null;
        isShaking = false;
    }

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

        if (CharY() > transform.position.y + deathOffset)
        {
            if (OnPlayerWentOutsideScreen != null)
            {
                OnPlayerWentOutsideScreen();
            }
        }
    }

    public void Shake(float intensity, float duration)
    {
        if (!isShaking)
        {
            //cameraShakeRoutine = StartCoroutine(CameraShake(intensity, duration));
        }
    }

    IEnumerator CameraShake(float intensity, float duration)
    {
        isShaking = true;
        float timeSinceStart = 0f;
        Vector3 originalPosition = transform.localPosition;
        while (timeSinceStart < duration)
        {
            timeSinceStart += Time.deltaTime;
            Vector3 newPosition = transform.localPosition;
            newPosition.x = Random.Range(-intensity, intensity);
            transform.localPosition = newPosition;
            yield return null;
        }
        transform.localPosition = originalPosition;
        isShaking = false;
    }
}