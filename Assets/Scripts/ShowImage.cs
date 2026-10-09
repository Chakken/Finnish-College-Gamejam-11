
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShowImage : MonoBehaviour
{
    [Header("UI Image")]
    public GameObject imageToShow;

    [Header("Timer Settings")]
    public float displayTime = 8f;

    [Header("Scene Settings")]
    public string targetScene = "Scene1";

    private Coroutine transitionCoroutine;
    private bool isTransitioning = false;

    void Start()
    {
        imageToShow.SetActive(false);
    }

    public void ShowUIImage()
    {
        if (isTransitioning)
            return;

        isTransitioning = true;

        // Show the image
        imageToShow.SetActive(true);

        // Check current scene
        if (SceneManager.GetActiveScene().name == targetScene)
        {
            // Already in Scene1: just hide image
            transitionCoroutine =
                StartCoroutine(HideAfterDelay());
        }
        else
        {
            // Different scene: load Scene1
            transitionCoroutine =
                StartCoroutine(LoadSceneSmoothly());
        }
    }

    IEnumerator HideAfterDelay()
    {
        // Wait 8 seconds, even if paused
        yield return new WaitForSecondsRealtime(displayTime);

        // Hide image
        imageToShow.SetActive(false);

        isTransitioning = false;
        transitionCoroutine = null;
    }

    IEnumerator LoadSceneSmoothly()
    {
        // Start loading target scene
        AsyncOperation sceneLoad =
            SceneManager.LoadSceneAsync(targetScene);

        if (sceneLoad == null)
        {
            Debug.LogError(
                "Could not load " + targetScene);

            imageToShow.SetActive(false);
            isTransitioning = false;
            transitionCoroutine = null;
            yield break;
        }

        sceneLoad.allowSceneActivation = false;

        // Keep image visible for 8 seconds
        yield return new WaitForSecondsRealtime(displayTime);

        // Wait for scene loading
        while (sceneLoad.progress < 0.9f)
        {
            yield return null;
        }

        // Resume normal time
        Time.timeScale = 1f;

        // Activate new scene
        sceneLoad.allowSceneActivation = true;
    }

    public void HideUIImage()
    {
        if (isTransitioning)
            return;

        imageToShow.SetActive(false);
    }
}
