using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float distance = 8f;
    [SerializeField] private float height = 5f;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float rotationAngle = 90f;
    
    private Transform target;
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

    private void UpdateCameraPosition(Vector3 desiredPosition)
    {
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            desiredPosition, 
            ref currentVelocity, 
            smoothSpeed * Time.deltaTime
        );
    }

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