﻿using UnityEngine;
using System.Collections;

public class SquareSpecialAbility : MonoBehaviour
{
    public float cooldownTime = 3f;
    public float duration = 0.4f;
    public float downForce = 50f;
    private float timeSinceLastUse = 0f;
    private float yVelocity;
    private float originalYVelocity;
    private new Rigidbody2D rigidbody;
    private bool isActive = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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

        if (Input.GetKeyDown(KeyCode.Space) && timeSinceLastUse >= cooldownTime)
        {
            timeSinceLastUse = 0f;
            originalYVelocity = rigidbody.velocity.y;
            yVelocity = originalYVelocity - downForce;
            isActive = true;
        }
        if (isActive)
        {
            Vector2 newVelocity = rigidbody.velocity;
            newVelocity.y = yVelocity;
            rigidbody.velocity = newVelocity;
        }
    }
}