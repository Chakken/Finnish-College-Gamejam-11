using UnityEngine;

public class CarAI : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float acceleration = 5f;
    public float turnSpeed = 4f;

    public Transform[] waypoints;
    public float waypointDistance = 3f;

    private Rigidbody rb;
    private int currentWaypoint = 0;
    private float currentSpeed = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          rb = GetComponent<Rigidbody>();  
    }

    private void FixedUpdate()
    {
        if (waypoints.Length == 0)
            return;

        //Get direction towards current waypoint
        Vector3 direction = waypoints[currentWaypoint].position - transform.position;

        direction.y = 0;

        //Smooth acceleration
        currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed, acceleration * Time.fixedDeltaTime);

        //Move forward
        Vector3 targetVelocity = transform.forward * currentSpeed;

        targetVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = targetVelocity;

        //Smooth turns
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);

            rb.MoveRotation(newRotation);
        }

        //Move to next waypoint
        if (direction.magnitude < waypointDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
        }
    }
}
