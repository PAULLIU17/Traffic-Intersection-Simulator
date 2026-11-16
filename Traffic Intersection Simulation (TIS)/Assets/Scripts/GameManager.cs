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
    [SerializeField] private TextMeshProUGUI timeRemainingText;

    [SerializeField] private SelectionManager selectionManager;

    private int score;
    private float currentTime;
    private bool isGameStarted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameStarted = false;
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

    // Sets time based on difficulty selection
    // Easy = 120 seconds
    // Medium = 90 seconds
    // Hard = 60 seconds
    void SetTime(int timeToAdd)
    {
        currentTime = timeToAdd;
        timerText.text = "Time: " + currentTime.ToString();
    }

    // Updates time
    void UpdateTime()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();

        // If game has started and time is more than 0 and player hasn't made selection yet,
        // Then count down
        if (isGameStarted && currentTime > 0 && !selectionManager.ReturnSubmitButtonState())
        {
            currentTime -= Time.deltaTime;

            // If time runs out, then game over
            if (currentTime <= 0)
            {
                GameOver();
            }
        }
    }

    // Sets game status to gameover and shows gameOverScreen
    public void GameOver()
    {
        finalScoreText.text = "Final Score: " + score.ToString();
        timeRemainingText.text = "Time Remaining: " + Mathf.CeilToInt(currentTime).ToString();
        gameScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(true);
    }

    // Restarts the game when called
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
