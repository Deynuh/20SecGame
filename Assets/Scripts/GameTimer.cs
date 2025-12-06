using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    private float gameDuration = 20f;
    private float timeRemaining;
    private TextMeshProUGUI timerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timeRemaining = gameDuration;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining < 0)
        {
            timeRemaining = 0f;
            // insert game over logic here later!
        }

        timerText.text = "Time left: " + timeRemaining.ToString("F2");
    }
}
