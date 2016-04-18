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
    public Character character;
    public Image[] highlights;
    private bool isGameOver = false;

    // Use this for initialization
    void Start()
    {
        gameOverPanel.SetActive(false);
        CameraFollow.OnPlayerWentOutsideScreen += HandleOnDeathEvent;
        retryButton.onClick.AddListener(HandleRetryButtonOnClick);
        Character.OnPlayerShapeShift += HandleShapeShiftEvent;
        isGameOver = false;
    }

    void HandleShapeShiftEvent(int index, int previousIndex)
    {
        highlights[index].enabled = true;
        highlights[previousIndex].enabled = false;
    }

    void Update()
    {
        for (int i = 0; i < shapes.Length; i++)
        {
            shapes[i].fillAmount = Mathf.Clamp(character.children[i].GetComponent<ISpecialAbility>().CoolDown, 0f, 1f);
        }
        if (isGameOver && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void FixedUpdate()
    {
        distanceText.text = Character.greatestDistanceAchieved.ToString();
    }

    void HandleOnDeathEvent()
    {
        gameOverPanel.SetActive(true);
        isGameOver = true;
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

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
