using UnityEngine;

public class JumpBoostCube : BaseCube
{
    [SerializeField] private float boostForce = 15f;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision?.gameObject || collision.contacts.Length == 0) return;
        
        if (collision.gameObject.TryGetComponent<BaseCube>(out BaseCube cube))
        {
            Vector3 contactNormal = collision.contacts[0].normal;
            
            float cubeTopY = transform.position.y + (boxCollider.size.y * 0.5f);
            float collisionHeight = collision.contacts[0].point.y;
            
            if (Vector3.Dot(contactNormal, Vector3.up) < -0.7f &&
                collisionHeight > cubeTopY - (boxCollider.size.y * 0.3f))
            {
                cube.BoostJump(boostForce);
            }
        }
    }

    protected override void InitializeGroundCheck()
    {
        base.InitializeGroundCheck();
        rayLength = boxCollider.size.y * 0.3f;
    }

    protected override void CheckGrounded()
    {
        origin = transform.position;
        origin.y -= boxCollider.size.y * 0.15f;

        isGrounded = Physics.BoxCast(
            origin,
            boxSize * 0.5f,
            Vector3.down,
            out _,
            Quaternion.identity,
            rayLength
        );
    }
}