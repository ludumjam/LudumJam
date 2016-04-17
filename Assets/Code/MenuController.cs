using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Main");
    }
}
