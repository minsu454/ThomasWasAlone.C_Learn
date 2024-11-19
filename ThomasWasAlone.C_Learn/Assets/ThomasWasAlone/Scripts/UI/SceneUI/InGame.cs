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

        CubeType[] cubeType = new CubeType[2];
        cubeType[0] = CubeType.BigCube;
        cubeType[1] = CubeType.SmallCube;
        
        _cursorChanger.Init(cubeType);
    }

    public void OnClickPauseButton()
    {
        Managers.UI.CreatePopup<PausePopup>();
    }
}