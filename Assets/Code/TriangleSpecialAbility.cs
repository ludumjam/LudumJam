using UnityEngine;
using System.Collections;

public class TriangleSpecialAbility : MonoBehaviour, ISpecialAbility
{
    public float cooldownTime = 3f;
    public float duration = 0.4f;
    public float scaleMultiplier = 0.5f;
	public AudioClip ability;
	public AudioSource audio2;
    public ParticleSystem particleSystem;
    public int numParticles = 100;

    private float timeSinceLastUse = 0f;
    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isActive = false;
    private bool isShrunk = false;

    void Start()
    {
        CameraFollow.OnPlayerWentOutsideScreen += HandleOnDeathEvent;
    }

    void HandleOnDeathEvent()
    {
        enabled = false;
    }

    void OnDestroy()
    {
        CameraFollow.OnPlayerWentOutsideScreen -= HandleOnDeathEvent;
    }
	
    // Update is called once per frame
    void Update()
    {
        timeSinceLastUse += Time.deltaTime;

        if (timeSinceLastUse >= duration)
        {
            if (isActive)
            {
                isActive = false;
                // Reset
                targetScale = originalScale;
                originalScale = transform.localScale;
                StartCoroutine(Increase());
            }
        }
    }

    IEnumerator Shrink()
    {
        float scaleTimer = 0f;
        Vector3 newScale = originalScale;
        while (newScale.sqrMagnitude > targetScale.sqrMagnitude)
        {
            scaleTimer += 10 * Time.deltaTime;
            newScale = Vector3.Lerp(originalScale, targetScale, scaleTimer);
            transform.localScale = newScale;
            yield return null;
        }
        isShrunk = true;
    }

    IEnumerator Increase()
    {
        float scaleTimer = 0f;
        Vector3 newScale = originalScale;
        while (newScale.sqrMagnitude < targetScale.sqrMagnitude)
        {
            scaleTimer += 10 * Time.deltaTime;
            newScale = Vector3.Lerp(originalScale, targetScale, scaleTimer);
            transform.localScale = newScale;
            yield return null;
        }
        isShrunk = false;
    }

    public void TriggerAbility()
    {
        if (timeSinceLastUse >= cooldownTime && !isShrunk)
        {
			AudioSource audio = GetComponent<AudioSource>();
			audio.clip = ability;
			audio2.PlayOneShot(ability, 1.0F);
			timeSinceLastUse = 0f;
            // Setup effect
            originalScale = transform.localScale;
            targetScale = originalScale * scaleMultiplier;
            StartCoroutine(Shrink());
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
