using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    // creates a singleton to let other scripts access the score
    public static GameScore Instance { get; private set; }

    private TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    public void SubtractScore(int points)
    {
        score -= points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }
}
