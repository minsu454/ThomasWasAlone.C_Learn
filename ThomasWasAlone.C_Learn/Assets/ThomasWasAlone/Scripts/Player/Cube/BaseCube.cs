using UnityEngine;

public abstract class BaseCube : MonoBehaviour
{
    [SerializeField] protected AudioClip jumpSound;
    [SerializeField] protected CubeType cubeType;
    public CubeType CubeType { get { return cubeType; } }

    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 7f;

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
        boxSize = GetDefaultBoxSize();
        rayLength = GetRayLength();
    }

    protected virtual void FixedUpdate()
    {
        CheckGrounded();
    }

    protected virtual void CheckGrounded()
    {
        origin = transform.position;
        origin.y -= GetOriginYOffset();

        isGrounded = Physics.BoxCast(
            origin,
            boxSize * 0.5f,
            Vector3.down,
            out _,
            Quaternion.identity,
            rayLength
        );
    }

    protected virtual Vector3 GetDefaultBoxSize()
    {
        var size = boxCollider.size * 0.9f;
        size.y = boxCollider.size.y * 0.1f;
        return size;
    }

    protected virtual float GetRayLength()
    {
        return boxCollider.size.y * 0.15f;
    }

    protected virtual float GetOriginYOffset()
    {
        return boxCollider.size.y * 0.5f;
    }

    public virtual void BoostJump(float boostForce)
    {
        if (rb)
        {
            Managers.Sound.SFX2DPlay(jumpSound);
            rb.AddForce(Vector3.up * boostForce, ForceMode.Impulse);
        }
    }
}
