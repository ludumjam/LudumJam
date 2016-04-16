﻿using UnityEngine;
using System.Collections.Generic;

public class LevelSpawner : MonoBehaviour {

    public int sequenceTriggerTicks = 50;

    private int currentSequence;
    private LevelSequence[] sequences;
    private float spawnTrigger;
    private float cameraHeight;
    private int sequenceTrigger;

    void Start() {
        spawnTrigger = CameraY() - 1f;
        sequences = GetComponents<LevelSequence>();
    }

    void Update() {
        if (CameraY() < spawnTrigger) {
            spawnTrigger = CameraY() - 1f;
            sequences[currentSequence].Tick();
            sequenceTrigger++;
            if (sequenceTrigger > sequenceTriggerTicks) {
                sequenceTrigger = 0;
                currentSequence++;
                currentSequence = currentSequence >= sequences.Length ? 0 : currentSequence;
            }
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