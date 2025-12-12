using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private AudioSource ambientSound;
    [SerializeField] private AudioSource heartbeatSound;

    private float gameDuration = 20f;
    private float timeRemaining;
    private TextMeshProUGUI timerText;
    private bool isGameOver = false;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timeRemaining = gameDuration;
        Time.timeScale = 1f; // ensures game is running
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        // gradually increase heartbeat volume
        heartbeatSound.volume = Mathf.Clamp01((gameDuration - timeRemaining) / gameDuration);

        if (timeRemaining < 0)
        {
            timeRemaining = 0f;

            if (!isGameOver)
            {
                isGameOver = true;
                EndGame();
            }
        }

        timerText.text = "Time left: " + timeRemaining.ToString("F2");
    }

    private void EndGame()
    {
        Time.timeScale = 0f; // freezes game
        ambientSound.Stop(); // stops ambient sound
        heartbeatSound.Stop(); // stops heartbeat sound

        int finalScore = GameScore.Instance.GetScore();

        finalScoreText.text = "Final Score: " + finalScore;
        gameOverPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // unfreeze time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
