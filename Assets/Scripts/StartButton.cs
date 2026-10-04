using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtons : MonoBehaviour
{
    public void Retry()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("Start Scene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
