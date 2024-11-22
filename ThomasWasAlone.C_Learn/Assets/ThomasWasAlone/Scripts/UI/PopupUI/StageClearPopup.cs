using Common.Event;
using Common.SceneEx;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageClearPopup : BasePopupUI
{
    [SerializeField] private AudioClip _clearClip;
    [SerializeField] private AudioClip _btnClip;

    public override void Init<T>(T option)
    {
        base.Init(option);

        Managers.Sound.SFX2DPlay(_clearClip);
        Managers.Data.SaveCurrentStage();

        var stageNameArr = Managers.Data.stageSO.stageNameArr;
        string name = Managers.Data.MapName;
        for (int i = 0; i < stageNameArr.Length; i++)
        {
            if (stageNameArr[i] == name && i + 1 != stageNameArr.Length)
            {
                EventManager.Dispatch(GameEventType.StageChoice, stageNameArr[i + 1]);
                break;
            }
        }
    }

    public void OnClickGoToNextStage()
    {
        Managers.Sound.SFX2DPlay(_btnClip);
        SceneManagerEx.LoadScene(SceneType.InGame);
    }

    public void OnClickGoToTitle()
    {
        Managers.Sound.SFX2DPlay(_btnClip);
        SceneManagerEx.LoadScene(SceneType.Title);
    }
}
