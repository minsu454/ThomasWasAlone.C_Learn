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
    private Vector3[] initialPositions;
    private Vector3[] initialScales;

    private Dictionary<string, Coroutine> coroutines = new Dictionary<string, Coroutine>();
    private List<Sequence> tweenSequences = new List<Sequence>();

    public void Init(List<SpawnData> data)
    {
        cubes = new BaseCube[data.Count];
        initialPositions = new Vector3[data.Count];
        initialScales = new Vector3[data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            GameObject cubePrefab = Managers.Cube.ReturnPlayer(data[i].Type);
            GameObject cubeGo = Instantiate(cubePrefab, data[i].Pos, Quaternion.identity);

            cubes[i] = cubeGo.GetComponent<BaseCube>();
        }

        EventManager.Subscribe(GameEventType.ChangeCube, SwitchToNextCube);

        SwitchToCube(0);
        SaveInitialTransforms();
    }

    private void SaveInitialTransforms()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            initialPositions[i] = cubes[i].transform.position;
            initialScales[i] = cubes[i].transform.localScale;
        }
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
        if (coroutines.TryGetValue("Reset", out var previousCoroutine))
            StopCoroutine(previousCoroutine);

        DieMotion();
        coroutines["Reset"] = StartCoroutine(ResetCubesRoutine());
    }

    private IEnumerator ResetCubesRoutine()
    {
        yield return new WaitForSeconds(0.6f);

        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].gameObject.SetActive(true);
            cubes[i].transform.position = initialPositions[i];
            cubes[i].transform.localScale = initialScales[i];
        }

        coroutines.Remove("Reset");
    }

    private void DieMotion()
    {
        foreach (var sequence in tweenSequences)
        {
            sequence?.Kill();
        }
        tweenSequences.Clear();

        for (int i = 0; i < cubes.Length; i++)
        {
            Sequence sequence = DOTween.Sequence();
            BaseCube cube = cubes[i];
            int index = i;

            sequence.Join(cube.transform.DOScale(Vector3.zero, 0.5f)
                .SetEase(Ease.InBack));
            sequence.Join(cube.transform.DOShakeRotation(0.5f, 45f, 10, 90));

            sequence.OnComplete(() =>
            {
                cube.gameObject.SetActive(false);
                cube.transform.localScale = initialScales[index];
                cube.transform.rotation = Quaternion.identity;
            });

            tweenSequences.Add(sequence);
        }
    }

    private void OnDestroy()
    {
        foreach (var sequence in tweenSequences)
        {
            sequence?.Kill();
        }
        tweenSequences.Clear();

        foreach (var coroutine in coroutines.Values)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }
        coroutines.Clear();

        EventManager.Unsubscribe(GameEventType.ChangeCube, SwitchToNextCube);
    }
}