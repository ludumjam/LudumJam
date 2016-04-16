using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    public float forceModifier = 0.5f;
    public float screenShakeIntensity = 1f;
    public float screenShakeDuration = 0.2f;
    private new Rigidbody2D rigidbody;
    private CameraFollow cameraFollow;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        CameraFollow.OnPlayerWentOutsideScreen += HandleOnDeathEvent;
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    void HandleOnDeathEvent()
    {
        enabled = false;
    }
	
    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.AddForce(Vector2.right * Input.GetAxis("Horizontal") * forceModifier, ForceMode2D.Impulse);
    }

    void OnDestroy()
    {
        CameraFollow.OnPlayerWentOutsideScreen -= HandleOnDeathEvent;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        cameraFollow.Shake(screenShakeIntensity, screenShakeDuration);
    }
}
