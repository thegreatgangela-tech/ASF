using UnityEngine;
using System.Collections;
using TMPro;

public class NPC : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public TextMeshProUGUI textUI;

    [Header("NPC")]
    public Transform robot;
    public Transform flyAwayPoint;
    public float flySpeed = 20f;

    void Start()
    {
        panel.SetActive(true);

        textUI.text =
            "Soldier! Click right button to kill enemies and protect the base!\n" +
            "Remember, don't get inside the energy field!";

        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        yield return new WaitForSeconds(5f);
        panel.SetActive(false);
        robot.LookAt(flyAwayPoint);

        while (Vector3.Distance(robot.position, flyAwayPoint.position) > 0.1f)
        {
            robot.position = Vector3.MoveTowards(
                robot.position,
                flyAwayPoint.position,
                flySpeed * Time.deltaTime
            );
            yield return null;
        }

        robot.gameObject.SetActive(false);
    }
}