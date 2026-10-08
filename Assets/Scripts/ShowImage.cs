using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class ShowImage : MonoBehaviour
{
    [Header("UI Image")]
    public GameObject imageToShow;

    [Header("Timer Settings")]
    public float displayTime = 8f;

    private Coroutine transitionCoroutine;

    private AsyncOperation sceneLoad;

    void Start()
    {
        // Hide image when game starts
        imageToShow.SetActive(false);
    }

    public void ShowUIImage()
    {
        // Show image when button is clicked
        imageToShow.SetActive(true);

        if (transitionCoroutine != null) 
        {
            StopCoroutine(transitionCoroutine);
        }

        transitionCoroutine = StartCoroutine(LoadSceneSmoothly());
    }

    IEnumerator LoadSceneSmoothly()
    {
        // Start loading Scene1 in the background
        sceneLoad = SceneManager.LoadSceneAsync("Scene1");

        if (sceneLoad == null)
        {
            Debug.LogError("Could not load Scene1!");
            transitionCoroutine = null;
            yield break;
        }

        // Prevent scene from activating immediately
        sceneLoad.allowSceneActivation = false;

        // Wait 8 seconds while scene loads
        float timer = 0f;

        while (timer < displayTime)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        // Wait until scene is ready
        while (sceneLoad.progress < 0.9f)
        {
            yield return null;
        }

        // Reset time scale
        Time.timeScale = 1f;

        // Activate Scene1
        sceneLoad.allowSceneActivation = true;
    }

    public void HideUIImage()
    {
        // Don't cancel an asynchronous load that is already running
        if (transitionCoroutine != null)
            return;

        imageToShow.SetActive(false);
    }
}