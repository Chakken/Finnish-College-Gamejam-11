using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameEndArea : MonoBehaviour
{
    [Header("Finish UI")]
    [SerializeField] private TMP_Text finishText;

    [Header("Restart Settings")]
    [SerializeField] private float restartDelay = 3f;

    private bool gameFinished = false;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        gameFinished = false;

        if (finishText != null)
        {
            finishText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || gameFinished)
            return;

        gameFinished = true;

        Debug.Log("Game Finished!");

        if (finishText != null)
        {
            finishText.gameObject.SetActive(true);
        }

        StartCoroutine(RestartAfterDelay());

        Time.timeScale = 0f;
    }

    private IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSecondsRealtime(restartDelay);

        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}