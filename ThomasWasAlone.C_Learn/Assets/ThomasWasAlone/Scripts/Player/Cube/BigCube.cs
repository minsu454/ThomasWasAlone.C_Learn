using UnityEngine;

public class BigCube : BaseCube
{
    /// <summary>
    /// 큰 큐브의 지면 체크를 위한 박스 크기를 반환합니다.
    /// 큰 큐브는 더 넓은 영역을 체크합니다.
    /// </summary>
    protected override Vector3 GetDefaultBoxSize()
    {
        var size = boxCollider.size * 0.9f;
        size.y = boxCollider.size.y * 0.05f;
        return size;
    }

    /// <summary>
    /// 큰 큐브의 지면 체크를 위한 레이 길이를 반환합니다.
    /// 큰 큐브는 더 긴 레이를 사용합니다.
    /// </summary>
    protected override float GetRayLength()
    {
        return boxCollider.size.y * 0.2f;
    }

    /// <summary>
    /// 큰 큐브의 지면 체크 시작 위치의 Y 오프셋을 반환합니다.
    /// 큰 큐브는 더 높은 위치에서 체크를 시작합니다.
    /// </summary>
    protected override float GetOriginYOffset()
    {
        return boxCollider.size.y * 0.8f;
    }
}
