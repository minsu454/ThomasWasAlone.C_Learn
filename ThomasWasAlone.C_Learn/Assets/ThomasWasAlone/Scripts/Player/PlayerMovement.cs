using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private BaseCube cube;
    private Rigidbody rb;
    private bool jumpRequested;
    
    private void Start()
    {
        cube = GetComponent<BaseCube>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!cube.enabled) return;
        CheckInput();
    }

    private void FixedUpdate()
    {
        if (!cube.enabled) return;
        
        if (jumpRequested) HandleJump();
    }

    private void CheckInput()
    {
        // 카메라 기준 이동 방향 계산
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // 이동 입력
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDirection = (right * h + forward * v);
        
        // 속도 적용
        Vector3 targetVelocity = moveDirection * cube.MoveSpeed;
        targetVelocity.y = rb.velocity.y;
        rb.velocity = targetVelocity;

        // 점프 입력
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }
    }

    private void HandleJump()
    {
        if (cube.IsGrounded)
        {
            rb.AddForce(Vector3.up * cube.JumpForce, ForceMode.Impulse);
        }
        jumpRequested = false;
    }
} 