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
    [SerializeField] private Leaderboard leaderboard;
    [SerializeField] private GameObject submitButton;

    private AudioSource audioSource;
    private float gameDuration = 20f;
    private float timeRemaining;
    private TextMeshProUGUI timerText;
    private bool isGameOver = false;
    private bool timerStarted = false;
    private string playerName;
    private static string savedPlayerName;
    private static bool restartFlag = false;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        // Time.timeScale = 1f; // ensures game is running
        audioSource = GetComponent<AudioSource>();
        submitButton.SetActive(false);

        if (restartFlag && !string.IsNullOrEmpty(savedPlayerName))
        {
            playerName = savedPlayerName;
            BeginGame();
            restartFlag = false;
        }
    }

    public void BeginGame()
    {
        if (string.IsNullOrWhiteSpace(playerName))
        {
            Debug.LogWarning("Please enter your name before starting!");
            // show UI warning ?
            return;
        }
        heartbeatSound.Play();
        startPanel.SetActive(false);
        timeRemaining = gameDuration;
        timerStarted = true;
        Time.timeScale = 1f;

        MouseLook mouseLook = FindFirstObjectByType<MouseLook>();
        mouseLook.EnableLook();

        RaycastShoot raycastShoot = FindFirstObjectByType<RaycastShoot>();
        raycastShoot.EnableShooting();

        SpawnManager spawnManager = FindFirstObjectByType<SpawnManager>();
        spawnManager.StartSpawning();

    }

    public void OnNameEntered(string name)
    {
        playerName = name.Trim();
        submitButton.SetActive(!string.IsNullOrWhiteSpace(playerName));
        Debug.Log("Player name set to: " + playerName);
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
        leaderboard.SetLeaderboardEntry(playerName, finalScore);
        gameOverPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // unfreeze time
        restartFlag = true;
        savedPlayerName = playerName;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
