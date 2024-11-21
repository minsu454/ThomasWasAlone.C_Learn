using Common.Yield;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingPlatform : MonoBehaviour, IMapBlockLogic
{
    // MapInput에서
    // 1. 블럭 2개 생성. (각각 종착점)
    // 2. 무빙플랫폼 생성 후 addcomponent<MovingPlatform>()
    // 3. start와 end에 블럭 2개 넣기
    // MovingPlatform에서
    // 1. start와 end 왔다 갔다 하기.
    private Transform platform;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    private float moveSpeed = 2f; // 움직임 속도
    public MapObjType mapObjType = MapObjType.MovingPlatform;
    public Coroutine coroutine;
    // start와 end 위치를 설정하는 메서드
    public void SetStartAndEnd(Vector3 start, Vector3 end)
    {
        startPosition = start;
        endPosition = end;
        transform.position = startPosition; // 처음에는 start 위치로 설정
    }
    public void StartCoroutineObj()
    {
        //한번 발동하면 아이템이 사라짐. 코루틴 중복 발동 예외 처리 필요 없을듯.
        coroutine = StartCoroutine(MapLogicCoroutine());
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 태그로 비교
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어가 충돌");
            collision.gameObject.transform.SetParent(transform, true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 태그로 비교
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어가 충돌 해제");
            collision.gameObject.transform.SetParent(null, true);
        }
    }
    public IEnumerator MapLogicCoroutine()
    {
        Debug.Log("코루틴 시작!");
        gameObject.transform.position = startPosition;
        Vector3 currentStartPos = startPosition;
        Vector3 currentEndPos = endPosition;

        while (true)  // 무한 루프
        {
            float journeyLength = Vector3.Distance(currentStartPos, currentEndPos);  // 이동할 거리
            float startTime = Time.time;  // 시작 시간

            while (true)
            {
                // 이동한 비율 계산
                float distanceCovered = (Time.time - startTime) * moveSpeed;
                float fractionOfJourney = distanceCovered / journeyLength;

                // Lerp를 사용해 두 점 사이를 부드럽게 이동
                transform.position = Vector3.Lerp(currentStartPos, currentEndPos, fractionOfJourney);

                // 끝에 도달하면 루프 종료
                if (fractionOfJourney >= 1f)
                {
                    // 위치를 반대쪽으로 변경 (끝에 도달한 후 반대 방향으로 이동)
                    Vector3 temp = currentStartPos;
                    currentStartPos = currentEndPos;
                    currentEndPos = temp;
                    break;  // 내부 while 루프 종료
                }

                yield return null;  // 한 프레임 대기
            }

            // 이동이 끝난 후 잠시 대기 (2초)
            yield return YieldCache.WaitForSeconds(2f);
        }
    }
}
