using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float startTime = 60f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject timeIsUpScreen;

    private float timeRemaining;
    private bool timerRunning = false;
    
    void Start()
    {
        timeRemaining = startTime;
        UpdateTimerDisplay();

        if (timeIsUpScreen != null)
            timeIsUpScreen.SetActive(false);
    }

    void Update()
    {
        if (!timerRunning) return;
        
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;

                TimerFinished();
            }

            UpdateTimerDisplay();
    }

    public void StartTimer() 
    {
        timeRemaining = startTime;
        timerRunning = true;
        UpdateTimerDisplay();
    }

    public void StopTimer() 
    {
        timerRunning = false;

        if (timeIsUpScreen != null)
            timeIsUpScreen.SetActive(true);

        Time.timeScale = 0f;
    }

    public void TimerFinished()
    {
        timeIsUpScreen.SetActive(true);
    }

    void UpdateTimerDisplay() 
    {
        if(timerText != null)
        {
            int seconds = Mathf.CeilToInt(timeRemaining);
            timerText.text = seconds.ToString();
        }
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene("Scene1");
    }
}