using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateView : MonoBehaviour
{
    private PlayerControls playerControls;
    [SerializeField] private CinemachineFreeLook playerCamera;
    public float sensitivity = 10.0f;
    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Rotate.performed += ctx => Rotate(ctx);
    }
    private void Rotate(InputAction.CallbackContext ctx)
    {
        if (playerControls.Player.ActivateRotate.IsPressed())
        {
            Debug.Log("Rotate called");
            float xDelta = ctx.ReadValue<Vector2>().x;
            playerCamera.m_XAxis.Value += (xDelta * sensitivity);
        }
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
        
    }
}
