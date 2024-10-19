using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class ScrollView : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook playerCamera;
    public float zoomSpeed = 1f;
    [SerializeField] private float zoomMultiplier = 0.001f;
    [SerializeField] private float smoothTime = 0.1f; // Time to smooth the zoom
    [SerializeField] private float yMin = 0f;
    [SerializeField] private float yMax = 1f;

    private PlayerControls playerControls;
    private float targetYValue; // Target Y-axis value
    private float currentYValue; // Current Y-axis value
    private float velocity; // Used for SmoothDamp

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Look.performed += ctx => Zoom(ctx);
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
    }

    private void Start()
    {
        // Initialize the currentYValue to the camera's starting Y-axis value
        currentYValue = playerCamera.m_YAxis.Value;
        targetYValue = currentYValue; // Start target at the current Y-axis value
    }

    private void Update()
    {
        // Smoothly transition to the target Y value
        currentYValue = Mathf.SmoothDamp(currentYValue, targetYValue, ref velocity, smoothTime);
        playerCamera.m_YAxis.Value = currentYValue; // Update camera Y-axis
    }

    private void Zoom(InputAction.CallbackContext ctx)
    {
        Vector2 scrollDelta = ctx.ReadValue<Vector2>();

        // Calculate zoom amount
        float zoomAmount = scrollDelta.y * zoomMultiplier * zoomSpeed;

        // Negate the zoom amount to flip the scroll effect
        targetYValue = Mathf.Clamp(targetYValue - zoomAmount, yMin, yMax); // Adjust min and max as needed
    }
}
