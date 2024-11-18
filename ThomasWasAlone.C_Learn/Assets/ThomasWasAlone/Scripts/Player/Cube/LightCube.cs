using UnityEngine;

public class LightCube : BaseCube 
{
    protected override void InitializeGroundCheck()
    {
        base.InitializeGroundCheck();
        rayLength = boxCollider.size.y * 0.3f;
    }
} 