using UnityEngine;

public abstract class BaseCube : MonoBehaviour
{
    // 점프할 때 재생되는 사운드
    [SerializeField] protected AudioClip jumpSound;
    // 큐브의 타입을 구분하는 열거형 값
    [SerializeField] protected CubeType cubeType;
    public CubeType CubeType { get { return cubeType; } }

    // 큐브의 이동 속도
    [SerializeField] protected float moveSpeed = 5f;
    // 큐브의 점프력
    [SerializeField] protected float jumpForce = 170f;

    // 일반적인 상황에서의 마찰력
    [SerializeField] protected float normalDrag = 3f;
    // 낙하 시 적용되는 마찰력
    [SerializeField] protected float fallingDrag = 0f;

    public float MoveSpeed => moveSpeed;
    public float JumpForce => jumpForce;
    public bool IsGrounded => isGrounded;

    protected bool isGrounded;
    // 지면 체크를 위한 박스캐스트 크기
    protected Vector3 boxSize;
    // 지면 체크를 위한 레이 길이
    protected float rayLength;
    // 지면 체크 시작 위치
    protected Vector3 origin;

    protected Rigidbody rb;
    protected BoxCollider boxCollider;

    protected bool isFalling => !isGrounded && rb.velocity.y < 0;

    /// <summary>
    /// 큐브의 기본 초기화를 수행합니다.
    /// </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        InitializeGroundCheck();

        rb.drag = normalDrag;
    }

    /// <summary>
    /// 지면 체크를 위한 값들을 초기화합니다.
    /// </summary>
    protected virtual void InitializeGroundCheck()
    {
        boxSize = GetDefaultBoxSize();
        rayLength = GetRayLength();
    }

    /// <summary>
    /// 물리 기반 업데이트를 수행합니다.
    /// </summary>
    protected virtual void FixedUpdate()
    {
        CheckGrounded();
        UpdateDrag();
    }

    /// <summary>
    /// 큐브가 지면에 닿아있는지 확인합니다.
    /// </summary>
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

    /// <summary>
    /// 지면 체크를 위한 기본 박스 크기를 반환합니다.
    /// </summary>
    protected virtual Vector3 GetDefaultBoxSize()
    {
        var size = boxCollider.size * 0.9f;
        size.y = boxCollider.size.y * 0.1f;
        return size;
    }

    /// <summary>
    /// 지면 체크를 위한 레이 길이를 반환합니다.
    /// </summary>
    protected virtual float GetRayLength()
    {
        return boxCollider.size.y * 0.15f;
    }

    /// <summary>
    /// 지면 체크 시작 위치의 Y 오프셋을 반환합니다.
    /// </summary>
    protected virtual float GetOriginYOffset()
    {
        return boxCollider.size.y * 0.5f;
    }

    /// <summary>
    /// 마찰력을 업데이트합니다.
    /// </summary>
    protected virtual void UpdateDrag()
    {
        rb.drag = isFalling ? fallingDrag : normalDrag;
    }

    /// <summary>
    /// 다른 큐브에 의해 부스트될 때 호출되는 점프 메서드입니다.
    /// </summary>
    /// <param name="boostForce">부스트 힘</param>
    public virtual void BoostJump(float boostForce)
    {
        if (rb)
        {
            Managers.Sound.SFX2DPlay(jumpSound);
            rb.AddForce(Vector3.up * boostForce, ForceMode.Impulse);
        }
    }
}
