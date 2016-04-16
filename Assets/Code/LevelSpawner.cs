using UnityEngine;
using System.Collections;
using System;

public class LevelSpawner : MonoBehaviour {

    public Transform player;
    public Transform mainCamera;
    public float triggerDistance;

    private float spawnTrigger;
    private float cameraHeight;

    void Start() {
        spawnTrigger = player.position.y - triggerDistance;
        cameraHeight = mainCamera.GetComponent<Camera>().orthographicSize * 2f;
    }

    void Update() {
        float y = player.transform.position.y;

        if (y < spawnTrigger) {
            spawnTrigger = y - triggerDistance;
            SpawnObstacle();
        }
    }

    private void SpawnObstacle() {

        GameObject go = (GameObject) Instantiate(Resources.Load("Prefabs/SquareObstacle"));

        float fallDistance = player.transform.position.y;
        go.transform.position = new Vector3(0, fallDistance - cameraHeight, 0);
        Obstacle obst = go.GetComponent<Obstacle>();
        if (obst != null) {
            obst.Spawn();
        }

    }
}