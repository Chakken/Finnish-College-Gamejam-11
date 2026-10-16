
using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    [Header("Input")]
    public InputAction moveAction;

    [Header("Movement")]
    public float moveSpeed = 25f;
    public float turnSpeed = 100f;

    [Header("Front Wheel Steering")]
    public Transform frontLeftSteering;
    public Transform frontRightSteering;

    [Header("Wheel Meshes")]
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;

    [Header("Wheel Animation Settings")]
    public float maxSteeringAngle = 30f;
    public float steeringSmoothness = 10f;
    public float wheelRadius = 0.35f;

    private Rigidbody rb;
    private Vector2 moveInput;

    private float currentSteeringAngle;

    private Quaternion leftSteeringOriginal;
    private Quaternion rightSteeringOriginal;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (frontLeftSteering != null)
            leftSteeringOriginal = frontLeftSteering.localRotation;

        if (frontRightSteering != null)
            rightSteeringOriginal = frontRightSteering.localRotation;
    }

    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        moveInput = Vector2.zero;
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        float forwardInput = moveInput.y;
        float turnInput = moveInput.x;

        // CAR MOVEMENT
        Vector3 expectedVelocity =
            transform.forward * forwardInput * moveSpeed;

        Vector3 velocityDifference =
            expectedVelocity - rb.linearVelocity;

        rb.AddForce(
            velocityDifference,
            ForceMode.VelocityChange
        );

        // CAR TURNING
        // Only turn while pressing W or S
        if (Mathf.Abs(forwardInput) > 0.01f)
        {
            float turnAmount =
                turnInput * turnSpeed *
                Time.fixedDeltaTime;

            // Reverse steering when backing up
            turnAmount *= Mathf.Sign(forwardInput);

            Quaternion turnRotation =
                Quaternion.Euler(0f, turnAmount, 0f);

            rb.MoveRotation(rb.rotation * turnRotation);
        }

        AnimateWheels(turnInput);
    }

    void AnimateWheels(float turnInput)
    {
        // SMOOTH FRONT WHEEL STEERING
        float targetSteeringAngle = 0f;

        if (Mathf.Abs(moveInput.y) > 0.01f) 
        {
            targetSteeringAngle = turnInput * maxSteeringAngle;
        }
            
        currentSteeringAngle = Mathf.MoveTowards(
            currentSteeringAngle,
            targetSteeringAngle,
            steeringSmoothness * maxSteeringAngle *
            Time.fixedDeltaTime
        );

        Quaternion steeringRotation =
            Quaternion.Euler(0f, currentSteeringAngle, 0f);

        if (frontLeftSteering != null)
        {
            frontLeftSteering.localRotation =
                leftSteeringOriginal * steeringRotation;
        }

        if (frontRightSteering != null)
        {
            frontRightSteering.localRotation =
                rightSteeringOriginal * steeringRotation;
        }

        // CALCULATE WHEEL SPIN
        float forwardSpeed = Vector3.Dot(
            rb.linearVelocity,
            transform.forward
        );

        float spinDegrees =
            (forwardSpeed / (2f * Mathf.PI *
            Mathf.Max(wheelRadius, 0.01f))) *
            360f * Time.fixedDeltaTime;

        // SPIN ALL FOUR WHEELS
        SpinWheel(frontLeftWheel, spinDegrees);
        SpinWheel(frontRightWheel, spinDegrees);
        SpinWheel(rearLeftWheel, spinDegrees);
        SpinWheel(rearRightWheel, spinDegrees);
    }

    void SpinWheel(Transform wheel, float degrees)
    {
        if (wheel != null)
        {
            wheel.Rotate(
                Vector3.right,
                degrees,
                Space.Self
            );
        }
    }
}
