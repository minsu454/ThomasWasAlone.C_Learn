using Common.Yield;
using System.Collections;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private BaseCube[] cubes;
    [SerializeField] private CameraController cameraController;
    
    private int currentCubeIndex = 0;
    private BaseCube currentCube => cubes[currentCubeIndex];
    
    private void Start()
    {
        SwitchToCube(0);
    }

    public void Move(float horizontal, float vertical, Transform cameraTransform)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (right * horizontal + forward * vertical).normalized;
        if (moveDirection != Vector3.zero)
        {
            Vector3 movement = moveDirection * (currentCube.MoveSpeed * Time.deltaTime);
            currentCube.transform.position += movement;
        }
    }

    public void Jump()
    {
        if (!currentCube.IsGrounded) return;
        
        if (currentCube is LightCube)
        {
            var mover = currentCube.GetComponent<LightCubeMover>();
            mover.Jump(currentCube.JumpForce);
        }
        else
        {
            Rigidbody rb = currentCube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * currentCube.JumpForce, ForceMode.Impulse);
            }
        }
    }

    public void SwitchToNextCube()
    {
        currentCubeIndex = (currentCubeIndex + 1) % cubes.Length;
        SwitchToCube(currentCubeIndex);
    }

    private void SwitchToCube(int index)
    {
        cameraController.SetTarget(cubes[index].transform);
    }
} 