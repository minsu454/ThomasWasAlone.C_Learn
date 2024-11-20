using Common.Yield;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Common.Event;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private BaseCube[] cubes;
    [SerializeField] private CameraController cameraController;
    
    private int currentCubeIndex = 0;
    private BaseCube currentCube => cubes[currentCubeIndex];
    
    public void Init(List<SpawnData> data)
    {
        cubes = new BaseCube[data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            GameObject cubePrefab = Managers.Cube.ReturnPlayer(data[i].Type);
            GameObject cubeGo = Instantiate(cubePrefab, data[i].Pos, Quaternion.identity);

            cubes[i] = cubeGo.GetComponent<BaseCube>();
        }

        EventManager.Subscribe(GameEventType.ChangeCube, SwitchToNextCube);

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

    public void SwitchToNextCube(object args)
    {
        currentCubeIndex = (currentCubeIndex + 1) % cubes.Length;
        SwitchToCube(currentCubeIndex);
    }

    private void SwitchToCube(int index)
    {
        cameraController.SetTarget(cubes[index].transform);
    }

    public void KillAllCubes()
    {
        foreach (var cube in cubes)
        {
            // 스케일 감소
            cube.transform.DOScale(Vector3.zero, 0.5f)
                .SetEase(Ease.InBack);
            
            // 회전
            cube.transform.DOShakeRotation(0.5f, 45f, 10, 90)
                .OnComplete(() => {
                    cube.gameObject.SetActive(false);
                    // 스케일 복구
                    cube.transform.localScale = Vector3.one;
                });
        }
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(GameEventType.ChangeCube, SwitchToNextCube);
    }
} 