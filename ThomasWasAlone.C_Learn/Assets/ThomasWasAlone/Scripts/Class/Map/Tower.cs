using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // MapInput에서
    // 1.생성 후 pos로 위치 넘겨주기
    // Tower에서
    // 1. 뿔 방향으로 발사, 
    // 2. 몇초후 pos 로 복귀
    public Vector3 startPos;
    public Vector3 targetPos;
    public float moveSpeed = 5f; // 이동 속도
    void Start()
    {
        startPos = transform.position;  // 원래 위치 설정
        targetPos = startPos + new Vector3(10, 0, 0);
        StartCoroutine(TowerCoroutine());
    }

    private IEnumerator TowerCoroutine()
    {
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

        yield return new WaitForSeconds(2f);
        transform.position = startPos;
        StartCoroutine(TowerCoroutine());
    }


}
