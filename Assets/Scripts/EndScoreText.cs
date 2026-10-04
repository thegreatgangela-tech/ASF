using UnityEngine;
using TMPro;

public class EndScoreText : MonoBehaviour
{
    private void Start()
    {
        TMP_Text text = GetComponent<TMP_Text>();

        if (GameManager.Instance != null)
        {
            text.text = "Final Score: " + GameManager.Instance.score;
        }
        else
        {
            text.text = "Final Score: 0";
        }
    }
}
