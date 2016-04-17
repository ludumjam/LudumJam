using UnityEngine;
using System.Collections.Generic;

public class LevelSequence : MonoBehaviour {

    public string key;
    public GameObject[] obstacles;

    private List<Transform> activeObstacles;
    private int[] obstacleTriggers;
    private float camHeight;

    void Start () {
        activeObstacles = new List<Transform>();
        InitObstacleTriggers();


        camHeight = Camera.main.orthographicSize * 4f;
    }

    private void InitObstacleTriggers() {
        obstacleTriggers = new int[obstacles.Length];
        for (int i = 0; i < obstacleTriggers.Length; i++) {
            GameObject obstacleGO = obstacles[i];
            Obstacle obstacle = obstacleGO.GetComponent<Obstacle>();
            int freq = obstacle == null ? 10 : obstacle.GetFrequency();
            obstacleTriggers[i] = Random.Range(0, freq);
        }
    }

    internal void Tick() {
        for (int i = 0; i < obstacles.Length; i++) {
            obstacleTriggers[i]++;
            GameObject obstacleGO = obstacles[i];
            Obstacle obstacle = obstacleGO.GetComponent<Obstacle>();
            int freq = obstacle == null ? 10 : obstacle.GetFrequency();

            if (obstacleTriggers[i] >= freq) {
                obstacleTriggers[i] = Random.Range(-freq/2, freq/2);
                SpawnObstacle(obstacleGO);
            }
        }
    }

    private void SpawnObstacle(GameObject obstacle) {

        GameObject go = Instantiate(obstacle);

        float fallDistance = Camera.main.transform.position.y;
        go.transform.position = new Vector3(0, fallDistance - camHeight, 0);
        Obstacle obst = go.GetComponent<Obstacle>();
        if (obst != null) {
            obst.Spawn();
        }

        activeObstacles.Add(go.transform);
    }

    void Update() {
        float camY = Camera.main.transform.position.y;

        List<Transform> marked = new List<Transform>();
        foreach (Transform t in activeObstacles) {
            if (t == null) {
                marked.Add(t);
            } else if (t.position.y > camY + camHeight || t.position.y < camY - camHeight*2) {
                marked.Add(t);
            }
        }

        foreach (Transform t in marked) {
            activeObstacles.Remove(t);
            if (t != null)
                Destroy(t.gameObject);
        }
    }
}
