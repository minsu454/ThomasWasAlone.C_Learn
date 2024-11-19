using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopup : BasePopupUI
{
    [SerializeField] private ChangeVolume changeVolume;

    
    private void Awake()
    {
        changeVolume.Init();
    }
}
