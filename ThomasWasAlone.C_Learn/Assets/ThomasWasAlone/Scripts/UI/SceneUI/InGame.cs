using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGame : BaseSceneUI
{
    public void OnClickPauseButton()
    {
        Managers.UI.CreatePopup<PausePopup>();
    }
}