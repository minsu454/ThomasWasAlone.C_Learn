using UnityEngine;

public class SmallCube : BaseCube
{
    /// <summary>
    /// 작은 큐브의 지면 체크를 위한 박스 크기를 반환합니다.
    /// 작은 큐브는 더 작은 영역을 체크합니다.
    /// </summary>
    protected override Vector3 GetDefaultBoxSize()
    {
        var size = boxCollider.size * 0.8f;
        size.y = boxCollider.size.y * 0.05f;
        return size;
    }

    /// <summary>
    /// 작은 큐브의 지면 체크를 위한 레이 길이를 반환합니다.
    /// 작은 큐브는 더 짧은 레이를 사용합니다.
    /// </summary>
    protected override float GetRayLength()
    {
        return boxCollider.size.y * 0.15f;
    }

    /// <summary>
    /// 작은 큐브의 지면 체크 시작 위치의 Y 오프셋을 반환합니다.
    /// 작은 큐브는 더 낮은 위치에서 체크를 시작합니다.
    /// </summary>
    protected override float GetOriginYOffset()
    {
        return boxCollider.size.y * 0.1f;
    }
}
