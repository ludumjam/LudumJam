using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelSpawner : MonoBehaviour {

    public Transform mainCamera;
    public float triggerDistance;

    private float spawnTrigger;
    private float cameraHeight;
    private List<Transform> activeObstacles;

    void Start() {
        spawnTrigger = Character.currentChild.transform.position.y - triggerDistance;
        cameraHeight = mainCamera.GetComponent<Camera>().orthographicSize * 2f;
        activeObstacles = new List<Transform>();
    }

    void Update() {
        float y = Character.currentChild.transform.transform.position.y;

        if (y < spawnTrigger) {
            spawnTrigger = y - triggerDistance;
            SpawnObstacle();
        }

        List<Transform> marked = new List<Transform>();
        foreach (Transform t in activeObstacles) {
            if (t.position.y > y + cameraHeight) {
                marked.Add(t);
            }
        }

        foreach (Transform t in marked) {
            activeObstacles.Remove(t);
            Destroy(t.gameObject);
        }

    }

    private void SpawnObstacle() {

        GameObject go = (GameObject) Instantiate(Resources.Load("Prefabs/SquareObstacle"));

        float fallDistance = Character.currentChild.transform.transform.position.y;
        go.transform.position = new Vector3(0, fallDistance - cameraHeight, 0);
        Obstacle obst = go.GetComponent<Obstacle>();
        if (obst != null) {
            obst.Spawn();
        }

        activeObstacles.Add(go.transform);

    }
}