using UnityEngine;

public class StartMenuButtons : MonoBehaviour
{
    public GameObject mainMenu;

    public MonoBehaviour playerMovement;

    public CountdownTimer countdownTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenu.SetActive(true);

        playerMovement.enabled = false;

        countdownTimer.StopTimer();

        Time.timeScale = 1f;
    }

    public void StartTheGame()
    {
        mainMenu.SetActive(false);

        playerMovement.enabled=true;

        countdownTimer.StartTimer();

        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
