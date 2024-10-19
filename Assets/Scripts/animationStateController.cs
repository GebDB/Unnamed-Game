using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationStateController : MonoBehaviour
{
    private Animator animator;
    private int isWalkingHash;
    private int isRunningHash;
    private PlayerControls playerControls;

    private Vector2 moveInput = Vector2.zero;
    private bool isRunning = false;

    // Sets up animation response to input
    private void Awake()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        playerControls = new PlayerControls();
        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        playerControls.Player.Run.performed += ctx => isRunning = true;
        playerControls.Player.Run.canceled += ctx => isRunning = false;
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
    }

    private void Update()
    {
        // Determine walking and running states based on moveInput
        bool isMoving = moveInput.magnitude > 0;

        // Update walking state
        animator.SetBool(isWalkingHash, isMoving);

        // Update running state
        if (isMoving)
        {
            animator.SetBool(isRunningHash, isRunning);
        }
        else
        {
            animator.SetBool(isRunningHash, false); // Stop running if not moving
        }
    }
}
