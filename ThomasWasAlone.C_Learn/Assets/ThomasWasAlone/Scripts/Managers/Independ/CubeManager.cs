using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public BaseCube[] cubes;
    private int currentCubeIndex = 0;
    public Camera mainCamera;
    
    private Vector3 moveDirection;
    private bool jumpRequested;
    private BaseCube currentCube => cubes[currentCubeIndex];

    private void Start()
    {
        SwitchToCube(0);
    }

    private void Update()
    {
        CheckCubeSwitch();
        GetMovementInput();
        GetJumpInput();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        if (jumpRequested) ApplyJump();
    }

    private void CheckCubeSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentCubeIndex = (currentCubeIndex + 1) % cubes.Length;
            SwitchToCube(currentCubeIndex);
        }
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

    private void ApplyMovement()
    {
        if (moveDirection != Vector3.zero)
        {
            Vector3 movement = moveDirection * (currentCube.MoveSpeed * Time.fixedDeltaTime);
            currentCube.transform.position += movement;
        }
    }

    private void ApplyJump()
    {
        if (currentCube.IsGrounded)
        {
            Rigidbody rb = currentCube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * currentCube.JumpForce, ForceMode.Impulse);
            }
        }
        jumpRequested = false;
    }

    private void SwitchToCube(int index)
    {
        moveDirection = Vector3.zero;
        jumpRequested = false;

        CameraController.Instance.SetTarget(cubes[index].transform);
    }
} 