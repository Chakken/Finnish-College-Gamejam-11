using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 expectedVelocity = transform.right * moveSpeed;

        rb.AddForce(
            expectedVelocity - rb.linearVelocity,
            ForceMode.VelocityChange
        );
    }
}