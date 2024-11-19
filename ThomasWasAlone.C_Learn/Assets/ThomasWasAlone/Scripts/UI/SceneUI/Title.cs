using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : BaseSceneUI
{
    public override void Init()
    {
        base.Init();
    }


    public void OnClickSelectMapButton()
    {
        Managers.UI.CreatePopup<SelectMapPopup>();
    }
    
    
    public void OnClickSettingsButton()
    {
        Managers.UI.CreatePopup<SettingsPopup>();
    }
    
    
    public void OnClickQuitGameButton()
    {
        Managers.UI.CreatePopup<QuitGamePopup>();
    }
}