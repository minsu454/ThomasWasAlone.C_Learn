using Common.Event;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Camera mainCamera;
    private CubeManager cubeManager;
    private CameraController cameraController;

    // 입력 처리를 위한 요청 플래그들
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

    /// <summary>
    /// 이동 입력을 처리합니다.
    /// </summary>
    private void HandleMovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            cubeManager.Move(h, v, mainCamera.transform);
        }
    }

    /// <summary>
    /// 점프 입력을 처리합니다.
    /// </summary>
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }
    }

    /// <summary>
    /// 큐브 전환 입력을 처리합니다.
    /// </summary>
    private void HandleSwitchInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            EventManager.Dispatch(GameEventType.ChangeCube, null);
        }
    }

    /// <summary>
    /// 카메라 회전 입력을 처리합니다.
    /// </summary>
    private void HandleRotateInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rotateRequested = true;
        }
    }

    /// <summary>
    /// 수집된 입력을 실제 동작으로 처리합니다.
    /// </summary>
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