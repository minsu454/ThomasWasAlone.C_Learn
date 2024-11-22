using UnityEngine;

public class LightCube : BaseCube
{
    /// <summary>
    /// 가벼운 큐브의 지면 체크를 위한 레이 길이를 반환합니다.
    /// 가벼운 큐브는 중간 길이의 레이를 사용합니다.
    /// </summary>
    protected override float GetRayLength()
    {
        return boxCollider.size.y * 0.3f;
    }
}