using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearPopup : BasePopupUI
{
    [SerializeField] private AudioClip _btnClip;

    
    public void OnClickGoToNextStage()
    {
        Managers.Sound.SFX2DPlay(_btnClip);
        Close();
    }

    public void OnClickGoToTitle()
    {
        Managers.Sound.SFX2DPlay(_btnClip);
        Close();
    }
}
