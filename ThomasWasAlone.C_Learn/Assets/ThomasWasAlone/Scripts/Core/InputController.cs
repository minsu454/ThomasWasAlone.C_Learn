using UnityEngine;

public class InputController : MonoBehaviour
{
    private Camera mainCamera;
    
    private Vector3 moveDirection;
    private bool jumpRequested;
    private bool switchRequested;
    
    public Vector3 MoveDirection => moveDirection;
    public bool JumpRequested => jumpRequested;
    public bool SwitchRequested => switchRequested;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GetMovementInput();
        GetJumpInput();
        GetSwitchInput();
    }

    private void GetMovementInput()
    {
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        moveDirection = (right * h + forward * v).normalized;
    }

    private void GetJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }
    }

    private void GetSwitchInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switchRequested = true;
        }
    }

    public void ResetJumpRequest()
    {
        jumpRequested = false;
    }

    public void ResetSwitchRequest()
    {
        switchRequested = false;
    }
} 