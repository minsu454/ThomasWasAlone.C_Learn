using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : BaseSceneUI
{
    [SerializeField] private AudioClip _btnClip;
    public override void Init()
    {
        base.Init();
    }


    /// <summary>
    /// SelectMapPopup UI 생성
    /// </summary>
    public void OnClickSelectMapButton()
    {
        Managers.UI.CreatePopup<SelectMapPopup>();
        Managers.Sound.SFX2DPlay(_btnClip);
    }
    
    
    /// <summary>
    /// SettingsPopup UI 생성
    /// </summary>
    public void OnClickSettingsButton()
    {
        Managers.UI.CreatePopup<SettingsPopup>();
        Managers.Sound.SFX2DPlay(_btnClip);
    }
    
    
    /// <summary>
    /// QuitGamePopup UI 생성
    /// </summary>
    public void OnClickQuitGameButton()
    {
        Managers.UI.CreatePopup<QuitGamePopup>();
        Managers.Sound.SFX2DPlay(_btnClip);
    }
}