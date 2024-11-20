using Common.SceneEx;
using UnityEngine;


public class PausePopup : BasePopupUI
{
    // public void SetTimeScale()
    // {
    //     Time.timeScale = 0;
    // }

    public void OnClickContinueGame()
    {
        Time.timeScale = 1;
        Close();
    }
    
    
    public void OnClickQuitGame()
    {
        Time.timeScale = 1;
        Close();
        SceneManagerEx.LoadScene(SceneType.Title);
    }
}