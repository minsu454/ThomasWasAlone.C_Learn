using UnityEngine;

public class SmallCube : BaseCube
{
    protected override void InitializeGroundCheck()
    {
        rayLength = boxCollider.size.y * 0.15f;
        boxSize = boxCollider.size * 0.8f;
        boxSize.y = boxCollider.size.y * 0.05f;
    }

    protected override void CheckGrounded()
    {
        origin = transform.position;
        origin.y -= boxCollider.size.y * 0.1f;

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
