using UnityEngine;

public class CollisionDetect : MonoBehaviour
{

    public GameObject obstacle;
    public GameObject popUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obstacle = GameObject.Find("ObstacleCar");

        popUp = GameObject.Find("Game Over");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(obstacle);

        if(CompareTag("Player"))
            popUp.SetActive(true);

    }
}