using UnityEngine;
using UnityEngine.SceneManagement; //Code written by Joel and YiFan

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Data")]
    public int score = 0;
    public float baseHealth = 100f;
    public float timeRemaining = 120f;

    private bool gameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (gameOver) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            GameOver();
        }
    }

    public void AddScore(int amount)
    {
        if (gameOver) return;
        score += amount;
        Debug.Log("Score: " + score);
    }

    public void DamageBase(float damage)
    {
        if (gameOver) return;

        baseHealth -= damage;
        baseHealth = Mathf.Clamp(baseHealth, 0f, 100f);

        Debug.Log("Base Health: " + baseHealth);

        if (baseHealth <= 0f)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        Debug.Log("GAME OVER");
        SceneManager.LoadScene("End Scene");
    }

    public void ResetGame()
    {
        score = 0;
        baseHealth = 100f;
        timeRemaining = 120f;
        gameOver = false;
    }
}