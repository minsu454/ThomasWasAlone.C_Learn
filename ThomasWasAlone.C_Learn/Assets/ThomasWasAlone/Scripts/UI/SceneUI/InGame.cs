using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGame : BaseSceneUI
{
    public override void Init()
    {
        base.Init();
    }
    
    public void OnClickPauseButton()
    {
        PausePopup pausePopup = FindObjectOfType<PausePopup>();
        if(pausePopup != null) return;
        
        Managers.UI.CreatePopup<PausePopup>();
    }
}