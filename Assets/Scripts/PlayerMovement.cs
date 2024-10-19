using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float groundDistance = 0.2f;
    [SerializeField] float sprintMult = 2.0f;
    [SerializeField] float rotationSpeed = 360.0f;
    [SerializeField] private Rigidbody rb;
    
    private PlayerControls playerControls;
    private Camera playerCamera;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool isRunning;
    private int jumpCount = 0;
    private const int maxJumps = 2;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerCamera = Camera.main;
    }

    private void OnEnable()
    {
        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        playerControls.Player.Jump.performed += ctx => Jump();
        playerControls.Player.Run.performed += ctx => isRunning = true;
        playerControls.Player.Run.canceled += ctx => isRunning = false;
        playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        /*
        float currentSpeed = isRunning ? speed * sprintMult : speed;
        Vector3 velocity = new Vector3(moveInput.x * currentSpeed, rb.velocity.y, moveInput.y * currentSpeed);
        rb.velocity = velocity;*/

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance);

        Vector3 moveDirection = Vector3.zero;

        // Rotate towards the movement direction
        if (moveInput != Vector2.zero)
        {
            Vector3 cameraForward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;

            // Ignore the vertical component
            cameraForward.y = 0;
            cameraRight.y = 0;

            // Normalize the direction vectors
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Calculate movement direction relative to camera
            moveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // gradual rotation
        }
        float currentSpeed = isRunning ? speed * sprintMult : speed;
        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);


    }

    private void Jump()
    {
        if (isGrounded || jumpCount < maxJumps)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumpCount++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGrounded)
        {
            jumpCount = 0; // Reset jump count when grounded
        }
    }
}
