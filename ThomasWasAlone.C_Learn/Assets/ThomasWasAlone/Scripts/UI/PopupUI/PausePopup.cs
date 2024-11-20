using Common.SceneEx;
using UnityEngine;


public class PausePopup : BasePopupUI
{
    public void OnClickContinueGame()
    {
        Time.timeScale = 1;
        Close();
    }

    public void OnClickSettings()
    {
        Managers.UI.CreatePopup<SettingsPopup>();
    }
    
    
    public void OnClickQuitGame()
    {
        Time.timeScale = 1;
        Close();
        SceneManagerEx.LoadScene(SceneType.Title);
    }
}