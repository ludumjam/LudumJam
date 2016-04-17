using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LeaderBoardUI : MonoBehaviour
{
    public GameObject scorePrefab;
    public GameObject leaderboardBackgroundPanel;
    public GameObject leaderboardPanel;
    private dreamloLeaderBoard leaderBoard;
    private List<dreamloLeaderBoard.Score> scores;
    private bool areScoresLoaded = false;
    private float offset = 70f;

    // Use this for initialization
    void Start()
    {
        leaderBoard = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
        List<dreamloLeaderBoard.Score> scores = new List<dreamloLeaderBoard.Score>();
        CameraFollow.OnPlayerWentOutsideScreen += HandleOnDeathEvent;
        leaderboardBackgroundPanel.GetComponent<Image>().enabled = false;
    }

    void HandleOnDeathEvent ()
    {
        leaderBoard.AddScore("Player 1", (int) Character.greatestDistanceAchieved);
        leaderBoard.LoadScores();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (!areScoresLoaded)
        {
            scores = leaderBoard.ToListHighToLow();
            if (scores.Count > 0)
            {
                areScoresLoaded = true;
                float previousYPosition = 0f;
                foreach (dreamloLeaderBoard.Score score in leaderBoard.ToListHighToLow())
                {
                    GameObject scoreObject = (GameObject) Instantiate(scorePrefab);
                    scoreObject.transform.SetParent(leaderboardPanel.transform);
                    scoreObject.transform.localPosition = Vector3.zero;
                    RectTransform scoreRectTransform = scoreObject.GetComponent<RectTransform>();
                    scoreRectTransform.localScale = Vector3.one;
                    scoreRectTransform.localPosition = new Vector3(0, previousYPosition + offset, 0);
                    previousYPosition += offset;
                    scoreRectTransform.offsetMin = new Vector2(0, scoreRectTransform.offsetMin.y);
                    scoreRectTransform.offsetMax = new Vector2(0, scoreRectTransform.offsetMax.y);
                    scoreObject.GetComponent<Text>().text = score.playerName + " : " + score.score;
                }
                leaderboardBackgroundPanel.GetComponent<Image>().enabled = true;
            }
        }
    }
}
