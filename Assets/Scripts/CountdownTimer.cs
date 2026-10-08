using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float startTime = 60f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject timeIsUpScreen;

    private float timeRemaining;
    private bool timerRunning = true;

    void Start()
    {
        timeRemaining = startTime;

        timeIsUpScreen.SetActive(false);
    }

    void Update()
    {
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerRunning = false;

                TimerFinished();
            }

            timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
        }
    }

    void TimerFinished()
    {
        Debug.Log("Time's up!");

        timeIsUpScreen.SetActive(true);

    }

    public void RestartGame() 
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}