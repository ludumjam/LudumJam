using UnityEngine;
using System.Collections;

public class CircleSpecialAbility : MonoBehaviour, ISpecialAbility {

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

    }

    void HandleOnDeathEvent() {
        enabled = false;
        Time.timeScale = 1.0f;
    }

    void OnDestroy() {
        CameraFollow.OnPlayerWentOutsideScreen -= HandleOnDeathEvent;
    }

    void Update() {
        timeSinceLastUse += Time.deltaTime;
       

        if (timeSinceLastUse >= duration) {
            if (isActive) {
                isActive = false;
                Physics2D.gravity = new Vector2(0, -10f);
                GetComponent<Rigidbody2D>().gravityScale = 1f;
                GetComponent<Rigidbody2D>().drag = 0f;
            }
        }
    }

    public void TriggerAbility() {
        if (timeSinceLastUse >= cooldownTime) {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = ability;
            audio2.PlayOneShot(ability, 1.0F);

            /*
            ParticleSystem psOne = GetComponent<ParticleSystem>();
            var em = psOne.emission;
            em.enabled = true;

            em.type = ParticleSystemEmissionType.Time;

            em.SetBursts(
                new ParticleSystem.Burst[]{
                new ParticleSystem.Burst(2.0f, 100),
                new ParticleSystem.Burst(4.0f, 100)
                });*/

            //ParticleSystem psOne = GetComponent<ParticleSystem>();
            //var em = psOne.emission;
            //em.enabled = true;
            //em.type = ParticleSystemEmissionType.Time;
            //em.SetBursts(new ParticleSystem.Burst[] {
            //        new ParticleSystem.Burst(2.0f, 1000)
            //    });

            particleSystem.GetComponent<ParticleSystem>().Emit(numParticles);

            timeSinceLastUse = 0f;
            isActive = true;
            Physics2D.gravity = new Vector2(0, 50f);
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            GetComponent<Rigidbody2D>().drag = 1000f;
        }
    }

    public float CoolDown {
        get {
            return timeSinceLastUse / cooldownTime;
        }
    }

}
