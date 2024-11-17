using UnityEngine;

public abstract class BaseCube : MonoBehaviour
{
    protected Rigidbody rb;
    protected BoxCollider boxCollider;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 8f;
    
    protected Vector3 moveInput;
    protected bool jumpRequested;
    protected bool isGrounded;
    
    protected Vector3 boxSize;
    protected float rayLength;
    protected Vector3 origin;
    
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    protected virtual void Update()
    {
        CheckInput();
        CheckGrounded();
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
        if (jumpRequested) HandleJump();
    }

    protected virtual void CheckInput()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.z = Input.GetAxis("Vertical");
        
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }
    }

    protected virtual void CheckGrounded()
    {
    }

    protected virtual void HandleMovement()
    {
        // 카메라의 방향을 기준으로 이동 방향 계산
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        
        // y축 회전만 사용하도록 y값을 0으로 설정
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // 카메라 기준으로 이동 방향 계산
        Vector3 targetVelocity = (right * moveInput.x + forward * moveInput.z) * moveSpeed;
        targetVelocity.y = rb.velocity.y;
        rb.velocity = targetVelocity;
    }

    protected virtual void HandleJump()
    {
        if (!isGrounded)
        {
            return;
        }

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpRequested = false;
    }

    public virtual void BoostJump(float boostForce)
    {
        if (rb)
        {
            rb.AddForce(Vector3.up * boostForce, ForceMode.Impulse);
        }
    }
}
