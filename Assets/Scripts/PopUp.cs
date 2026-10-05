using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject popUp;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        popUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPopup() 
    {
        popUp.SetActive(true);
    }

    public void ClosePopup() 
    {  
        popUp.SetActive(false);
    }
}
