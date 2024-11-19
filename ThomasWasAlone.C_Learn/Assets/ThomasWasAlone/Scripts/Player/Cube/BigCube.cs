using UnityEngine;

public class BigCube : BaseCube
{
    protected override Vector3 GetDefaultBoxSize()
    {
        var size = boxCollider.size * 0.9f;
        size.y = boxCollider.size.y * 0.05f;
        return size;
    }

    protected override float GetRayLength()
    {
        return boxCollider.size.y * 0.2f;
    }

    protected override float GetOriginYOffset()
    {
        return boxCollider.size.y * 0.8f;
    }
}
