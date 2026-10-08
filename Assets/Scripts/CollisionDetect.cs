using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public TMP_Text GameOver;
    
    public void Start()
    {
        GameOver.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameOver.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }
}