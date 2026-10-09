using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;

    public bool gameIsPaused = false;
    public bool gameHasStarted = false;
    public bool gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenuUI.SetActive(false);

        gameIsPaused = false;
        gameHasStarted = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameHasStarted || gameOver)
            return;

        //Detect ESC
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else 
            {
                PauseGame();
            }
        }
    }

    public void StartGameplay() 
    {
        gameHasStarted = true;
        gameOver = false;
        gameIsPaused = false;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ResumeGame() 
    {
        if (!gameHasStarted || gameOver)
            return;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void PauseGame() 
    {
        if (!gameHasStarted || gameOver)
            return;

        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;

        gameIsPaused = true;
    }
}
