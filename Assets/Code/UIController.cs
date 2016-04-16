using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour
{

    public GameObject gameOverPanel;
    public Button retryButton;
    public Text distanceText;

    // Use this for initialization
    void Start()
    {
        gameOverPanel.SetActive(false);
        CameraFollow.OnPlayerWentOutsideScreen += HandleOnDeathEvent;
        retryButton.onClick.AddListener(HandleRetryButtonOnClick);
    }

    void FixedUpdate()
    {
        distanceText.text = Character.greatestDistanceAchieved.ToString();
    }

    void HandleOnDeathEvent()
    {
        gameOverPanel.SetActive(true);
    }

    void HandleRetryButtonOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        CameraFollow.OnPlayerWentOutsideScreen -= HandleOnDeathEvent;
    }
}
