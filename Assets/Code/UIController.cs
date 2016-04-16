using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour
{

    public GameObject gameOverPanel;
    public Button retryButton;
    public Text distanceText;
    public Image[] shapes;

    // Use this for initialization
    void Start()
    {
        gameOverPanel.SetActive(false);
        CameraFollow.OnPlayerWentOutsideScreen += HandleOnDeathEvent;
        retryButton.onClick.AddListener(HandleRetryButtonOnClick);
        Character.OnPlayerShapeShift += HandleShapeShiftEvent;
    }

    void HandleShapeShiftEvent (int index, int previousIndex)
    {
        Color tempColor = shapes[index].color;
        tempColor.a = 1f;
        shapes[index].color = tempColor;
        tempColor = shapes[previousIndex].color;
        tempColor.a = 0f;
        shapes[previousIndex].color = tempColor;
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
        Character.OnPlayerShapeShift -= HandleShapeShiftEvent;
    }
}
