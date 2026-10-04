using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Scene : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Start Scene");
    }
}