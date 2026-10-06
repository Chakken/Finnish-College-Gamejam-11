using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float startTime = 60f;
    [SerializeField] private TMP_Text timerText;

    private float timeRemaining;
    private bool timerRunning = true;

    void Start()
    {
        timeRemaining = startTime;
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
    }
}