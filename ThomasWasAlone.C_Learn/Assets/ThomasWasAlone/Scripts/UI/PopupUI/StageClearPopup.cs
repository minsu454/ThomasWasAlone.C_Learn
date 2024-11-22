using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearPopup : BasePopupUI
{
    [SerializeField] private AudioClip _btnClip;

    
    /// <summary>
    /// 게임 클리어 후 다음 스테이지로 돌아가는 기능
    /// </summary>
    public void OnClickGoToNextStage()
    {
        Managers.Sound.SFX2DPlay(_btnClip);
        Close();
    }

    
    /// <summary>
    /// 게임 클리어 후 타이틀로 돌아가는 기능
    /// </summary>
    public void OnClickGoToTitle()
    {
        Managers.Sound.SFX2DPlay(_btnClip);
        Close();
    }
}