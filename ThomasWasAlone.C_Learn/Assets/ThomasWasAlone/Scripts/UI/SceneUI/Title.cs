using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : BaseSceneUI
{
    public void OnClickPauseButton()
    {
        Managers.UI.CreatePopup<PausePopup>();
    }
}