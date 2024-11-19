using UnityEngine;
using System.Collections.Generic;

public class JumpBoostCube : BaseCube
{
    [SerializeField] private float boostForce = 10f;
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
        if (collision.contacts.Length == 0) return;
        if (collision.gameObject.TryGetComponent<BaseCube>(out BaseCube cube))
        {   // Stay 추가로 인한 예외처리
            if (boostedCubes.Contains(cube)) return;
            
            Vector3 contactNormal = collision.contacts[0].normal;
            float contactY = collision.contacts[0].point.y;
            float cubeBottom = transform.position.y - (boxCollider.size.y * 0.5f);
            float cubeHeight = boxCollider.size.y;
            
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