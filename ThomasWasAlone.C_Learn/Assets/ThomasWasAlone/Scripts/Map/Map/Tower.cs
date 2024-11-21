using Common.Event;
using Common.Yield;
using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour, IMapBlockLogic
{
    // MapInput에서
    // 1.생성 후 pos로 위치 넘겨주기
    // Tower에서
    // 1. 뿔 방향으로 발사, 
    // 2. 몇초후 pos 로 복귀
    public Vector3 startPos;
    public Vector3 targetPos;
    public float moveSpeed = 5f; // 이동 속도
    public MapObjType mapObjType = MapObjType.Tower;
    public Coroutine coroutine;
    public float rayDistance = 10f;  // 레이 발사 거리
    public LayerMask layerMask;      // 레이 적중을 확인할 레이어
    public float boxx = 1f;
    public float boxy = 3f;
    public float boxz = 2f;
    private Vector3 hitPoint;
    public void StartCoroutineObj()
    {
        //한번 발동하면 아이템이 사라짐. 코루틴 중복 발동 예외 처리 필요 없을듯.

        coroutine = StartCoroutine(MapLogicCoroutine());
    }
    private void Update()
    {
        if (coroutine == null)
        {
            // 정면으로 레이 발사
            ShootBoxRay();
        }
    }
    private void ShootBoxRay()
    {
        Vector3 rayDirection = transform.right;
        Vector3 rayOrigin = transform.position;

        Vector3 boxSize = new Vector3(boxx, boxy, boxz);  // 원하는 크기로 조정
        RaycastHit hit;
        if (Physics.BoxCast(rayOrigin, boxSize / 2, rayDirection, out hit, Quaternion.identity, rayDistance, layerMask))
        {
            StartCoroutineObj();
            Debug.Log("BoxRay 충돌한 오브젝트: " + hit.collider.gameObject.name);
            hitPoint = hit.point;
        }
    }

    private void OnDrawGizmos()
    {
        if (hitPoint != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(hitPoint, new Vector3(boxx, boxy, boxz));
        }
    }

    public IEnumerator MapLogicCoroutine()
    {
        startPos = transform.position;  // 원래 위치 설정
        // 타워가 목표 위치로 부드럽게 이동
        float journeyLength = Vector3.Distance(startPos, targetPos); // 이동 거리 계산
        float startTime = Time.time;  // 이동 시작 시간

        // 목표 위치로 이동
        while (Vector3.Distance(transform.position, targetPos) > 0.1f) // 목표 위치에 가까워질 때까지
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(startPos, targetPos, fractionOfJourney);

            yield return null;  // 한 프레임 대기
        }

        yield return YieldCache.WaitForSeconds(2f);
        transform.position = startPos;
        StopCoroutine(coroutine);
        coroutine = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BaseCube>(out _))
        {
            EventManager.Dispatch(GameEventType.KillAllCubes, null);
        }
    }
}
