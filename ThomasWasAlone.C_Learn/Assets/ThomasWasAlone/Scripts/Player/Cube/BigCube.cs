using UnityEngine;

public class BigCube : BaseCube
{
    protected override void CheckGrounded()
    {
        float rayLength = boxCollider.size.y * 0.15f;
        Vector3 boxSize = boxCollider.size * 0.9f;
        boxSize.y = boxCollider.size.y * 0.1f;
        
        Vector3 origin = transform.position - new Vector3(0, boxCollider.size.y * 0.4f, 0);
        
        isGrounded = Physics.BoxCast(
            origin,
            boxSize * 0.5f,
            Vector3.down,
            out RaycastHit hit,
            transform.rotation,
            rayLength
        );
    }
}
