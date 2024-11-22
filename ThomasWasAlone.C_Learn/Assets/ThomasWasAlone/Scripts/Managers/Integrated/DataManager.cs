using Common.Event;
using UnityEngine;

public sealed class DataManager
{
    private string mapName = "SaveMap1";    //맵이름
    public string MapName { get { return mapName; } }

    public StageSO stageSO { get; private set; }

    public void Init()
    {
        EventManager.Subscribe(GameEventType.StageChoice, StageChoiceCompleted);
        stageSO = Resources.Load<StageSO>("Stage/StageSO");
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

    /// <summary>
    /// 현재 스테이지 저장해주는 함수
    /// </summary>
    public void SaveCurrentStage()
    {
        string currentStage = mapName;
        PlayerPrefs.SetInt(currentStage, 1);
    }

    /// <summary>
    /// 클리어데이터 반환해주는 함수
    /// </summary>
    public bool GetClearData(string stagename)
    {
        return PlayerPrefs.HasKey(stagename);
    }

    /// <summary>
    /// 모든 저장 데이터 지워주는 함수
    /// </summary>
    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }
}