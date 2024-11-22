using UnityEngine;
using System.Collections.Generic;

public class JumpBoostCube : BaseCube
{
    // 다른 큐브를 부스트할 때 적용되는 힘
    [SerializeField] private float boostForce = 230f;
    // 이미 부스트된 큐브들을 추적하는 해쉬셋
    private HashSet<BaseCube> boostedCubes = new HashSet<BaseCube>();

    /// <summary>
    /// 충돌이 시작될 때 부스트 점프를 체크합니다.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        CheckBoostJump(collision);
    }

    /// <summary>
    /// 충돌이 지속되는 동안 부스트 점프를 체크합니다.
    /// </summary>
    private void OnCollisionStay(Collision collision)
    {
        CheckBoostJump(collision);
    }

    /// <summary>
    /// 충돌이 종료될 때 부스트된 큐브 목록에서 제거합니다.
    /// </summary>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BaseCube>(out BaseCube cube))
        {
            boostedCubes.Remove(cube);
        }
    }

    /// <summary>
    /// 부스트 큐브의 지면 체크를 위한 레이 길이를 반환합니다.
    /// </summary>
    protected override float GetRayLength()
    {
        return boxCollider.size.y * 0.3f;
    }

    /// <summary>
    /// 부스트 큐브의 지면 체크 시작 위치의 Y 오프셋을 반환합니다.
    /// </summary>
    protected override float GetOriginYOffset()
    {
        return boxCollider.size.y * 0.15f;
    }

    /// <summary>
    /// 충돌한 큐브에 대해 부스트 점프 조건을 확인하고 적용합니다.
    /// </summary>
    private void CheckBoostJump(Collision collision)
    {
        // 호출은 됐으나 접촉지점이 없는 경우 예외처리
        if (collision.contacts.Length == 0) return;
        // BaseCube 컴포넌트가 있는 경우에만 처리
        if (collision.gameObject.TryGetComponent<BaseCube>(out BaseCube cube))
        {
            // HashSet에 이미 존재하는 경우 예외처리(Enter와 Stay 중복 호출 방지)
            if (boostedCubes.Contains(cube)) return;

            Vector3 contactNormal = collision.contacts[0].normal;
            float contactY = collision.contacts[0].point.y;
            float cubeBottom = transform.position.y - (boxCollider.size.y * 0.5f);
            float cubeHeight = boxCollider.size.y;
            // 내적을 통해 접촉 법선 벡터가 아래쪽을 향하는지 확인 && 접촉지점이 특정 높이 이상인 경우에만
            if (Vector3.Dot(contactNormal, Vector3.up) < -0.5f &&
                contactY > cubeBottom + (cubeHeight * 0.3f))
            {
                cube.BoostJump(boostForce);
                boostedCubes.Add(cube);
            }
        }
    }
}