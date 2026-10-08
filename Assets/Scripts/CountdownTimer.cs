using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float startTime = 60f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Game Over UI")]
    [SerializeField] private TMP_Text gameOverText;

    private float timeRemaining;
    private bool timerRunning = false;
    private bool gameOver = false;

    void Start()
    {
        timeRemaining = startTime;
        timerRunning = false;
        gameOver = false;

        UpdateTimerDisplay();

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.enabled = false;
        }
        if (gameOverPanel != null) 
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (!timerRunning || gameOver)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;

            UpdateTimerDisplay();
            TimerFinished();

            return;
        }

        UpdateTimerDisplay();
    }

    public void StartTimer()
    {
        if (gameOver)
            return;

        timeRemaining = startTime;
        timerRunning = true;

        Debug.Log("Timer started!");

        UpdateTimerDisplay();
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void TimerFinished()
    {
        if (gameOver)
            return;

        gameOver = true;
        timerRunning = false;

        Debug.Log("TIMER REACHED ZERO! GAME OVER!");

        // Show entire Game Over UI
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Show Game Over text
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.enabled = true;
            gameOverText.text = "GAME OVER";
        }

        // Freeze gameplay
        Time.timeScale = 0f;
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(timeRemaining);
            timerText.text = seconds.ToString();
        }
    }

    public void RestartGame()
    {
        // Unfreeze before restarting
        Time.timeScale = 1f;

        SceneManager.LoadScene("Scene1");
    }

    // Test Game Over manually from Inspector
    [ContextMenu("Test Game Over")]
    public void TestGameOver()
    {
        TimerFinished();
    }

    private void LateUpdate()
    {
        if (!gameOver || gameOverText == null)
            return;

        if (!gameOverText.gameObject.activeSelf)
        {
            Debug.LogWarning("GameOverText GameObject is disabled!");
        }

        if (!gameOverText.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("GameOverText or one of its parents is disabled!");
        }

        if (!gameOverText.enabled)
        {
            Debug.LogWarning("GameOverText component is disabled!");
        }
    }
}