﻿using UnityEngine;
using System.Collections;

public class SquareSpecialAbility : MonoBehaviour, ISpecialAbility
{
    public float cooldownTime = 3f;
    public float duration = 0.4f;
    public float downForce = 50f;
	public AudioClip ability;
	public AudioSource audio2;
    public ParticleSystem particleSystem;
    public int numParticles = 100;

    private float timeSinceLastUse = 0f;
    private float yVelocity;
    private float originalYVelocity;
    private new Rigidbody2D rigidbody;
    private bool isActive = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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

    void Update()
    {
        timeSinceLastUse += Time.deltaTime;

        if (timeSinceLastUse >= duration)
        {
            if (isActive)
            {
                isActive = false;
                Vector2 newVelocity = rigidbody.velocity;
                newVelocity.y = originalYVelocity;
                rigidbody.velocity = newVelocity;
            }
        }
        if (isActive)
        {
            Vector2 newVelocity = rigidbody.velocity;
            newVelocity.y = yVelocity;
            rigidbody.velocity = newVelocity;
        }
    }

    void ISpecialAbility.TriggerAbility()
    {
        if (timeSinceLastUse >= cooldownTime)
        {
			AudioSource audio = GetComponent<AudioSource>();
			audio.clip = ability;
			audio2.PlayOneShot(ability, 1.0F);
			timeSinceLastUse = 0f;
            originalYVelocity = rigidbody.velocity.y;
            yVelocity = originalYVelocity - downForce;
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