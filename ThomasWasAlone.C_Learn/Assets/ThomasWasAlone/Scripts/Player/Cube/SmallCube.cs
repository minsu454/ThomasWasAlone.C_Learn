using UnityEngine;

public class SmallCube : BaseCube
{
    protected override Vector3 GetDefaultBoxSize()
    {
        var size = boxCollider.size * 0.8f;
        size.y = boxCollider.size.y * 0.05f;
        return size;
    }

    protected override float GetRayLength()
    {
        return boxCollider.size.y * 0.15f;
    }

    protected override float GetOriginYOffset()
    {
        return boxCollider.size.y * 0.1f;
    }
}
