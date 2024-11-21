/// <summary>
/// 게임 이벤트 타입(구독하기 위한)
/// </summary>
public enum GameEventType
{
    StageChoice = 0,        //스테이지 골랐을 때
    ChangeCube,             //큐브 전환할 때
    KillAllCubes,           //모든 큐브 죽일 때
    InEndCube,              //클리어 체크할 때
    LockInput,              //인풋 잠금할 때
}
