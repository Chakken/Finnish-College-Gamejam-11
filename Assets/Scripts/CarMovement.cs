using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    [Header("Input")]
    public InputAction moveAction;

    [Header("Movement")]
    public float moveSpeed = 25f;
    public float turnSpeed = 100f;

    private Rigidbody rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // W / S
        float forwardInput = moveInput.y;

        // A / D
        float turnInput = moveInput.x;

        // -------------------------
        // FORWARD / BACKWARD
        // -------------------------

        Vector3 expectedVelocity =
            transform.forward * forwardInput * moveSpeed;

        Vector3 velocityDifference =
            expectedVelocity - rb.linearVelocity;

        rb.AddForce(
            velocityDifference,
            ForceMode.VelocityChange
        );

        // -------------------------
        // TURN LEFT / RIGHT
        // -------------------------

        float turnAmount =
            turnInput * turnSpeed * Time.fixedDeltaTime;

        Quaternion turnRotation =
            Quaternion.Euler(0f, turnAmount, 0f);

        rb.MoveRotation(
            rb.rotation * turnRotation
        );
    }
}