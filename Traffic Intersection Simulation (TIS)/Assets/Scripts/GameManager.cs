using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject titleScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public Button switchView;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Starts the game with the intended difficulty
    public void StartGame(int difficultyLevel)
    {
        titleScreen.gameObject.SetActive(false);

        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        switchView.gameObject.SetActive(true);

        // If easy, spawn 3 vehicles

        // If medium, spawn 5 vehicles

        // If hard, spawn 7 vechicles
    }

    // Game Over
    void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // Restarts the game when called
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
