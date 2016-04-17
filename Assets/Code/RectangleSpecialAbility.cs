using UnityEngine;
using System.Collections;

public class RectangleSpecialAbility : MonoBehaviour, ISpecialAbility {

    public float cooldownTime = 3f;
    public float duration = 0.4f;
	public AudioClip ability;
	public AudioSource audio2;
    public ParticleSystem particleSystem;
    public int numParticles = 100;

    private float timeSinceLastUse = 0f;
    private float yVelocity;
    private float originalYVelocity;
    private new Rigidbody2D rigidbody;
    private bool isActive = false;
    private GameObject laser;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        CameraFollow.OnPlayerWentOutsideScreen += HandleOnDeathEvent;
        laser = transform.Find("Laser").gameObject;
        laser.SetActive(false);
    }

    void HandleOnDeathEvent() {
        enabled = false;
        laser.SetActive(false);
    }

    void OnDestroy() {
        CameraFollow.OnPlayerWentOutsideScreen -= HandleOnDeathEvent;
    }

    void Update() {
        timeSinceLastUse += Time.deltaTime;

        if (timeSinceLastUse >= duration) {
            if (isActive) {
                isActive = false;
                laser.SetActive(false);
            }
        }
        if (isActive) {
			
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right);
            if (hitInfo.rigidbody != null) {
                Destroy(hitInfo.rigidbody.gameObject);
            }
        }

    }

    public void TriggerAbility()
    {
        if (timeSinceLastUse >= cooldownTime) {
			AudioSource audio = GetComponent<AudioSource>();
			audio.clip = ability;
			audio2.PlayOneShot(ability, 1.0F);
            timeSinceLastUse = 0f;
            laser.SetActive(true);
            isActive = true;
            particleSystem.GetComponent<ParticleSystem>().Emit(numParticles);
        }
    }

    public float CoolDown
    {
        get
        {
            return timeSinceLastUse / cooldownTime;
        }
    }
}
