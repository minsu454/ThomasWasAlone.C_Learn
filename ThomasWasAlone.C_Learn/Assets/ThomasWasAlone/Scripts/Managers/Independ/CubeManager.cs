using Common.Yield;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Common.Event;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip spawnSound;
    [SerializeField] private BaseCube[] cubes;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private LayerMask wallLayer;

    private int currentCubeIndex = 0;
    private BaseCube currentCube => cubes[currentCubeIndex];
    private Vector3[] initialPositions;
    private Vector3[] initialScales;

    private Dictionary<string, Coroutine> coroutines = new Dictionary<string, Coroutine>();
    private List<Sequence> tweenSequences = new List<Sequence>();


    private void SpawnAnimation(BaseCube cube, Vector3 targetScale)
    {
        cube.transform.localScale = Vector3.zero;

        Sequence spawnSequence = DOTween.Sequence();

        Managers.Sound.SFX2DPlay(spawnSound);
        //* 큐브가 나타나는 효과
        spawnSequence.Append(cube.transform.DOScale(targetScale * 1.2f, 0.2f)
            .SetEase(Ease.OutQuad));

        //* 큐브가 줄어들었다가 원래 크기로 돌아오는 효과
        spawnSequence.Append(cube.transform.DOScale(targetScale, 0.1f)
            .SetEase(Ease.InOutQuad));

        tweenSequences.Add(spawnSequence);
    }

    private void OnKillAllCubes(object args)
    {
        KillAllCubes();
    }

    public void Init(List<SpawnData> data)
    {
        cubes = new BaseCube[data.Count];
        initialPositions = new Vector3[data.Count];
        initialScales = new Vector3[data.Count];
        EventManager.Subscribe(GameEventType.KillAllCubes, OnKillAllCubes);
        StartCoroutine(SpawnCubesSequentially(data));
    }

    private IEnumerator SpawnCubesSequentially(List<SpawnData> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            GameObject cubePrefab = Managers.Cube.ReturnPlayer(data[i].Type);
            GameObject cubeGo = Instantiate(cubePrefab, data[i].Pos, Quaternion.identity);

            BaseCube cube = cubeGo.GetComponent<BaseCube>();
            cubes[i] = cube;

            //* 초기 위치와 스케일 저장
            initialPositions[i] = data[i].Pos;
            initialScales[i] = cube.transform.localScale;

            //* 스폰 애니메이션 실행
            SpawnAnimation(cube, initialScales[i]);

            //* 카메라 전환
            cameraController.SetTarget(cube.transform);

            //* 큐브 커서 변경
            EventManager.Dispatch(GameEventType.ChangeCube, null);

            //* 다음 큐브 생성 전 대기
            yield return YieldCache.WaitForSeconds(0.5f);
        }

        //* 모든 큐브 생성 후 첫 번째 큐브로 카메라 전환
        currentCubeIndex = 0;
        SwitchToCube(0);

        EventManager.Dispatch(GameEventType.ChangeCube, null);
        EventManager.Subscribe(GameEventType.ChangeCube, SwitchToNextCube);
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

            Vector3 originalPosition = currentCube.transform.position;

            float rayDistance = 0.5f;
            if (!Physics.Raycast(currentCube.transform.position, moveDirection, rayDistance, wallLayer))
            {
                currentCube.transform.position += movement;

                if (Physics.OverlapBox(currentCube.transform.position,
                    currentCube.GetComponent<BoxCollider>().size / 2,
                    currentCube.transform.rotation,
                    wallLayer).Length > 0)
                {
                    currentCube.transform.position = originalPosition;
                }
            }
        }
    }

    public void Jump()
    {
        if (!currentCube.IsGrounded) return;

        Managers.Sound.SFX2DPlay(jumpSound);

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
        Managers.Sound.SFX2DPlay(dieSound);
        DieMotion();
        coroutines["Reset"] = StartCoroutine(ResetCubesRoutine());
    }

    private IEnumerator ResetCubesRoutine()
    {
        yield return YieldCache.WaitForSeconds(0.6f);

        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].gameObject.SetActive(true);
            cubes[i].transform.position = initialPositions[i];
            cubes[i].transform.localScale = initialScales[i];
        }
        Physics.SyncTransforms();
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
        EventManager.Unsubscribe(GameEventType.KillAllCubes, OnKillAllCubes);
    }
}