using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    [Header("Input")]
    public InputAction moveAction;
    public Vector2 moveInput;

    [Header("Car Settings")]
    public float moveSpeed = 25f;
    public float turnSpeed = 100f;

    void Start()
    {
        moveAction.Enable();
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        // W/S - Drive forward and backward
        float movement = moveInput.y * moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * movement);

        // A/D - Turn left and right
        float rotation = moveInput.x * turnSpeed * Time.deltaTime;

        // Reverse steering direction when backing up
        if (moveInput.y < 0)
        {
            rotation *= -1;
        }

        transform.Rotate(Vector3.up * rotation);
    }

    void OnDisable()
    {
        moveAction.Disable();
    }
}