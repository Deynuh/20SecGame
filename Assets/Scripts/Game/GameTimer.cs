using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private AudioSource ambientSound;
    [SerializeField] private AudioSource heartbeatSound;

    private AudioSource audioSource;
    private float gameDuration = 20f;
    private float timeRemaining;
    private TextMeshProUGUI timerText;
    private bool isGameOver = false;
    private bool hasBlinked = false;
    private bool timerStarted = false;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        // Time.timeScale = 1f; // ensures game is running
        audioSource = GetComponent<AudioSource>();
    }

    public void BeginGame()
    {
        startPanel.SetActive(false);
        timeRemaining = gameDuration;
        timerStarted = true;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (!timerStarted || isGameOver) return;

        timeRemaining -= Time.deltaTime;
        timerText.text = "Time left: " + timeRemaining.ToString("F2");
        // gradually increase heartbeat volume
        heartbeatSound.volume = Mathf.Clamp01((gameDuration - timeRemaining) / gameDuration);

        // check for game over
        if (timeRemaining < 0)
        {
            timeRemaining = 0f;

            if (!isGameOver)
            {
                isGameOver = true;
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        Time.timeScale = 0f; // freezes game
        ambientSound.Stop(); // stops ambient sound
        heartbeatSound.Stop(); // stops heartbeat sound
        audioSource.Play(); // plays game over sound

        int finalScore = GameScore.Instance.GetScore();

        finalScoreText.text = "Your Score: " + finalScore;
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
