using Common.Yield;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Common.Event;

public class CubeManager : MonoBehaviour
{
    // 점프 시 재생할 사운드
    [SerializeField] private AudioClip jumpSound;
    // 사망 시 재생할 사운드
    [SerializeField] private AudioClip dieSound;
    // 스폰 시 재생할 사운드
    [SerializeField] private AudioClip spawnSound;
    // 게임에서 사용할 큐브들의 배열
    [SerializeField] private BaseCube[] cubes;

    // 입력 제어를 위한 컨트롤러
    [SerializeField] private InputController inputController;

    // 카메라 제어를 위한 컨트롤러
    private CameraController cameraController;
    // 현재 선택된 큐브의 인덱스
    private int currentCubeIndex = 0;
    // 현재 선택된 큐브에 대한 프로퍼티
    private BaseCube currentCube => cubes[currentCubeIndex];
    // 큐브들의 초기 위치를 저장하는 배열
    private Vector3[] initialPositions;
    // 큐브들의 초기 크기를 저장하는 배열
    private Vector3[] initialScales;

    // 코루틴들을 관리하는 딕셔너리
    private Dictionary<string, Coroutine> coroutines = new Dictionary<string, Coroutine>();
    // DOTween 시퀀스들을 관리하는 리스트
    private List<Sequence> tweenSequences = new List<Sequence>();

    /// <summary>
    /// 큐브 생성 시 실행되는 애니메이션을 처리합니다.
    /// </summary>
    private void SpawnAnimation(BaseCube cube, Vector3 targetScale)
    {
        cube.transform.localScale = Vector3.zero;

        Managers.Sound.SFX2DPlay(spawnSound);

        Sequence spawnSequence = DOTween.Sequence();
        //* 큐브가 나타나는 효과
        spawnSequence.Append(cube.transform.DOScale(targetScale * 1.2f, 0.2f)
            .SetEase(Ease.OutQuad));

        //* 큐브가 줄어들었다가 원래 크기로 돌아오는 효과
        spawnSequence.Append(cube.transform.DOScale(targetScale, 0.1f)
            .SetEase(Ease.InOutQuad));

        tweenSequences.Add(spawnSequence);
    }

    /// <summary>
    /// 모든 큐브 사망 이벤트를 처리합니다.
    /// </summary>
    private void OnKillAllCubes(object args)
    {
        KillAllCubes();
    }

    /// <summary>
    /// 입력 잠금 상태를 처리합니다.
    /// </summary>
    private void LookInput(object args)
    {
        inputController.enabled = (bool)args;
    }

    /// <summary>
    /// CubeManager를 초기화하고 큐브들을 생성합니다.
    /// </summary>
    public void Init(List<SpawnData> data)
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        if (cameraController == null)
        {
            return;
        }

        cubes = new BaseCube[data.Count];
        initialPositions = new Vector3[data.Count];
        initialScales = new Vector3[data.Count];

        EventManager.Subscribe(GameEventType.KillAllCubes, OnKillAllCubes);
        EventManager.Subscribe(GameEventType.LockInput, LookInput);

        EventManager.Dispatch(GameEventType.LockInput, false);

        StartCoroutine(SpawnCubesSequentially(data));
    }

    /// <summary>
    /// 큐브들을 순차적으로 생성하는 코루틴입니다.
    /// </summary>
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
        EventManager.Dispatch(GameEventType.LockInput, true);

        EventManager.Subscribe(GameEventType.ChangeCube, SwitchToNextCube);
    }

    /// <summary>
    /// 현재 선택된 큐브를 이동시킵니다.
    /// </summary>
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

    /// <summary>
    /// 현재 선택된 큐브를 점프시킵니다.
    /// </summary>
    public void Jump()
    {
        if (!currentCube.IsGrounded) return;

        if (currentCube is LightCube)
        {
            Managers.Sound.SFX2DPlay(jumpSound);
            var mover = currentCube.GetComponent<LightCubeMover>();
            mover.Jump(currentCube.JumpForce);
        }
        else
        {
            Rigidbody rb = currentCube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Managers.Sound.SFX2DPlay(jumpSound);
                rb.AddForce(Vector3.up * currentCube.JumpForce, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// 다음 큐브로 전환합니다.
    /// </summary>
    public void SwitchToNextCube(object args)
    {
        currentCubeIndex = (currentCubeIndex + 1) % cubes.Length;
        SwitchToCube(currentCubeIndex);
    }

    /// <summary>
    /// 지정된 인덱스의 큐브로 전환합니다.
    /// </summary>
    private void SwitchToCube(int index)
    {
        cameraController.SetTarget(cubes[index].transform);
    }

    /// <summary>
    /// 모든 큐브를 사망 상태로 만듭니다.
    /// </summary>
    public void KillAllCubes()
    {
        if (coroutines.TryGetValue("Reset", out var previousCoroutine))
            StopCoroutine(previousCoroutine);
        Managers.Sound.SFX2DPlay(dieSound);
        DieMotion();
        coroutines["Reset"] = StartCoroutine(ResetCubesRoutine());
    }

    /// <summary>
    /// 큐브들을 초기 상태로 리셋하는 코루틴입니다.
    /// </summary>
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

    /// <summary>
    /// 큐브들의 사망 모션을 실행합니다.
    /// </summary>
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

    /// <summary>
    /// 객체가 파괴될 때 리소스를 정리합니다.
    /// </summary>
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
        EventManager.Unsubscribe(GameEventType.LockInput, LookInput);
    }
}