using UnityEngine;

public class BigCube : BaseCube
{
    protected override void InitializeGroundCheck()
    {
        boxSize = boxCollider.size * 0.9f;
        boxSize.y = boxCollider.size.y * 0.05f;
        rayLength = boxCollider.size.y * 0.2f;
    }

    protected override void CheckGrounded()
    {
        origin = transform.position;
        origin.y -= boxCollider.size.y * 0.8f;

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
