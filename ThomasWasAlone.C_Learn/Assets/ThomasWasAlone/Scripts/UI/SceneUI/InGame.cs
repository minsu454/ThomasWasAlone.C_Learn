using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGame : BaseSceneUI
{
    [SerializeField] private CursorChanger _cursorChanger;
    
    public override void Init()
    {
        base.Init();
        _cursorChanger.Init();
    }

    public void OnClickPauseButton()
    {
        Managers.UI.CreatePopup<PausePopup>();
    }
}