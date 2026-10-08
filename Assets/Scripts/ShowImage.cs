
using System.Collections;
using UnityEngine;

public class ShowImage : MonoBehaviour
{
    [Header("UI Image")]
    public GameObject imageToShow;

    [Header("Timer Settings")]
    public float displayTime = 3;

    private Coroutine hideCoroutine;

    void Start()
    {
        // Hide image when game starts
        imageToShow.SetActive(false);
    }

    public void ShowUIImage()
    {
        // Show image when button is clicked
        imageToShow.SetActive(true);

        if (hideCoroutine != null) 
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay() 
    {
        yield return new WaitForSeconds(displayTime);

        imageToShow.SetActive(false);

        hideCoroutine = null;
    }
    public void HideUIImage()
    {
        if (hideCoroutine != null) 
        { 
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        // Hide image
        imageToShow.SetActive(false);
    }
}
