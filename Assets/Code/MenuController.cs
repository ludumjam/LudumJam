using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
    public GameObject scoreTextPrefab;
    public GameObject leaderboardPanel;
    public float offset = 70f;
    private dreamloLeaderBoard leaderBoard;
    private List<dreamloLeaderBoard.Score> scores;

    void Start()
    {
        scores = new List<dreamloLeaderBoard.Score>();
        leaderBoard = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
        leaderBoard.LoadScores();
    }

    void Update()
    {
        if (leaderBoard.ToListHighToLow().Count != scores.Count)
        {
            scores = leaderBoard.ToListHighToLow();
            PopulateLeaderboard();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    public void PopulateLeaderboard()
    {
        float previousYPosition = 0f;
        for (int i = 0; i < scores.Count; i++)
        {
            GameObject scoreObject = (GameObject)Instantiate(scoreTextPrefab);
            scoreObject.transform.SetParent(leaderboardPanel.transform);
            scoreObject.transform.localPosition = Vector3.zero;
            RectTransform scoreRectTransform = scoreObject.GetComponent<RectTransform>();
            scoreRectTransform.localScale = Vector3.one;
            scoreRectTransform.localPosition = new Vector3(0, previousYPosition - offset, 0);
            scoreRectTransform.offsetMin = new Vector2(10, scoreRectTransform.offsetMin.y);
            scoreRectTransform.offsetMax = new Vector2(30, scoreRectTransform.offsetMax.y);
            previousYPosition -= offset;
            scoreObject.GetComponent<Text>().text = scores[i].playerName + " : " + scores[i].score;
        }
    }
}
