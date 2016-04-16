﻿using UnityEngine;
using System.Collections;

public class RectangleSpecialAbility : MonoBehaviour {

    public float cooldownTime = 3f;
    public float duration = 0.4f;
    public float downForce = 50f;

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

        if (Input.GetKeyDown(KeyCode.Space) && timeSinceLastUse >= cooldownTime) {
            timeSinceLastUse = 0f;
            laser.SetActive(true);
            isActive = true;
        }

        if (isActive) {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right);
            if (hitInfo.rigidbody != null) {
                hitInfo.rigidbody.AddForce(transform.right*200f);
            }
        }

    }
}