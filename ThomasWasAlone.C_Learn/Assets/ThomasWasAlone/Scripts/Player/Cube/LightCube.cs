using UnityEngine;

public class LightCube : BaseCube 
{
    protected override float GetRayLength()
    {
        return boxCollider.size.y * 0.3f;
    }
} 