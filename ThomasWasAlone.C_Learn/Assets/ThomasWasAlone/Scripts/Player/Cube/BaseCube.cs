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
        float rayLength = boxCollider.size.y * 0.15f;
        Vector3 boxSize = boxCollider.size * 0.9f;
        boxSize.y = boxCollider.size.y * 0.1f;
        
        Vector3 origin = transform.position - new Vector3(0, boxCollider.size.y * 0.4f, 0);
        
        isGrounded = Physics.BoxCast(
            origin,
            boxSize * 0.5f,
            Vector3.down,
            out RaycastHit hit,
            transform.rotation,
            rayLength
        );
    }

    protected virtual void HandleMovement()
    {
        Vector3 targetVelocity = (transform.right * moveInput.x + transform.forward * moveInput.z) * moveSpeed;
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
