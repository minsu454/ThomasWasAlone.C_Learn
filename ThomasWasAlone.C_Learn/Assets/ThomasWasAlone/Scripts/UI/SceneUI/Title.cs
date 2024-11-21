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


    public void OnClickSelectMapButton()
    {
        Managers.UI.CreatePopup<SelectMapPopup>();
        Managers.Sound.SFX2DPlay(_btnClip);
    }
    
    
    public void OnClickSettingsButton()
    {
        Managers.UI.CreatePopup<SettingsPopup>();
        Managers.Sound.SFX2DPlay(_btnClip);
    }
    
    
    public void OnClickQuitGameButton()
    {
        Managers.UI.CreatePopup<QuitGamePopup>();
        Managers.Sound.SFX2DPlay(_btnClip);
    }
}