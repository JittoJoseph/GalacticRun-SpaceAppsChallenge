using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject gameUI;
    public GameObject GameOverUI;
    public Button playButton;
    public Button quitButton;
    public Button restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI overscore;
    public TextMeshProUGUI highScore;
    public Image healthBar;
    public SparrowController playerController;
    public WaveManager waveManager;

    private bool isGameRunning = false;
    private int score = 0;
    private float hqHealth = 1f; // 1 represents 100% health

    private void Start()
    {
        SetupInitialState();
        playButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        restartButton.onClick.AddListener(RestartGame);
    }

    private void SetupInitialState()
    {
        isGameRunning = false;
        mainMenuUI.SetActive(true);
        gameUI.SetActive(false);
        playerController.enabled = false;
        waveManager.StopGame();
        score = 0;
        hqHealth = 1f;
        UpdateScoreDisplay();
        UpdateHealthDisplay(hqHealth);
        GameOverUI.SetActive(false);  // Hide GameOver UI at start
    }

    private void StartGame()
    {
        isGameRunning = true;
        mainMenuUI.SetActive(false);
        gameUI.SetActive(true);
        playerController.enabled = true;
        waveManager.StartGame();
        ResetGameState();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void ResetGameState()
    {
        playerController.ResetPosition();
        waveManager.ClearEnemies();
        waveManager.StartGame();
        score = 0;
        hqHealth = 1f;
        UpdateScoreDisplay();
        UpdateHealthDisplay(hqHealth);
        GameOverUI.SetActive(false);  // Hide GameOver UI on restart
    }

    public void UpdateScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateHealthDisplay(float healthPercentage)
    {
        healthBar.fillAmount = healthPercentage;
    }

    public void DamageHQ(float damage)
    {
        hqHealth -= damage;
        if (hqHealth <= 0)
        {
            hqHealth = 0;
            GameOver();
        }
        UpdateHealthDisplay(hqHealth);
    }

    private void GameOver()
    {
        isGameRunning = false;
        Debug.Log("Game Over! Final Score: " + score);

        // Stop the game
        waveManager.StopGame();

        // Show the GameOver UI
        GameOverUI.SetActive(true);

        // Get the high score from PlayerPrefs
        int highScoreValue = PlayerPrefs.GetInt("HighScore", 0);

        // Check if the current score is higher than the high score
        if (score > highScoreValue)
        {
            // Update the high score
            highScoreValue = score;
            PlayerPrefs.SetInt("HighScore", highScoreValue);
            PlayerPrefs.Save();
        }

        // Display the final score and high score on the GameOver UI
        overscore.text = "Final Score: " + score;
        highScore.text = "High Score: " + highScoreValue;
		playerController.enabled = false;
    }

    private void RestartGame()  // Add this method
    {
        // Reset game state and start a new game
        ResetGameState();
        StartGame();
    }
}
