using UnityEngine;
using System.Collections.Generic;

public class JumpBoostCube : BaseCube
{
    [SerializeField] private float boostForce = 200f;
    // 이미 부스트된 큐브들을 확인하기위한 HashSet
    private HashSet<BaseCube> boostedCubes = new HashSet<BaseCube>();

    private void OnCollisionEnter(Collision collision)
    {
        CheckBoostJump(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        CheckBoostJump(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BaseCube>(out BaseCube cube))
        {
            boostedCubes.Remove(cube);
        }
    }

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

    protected override float GetRayLength()
    {
        return boxCollider.size.y * 0.3f;
    }

    protected override float GetOriginYOffset()
    {
        return boxCollider.size.y * 0.15f;
    }
}