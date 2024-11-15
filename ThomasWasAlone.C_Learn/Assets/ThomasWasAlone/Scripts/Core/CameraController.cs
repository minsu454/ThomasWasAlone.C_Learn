using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    
    [SerializeField] private Vector3[] cameraPositions;
    [SerializeField] private float smoothSpeed = 5f;
    
    private Transform target;
    private int currentPositionIndex;
    private Vector3 desiredPosition;
    private Vector3 currentVelocity;
    
    private void Awake()
    {
        Instance = this;
        cameraPositions = new[]
        {
            new Vector3(0, 5f, -8f),
            new Vector3(-8f, 5f, 4f),
            new Vector3(8f, 5f, 4f) 
        };
    }

    private void LateUpdate()
    {
        if (!target) return;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentPositionIndex = (currentPositionIndex + 1) % cameraPositions.Length;
        }

        desiredPosition = target.position + cameraPositions[currentPositionIndex];
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed * Time.deltaTime);
        transform.LookAt(target);
    }

    public void SetTarget(Transform newTarget) => target = newTarget;
} 