using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private int score;
    private float currentTime;
    private bool isGameStarted;
    private bool isGameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameStarted = false;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {   
        UpdateTime();
    }

    // Starts the game with the intended difficulty
    public void StartGame(int time)
    {
        isGameStarted = true;
        titleScreen.gameObject.SetActive(false);
        gameScreen.gameObject.SetActive(true);
        SetScore();
        SetTime(time);

    }

    // Sets score to 0
    void SetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    // Updates score whenever player makes the correct choice
    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    // Sets time based on difficulty level
    // Easy = 60 seconds
    // Medium = 40 seconds
    // Hard = 20 seconds
    void SetTime(int timeToAdd)
    {
        currentTime = timeToAdd;
        timerText.text = "Time: " + currentTime.ToString();
    }

    // Updates time
    void UpdateTime()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();

        // If game has started and time is more than 0
        // Continue countdown
        if (isGameStarted && currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            // If time runs out, then game over
            if (currentTime <= 0)
            {
                GameOver();
            }
        }
    }

    // Returns isGameStarted
    public bool IsGameStarted()
    {
        return isGameStarted;
    }

    // Returns IsGameOver
    public bool IsGameOver()
    {
        return isGameOver;
    }

    // Game Over
    void GameOver()
    {
        isGameOver = true;
        finalScoreText.text = "Final Score: " + score.ToString();
        gameScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(true);
    }

    // Restarts the game when called
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
