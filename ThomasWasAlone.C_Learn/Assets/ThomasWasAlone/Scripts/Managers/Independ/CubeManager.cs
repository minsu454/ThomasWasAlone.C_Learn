using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private BaseCube[] cubes;
    private InputController inputController;
    
    private int currentCubeIndex = 0;
    private BaseCube currentCube => cubes[currentCubeIndex];
    
    private void Awake()
    {
        inputController = GetComponent<InputController>();
    }

    private void Start()
    {
        SwitchToCube(0);
    }

    private void Update()
    {
        CheckCubeSwitch();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        if (inputController.JumpRequested)
        {
            ApplyJump();
        }
    }

    private void CheckCubeSwitch()
    {
        if (inputController.SwitchRequested)
        {
            currentCubeIndex = (currentCubeIndex + 1) % cubes.Length;
            SwitchToCube(currentCubeIndex);
            inputController.ResetSwitchRequest(); // TAP 입력 상태 초기화
        }
    }

    private void ApplyMovement()
    {
        Vector3 moveDirection = inputController.MoveDirection;
        if (moveDirection != Vector3.zero)
        {
            Vector3 movement = moveDirection * (currentCube.MoveSpeed * Time.fixedDeltaTime);
            currentCube.transform.position += movement;
        }
    }

    private void ApplyJump()
    {
        if (currentCube.IsGrounded)
        {
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
        inputController.ResetJumpRequest();
    }

    private void SwitchToCube(int index)
    {
        CameraController.Instance.SetTarget(cubes[index].transform);
    }
} 