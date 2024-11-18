using UnityEngine;

public abstract class BaseCube : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 8f;
    
    public float MoveSpeed => moveSpeed;
    public float JumpForce => jumpForce;
    public bool IsGrounded => isGrounded;
    
    protected bool isGrounded;
    protected Vector3 boxSize;
    protected float rayLength;
    protected Vector3 origin;
    
    protected Rigidbody rb;
    protected BoxCollider boxCollider;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        InitializeGroundCheck();
    }

    protected virtual void InitializeGroundCheck()
    {
        boxSize = boxCollider.size * 0.9f;
        boxSize.y = boxCollider.size.y * 0.1f;
        rayLength = boxCollider.size.y * 0.15f;
    }

    protected virtual void Update()
    {
        CheckGrounded();
    }

    protected virtual void CheckGrounded()
    {
        origin = transform.position;
        origin.y -= boxCollider.size.y * 0.5f;
        
        isGrounded = Physics.BoxCast(
            origin,
            boxSize * 0.5f,
            Vector3.down,
            out _,
            Quaternion.identity,
            rayLength
        );

        Debug.DrawRay(origin, Vector3.down * rayLength, isGrounded ? Color.green : Color.red);
    }

    public virtual void BoostJump(float boostForce)
    {
        if (rb)
        {
            rb.AddForce(Vector3.up * boostForce, ForceMode.Impulse);
        }
    }
}
