using Common.SceneEx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopup : BasePopupUI
{
    public void OnClickQuitGame()
    {
        SceneManagerEx.LoadScene(SceneType.Title);
    }
}