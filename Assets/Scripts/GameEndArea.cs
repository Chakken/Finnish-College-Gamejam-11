using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameEndArea : MonoBehaviour
{
    [Header("Finish UI")]
    [SerializeField] private TMP_Text finishText;

    [Header("Restart Settings")]
    [SerializeField] private float scenechangeDelay = 0f;

    [Header("Final Level Settings")]
    [Tooltip("Name of the final gameplay scene.")]
    [SerializeField] private string finalLevelName = "Scene4";

    [Tooltip("Scene that loads after completing the final level.")]
    [SerializeField] private int finalLevelBuildIndex = 3;
    [SerializeField] private string endingSceneName = "End Scene";

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

        StartCoroutine(LoadSceneAfterDelay());

        Time.timeScale = 0f;
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSecondsRealtime(scenechangeDelay);

        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();

        //If this is the final scene, go to the ending screen
        if (currentScene.name == finalLevelName) 
        {
            AsyncOperation endingLoad = SceneManager.LoadSceneAsync(endingSceneName);

            if (endingLoad == null)
            {
                Debug.LogError("Could not load ending scene: " + endingSceneName);

                yield break;
            }

            while (!endingLoad.isDone)
            {
                yield return null;
            }
        }
        else 
        {
            //Otherwise load next scene
            int nextSceneIndex = currentScene.buildIndex + 1;

            if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings) 
            {
                Debug.LogError("There is no scenes left!");

                yield break;
            }
            AsyncOperation nextLevelLoad = SceneManager.LoadSceneAsync(nextSceneIndex);

            if (nextLevelLoad == null) 
            {
                Debug.LogError("Failed to load next scene");

                yield break;
            }
            while (!nextLevelLoad.isDone)
            {
                yield return null;
            }
        }
    }
}