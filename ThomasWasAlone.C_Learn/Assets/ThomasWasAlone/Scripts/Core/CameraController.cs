using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    
    [SerializeField] private float distance = 8f;
    [SerializeField] private float height = 5f;
    [SerializeField] private float smoothSpeed = 5f;
    
    private Transform target;
    private float currentRotationAngle;
    private Vector3 currentVelocity;
    
    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        if (!target) return;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentRotationAngle += 90f;
            if (currentRotationAngle >= 360f) currentRotationAngle = 0f;
            // 플레이어도 회전
            target.rotation = Quaternion.Euler(0, currentRotationAngle, 0);
        }

        float rad = currentRotationAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(
            Mathf.Sin(rad) * distance,
            height,
            -Mathf.Cos(rad) * distance
        );
        
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed * Time.deltaTime);
        transform.LookAt(target);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        currentRotationAngle = 0f;
        target.rotation = Quaternion.Euler(0, currentRotationAngle, 0);
    }
} 