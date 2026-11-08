using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button switchView;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private SpawnManager spawnManager;

    private int score;
    private float time;
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
    public void StartGame(int difficulty)
    {
        isGameStarted = true;

        titleScreen.gameObject.SetActive(false);

        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        switchView.gameObject.SetActive(true);
        selectionPanel.gameObject.SetActive(true);

        switch (difficulty)
        {
            // Easy
            case 3:
                spawnManager.SpawnCars(3);
                Debug.Log("Spawned 3 cars");
                break;
            // Medium
            case 2:
                spawnManager.SpawnCars(5);
                Debug.Log("Spawned 5 cars");
                break;
            // Hard
            case 1:
                spawnManager.SpawnCars(7);
                Debug.Log("Spawned 7 cars");
                break;
            default:
                Debug.Log("Error");
                break;
        }

        SetScore();
        SetTime(difficulty);

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
    // Easy = 180 seconds
    // Medium = 120 seconds
    // Hard = 60 seconds
    void SetTime(int difficulty)
    {
        time = difficulty * 60;
        timerText.text = "Time: " + time.ToString();
    }

    // Updates time
    void UpdateTime()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(time).ToString();

        // If game has started and time is more than 0
        // Continue countdown
        if (isGameStarted && time > 0)
        {
            time -= Time.deltaTime;

            // If time runs out, then game over
            if (time <= 0)
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
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // Restarts the game when called
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
