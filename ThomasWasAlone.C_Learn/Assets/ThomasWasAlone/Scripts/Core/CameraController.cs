using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 카메라와 타겟 사이의 거리
    [SerializeField] private float distance = 8f;
    // 카메라의 높이
    [SerializeField] private float height = 5f;
    // 카메라 이동의 부드러움 정도
    [SerializeField] private float smoothSpeed = 5f;
    // 카메라 회전 각도
    [SerializeField] private float rotationAngle = 90f;

    // 현재 추적 중인 타겟
    private Transform target;
    // 현재 카메라의 회전 각도
    private float currentRotationAngle;
    private Vector3 currentVelocity;
    private WallTransparencyController wallTransparency;

    private void Awake()
    {
        wallTransparency = GetComponent<WallTransparencyController>();
    }

    private void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPosition = CalculateDesiredPosition();
        UpdateCameraPosition(desiredPosition);
        transform.LookAt(target);
    }

    /// <summary>
    /// 카메라의 목표 위치를 계산합니다.
    /// </summary>
    private Vector3 CalculateDesiredPosition()
    {
        float rad = currentRotationAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(
            Mathf.Sin(rad) * distance,
            height,
            -Mathf.Cos(rad) * distance
        );

        return target.position + offset;
    }

    /// <summary>
    /// 카메라 위치를 부드럽게 업데이트합니다.
    /// </summary>
    private void UpdateCameraPosition(Vector3 desiredPosition)
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref currentVelocity,
            smoothSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// 카메라를 90도 회전시킵니다.
    /// </summary>
    public void Rotate()
    {
        currentRotationAngle += rotationAngle;
        if (currentRotationAngle >= 360f)
        {
            currentRotationAngle = 0f;
        }
        if (target != null)
        {
            target.rotation = Quaternion.Euler(0, currentRotationAngle, 0);
        }
    }

    /// <summary>
    /// 새로운 타겟을 설정하고 카메라를 초기화합니다.
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        currentRotationAngle = 0f;
        target.rotation = Quaternion.Euler(0, currentRotationAngle, 0);

        if (wallTransparency != null)
        {
            wallTransparency.SetTarget(newTarget);
        }
    }
}