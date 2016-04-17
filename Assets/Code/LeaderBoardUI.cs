using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LeaderBoardUI : MonoBehaviour
{
    public GameObject scorePrefab;
    public GameObject leaderboardBackgroundPanel;
    public GameObject leaderboardPanel;
    public Text scoreText;
    public InputField inputField;
    public GameObject submissionPanel;
    private dreamloLeaderBoard leaderBoard;
    private List<dreamloLeaderBoard.Score> scores;
    private bool areScoresLoaded = false;
    private float offset = 70f;

    // Use this for initialization
    void Start()
    {
        leaderBoard = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
        CameraFollow.OnPlayerWentOutsideScreen += HandleOnDeathEvent;
        leaderboardBackgroundPanel.GetComponent<Image>().enabled = false;
        areScoresLoaded = false;
        submissionPanel.SetActive(false);
        inputField.onEndEdit.AddListener(HandleOnEndEdit);
    }

    void HandleOnDeathEvent()
    {
        scoreText.text = Character.greatestDistanceAchieved.ToString();
        submissionPanel.SetActive(true);
    }

    void HandleOnEndEdit(string text)
    {
        leaderBoard.AddScore(text, (int)Character.greatestDistanceAchieved);
        submissionPanel.SetActive(false);
    }

    void OnDestroy()
    {
        CameraFollow.OnPlayerWentOutsideScreen -= HandleOnDeathEvent;
        inputField.onEndEdit.RemoveListener(HandleOnEndEdit);
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
                for (int i = 0; i < 10; i++)
                {
                    GameObject scoreObject = (GameObject)Instantiate(scorePrefab);
                    scoreObject.transform.SetParent(leaderboardPanel.transform);
                    scoreObject.transform.localPosition = Vector3.zero;
                    RectTransform scoreRectTransform = scoreObject.GetComponent<RectTransform>();
                    scoreRectTransform.localScale = Vector3.one;
                    scoreRectTransform.localPosition = new Vector3(0, previousYPosition - offset, 0);
                    previousYPosition -= offset;
                    scoreRectTransform.offsetMin = new Vector2(0, scoreRectTransform.offsetMin.y);
                    scoreRectTransform.offsetMax = new Vector2(0, scoreRectTransform.offsetMax.y);
                    scoreObject.GetComponent<Text>().text = scores[i].playerName + " : " + scores[i].score;
                }
                leaderboardBackgroundPanel.GetComponent<Image>().enabled = true;
            }
        }
    }
}
