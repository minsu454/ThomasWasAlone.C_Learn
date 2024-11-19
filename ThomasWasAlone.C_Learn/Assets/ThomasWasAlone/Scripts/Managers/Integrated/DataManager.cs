using Common.Event;

public sealed class DataManager
{
    private string mapName = "SaveMap8";    //맵이름
    public string MapName { get { return mapName; } }

    public void Init()
    {
        EventManager.Subscribe(GameEventType.StageChoice, StageChoiceCompleted);
    }

    /// <summary>
    /// 스테이지 고를 때 맵이름 저장해주는 함수
    /// </summary>
    public void StageChoiceCompleted(object args)
    {
        mapName = (string)args;
    }

    public void Delete()
    {
        EventManager.Subscribe(GameEventType.StageChoice, StageChoiceCompleted);
    }
}