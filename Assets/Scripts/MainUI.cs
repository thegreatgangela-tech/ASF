using UnityEngine;
using TMPro;
//Code written by Joel and YiFan

public class MainUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text timeText;

    private GameManager gm; 

    void Start()
    {
        gm = GameManager.Instance;
        if (gm == null)
            Debug.LogError("GameManager instance not found!");
    }

    void Update() 
    {
        if (gm == null) return;

      
        if (scoreText != null)
            scoreText.text = "Score: " + gm.score;

       
        if (healthText != null)
            healthText.text = "Health: " + Mathf.FloorToInt(gm.baseHealth);

      
        if (timeText != null) //This part is written by AI
        {
            float t = gm.timeRemaining;
            int m = Mathf.FloorToInt(t / 60);
            int s = Mathf.FloorToInt(t % 60);
            timeText.text = $"{m:00}:{s:00}";
        }

        if (healthText != null)
{
    healthText.text = $"Health: {Mathf.FloorToInt(gm.baseHealth)}";
    Debug.Log("Updating Health UI: " + healthText.text);
}
else
{
    Debug.Log("HealthText is NULL!");
}
    }
}