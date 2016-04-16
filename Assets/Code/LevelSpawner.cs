﻿using UnityEngine;
using System.Collections.Generic;

public class LevelSpawner : MonoBehaviour {

    public float sequenceTriggerDistance;
    public string startSequence;

    private LevelSequence currentSequence;
    private float spawnTrigger;
    private float cameraHeight;

    void Start() {
        spawnTrigger = CameraY() - 1f;
        currentSequence = GetSequence(startSequence);
    }

    void Update() {
        if (CameraY() < spawnTrigger) {
            spawnTrigger = CameraY() - 1f;
            currentSequence.Tick();
        }
    }

    float CameraY() {
        return Camera.main.transform.position.y;
    }

    LevelSequence GetSequence(string key) {
        LevelSequence result = null;
        foreach (LevelSequence seq in GetComponents<LevelSequence>()) {
            if (seq.key == key) {
                result = seq;
            }
        }

        if (result == null) {
            Debug.LogError("No LevelSequence with key: " + key);
        }

        return result;
    }
}