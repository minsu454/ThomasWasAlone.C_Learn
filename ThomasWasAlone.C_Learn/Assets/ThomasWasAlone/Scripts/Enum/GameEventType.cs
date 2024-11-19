/// <summary>
/// 게임 이벤트 타입(구독하기 위한)
/// </summary>
public enum GameEventType
{
    StageChoice = 0,        //스테이지 골랐을 때
    LockPlayerMove,         //플레이어 움직임이 제한되었을 때
    ChangeViewPoint,        //카메라 움직임이 감지 되었을때
}
