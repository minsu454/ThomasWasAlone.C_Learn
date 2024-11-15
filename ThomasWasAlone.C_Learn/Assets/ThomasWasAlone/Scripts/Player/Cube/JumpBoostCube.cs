using UnityEngine;

public class JumpBoostCube : BaseCube
{
    [SerializeField] private float boostForce = 15f;
    
    protected override void CheckGrounded()
    {
        float rayLength = boxCollider.size.y * 0.15f;
        Vector3 boxSize = boxCollider.size * 0.9f;
        boxSize.y = boxCollider.size.y * 0.1f;
        
        Vector3 origin = transform.position - new Vector3(0, boxCollider.size.y * 0.15f, 0);
        
        isGrounded = Physics.BoxCast(
            origin,
            boxSize * 0.5f,
            Vector3.down,
            out RaycastHit hit,
            transform.rotation,
            rayLength
        );
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision?.gameObject || collision.contacts.Length == 0) return;
        
        if (collision.gameObject.TryGetComponent<BaseCube>(out BaseCube cube) && 
            collision.contacts[0].point.y > transform.position.y)
        {
            cube.BoostJump(boostForce);
        }
    }
}