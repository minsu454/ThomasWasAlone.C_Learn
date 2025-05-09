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
    [SerializeField] private AudioClip _btnClip;

    
    public override void Init()
    {
        base.Init();

        GameObject mapGo = Managers.Map.ReturnData(Managers.Data.MapName);

        var data = mapGo.GetComponent<Map>().mapData.startList;
        CubeType[] cubeType = new CubeType[data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            cubeType[i] = data[i].Type;
        }

        _cursorChanger.Init(cubeType);
    }

    public void OnClickPauseButton()
    {
        Managers.UI.CreatePopup<PausePopup>();
        Managers.Sound.SFX2DPlay(_btnClip);
    }
}