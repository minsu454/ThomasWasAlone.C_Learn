using Common.Event;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Camera mainCamera;
    private CubeManager cubeManager;
    private CameraController cameraController;

    private bool jumpRequested;
    private bool switchRequested;
    private bool rotateRequested;

    private void Awake()
    {
        cubeManager = GetComponent<CubeManager>();
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleSwitchInput();
        HandleRotateInput();

        ProcessInputs();
    }

    private void HandleMovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            cubeManager.Move(h, v, mainCamera.transform);
        }
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }
    }

    private void HandleSwitchInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            EventManager.Dispatch(GameEventType.ChangeCube, null);
        }
    }

    private void HandleRotateInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rotateRequested = true;
        }
    }

    private void ProcessInputs()
    {
        if (jumpRequested)
        {
            cubeManager.Jump();
            jumpRequested = false;
        }

        if (rotateRequested)
        {
            cameraController.Rotate();
            rotateRequested = false;
        }
    }
}