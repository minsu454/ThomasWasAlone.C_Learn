using Common.SceneEx;
using UnityEngine;


public class PausePopup : BasePopupUI
{
    [SerializeField] private AudioClip _btnClip;
    public void OnClickContinueGame()
    {
        Time.timeScale = 1;
        Close();
        Managers.Sound.SFX2DPlay(_btnClip);

    }

    public void OnClickSettings()
    {
        Managers.UI.CreatePopup<SettingsPopup>();
        Managers.Sound.SFX2DPlay(_btnClip);

    }
    
    
    public void OnClickQuitGame()
    {
        Time.timeScale = 1;
        Close();
        SceneManagerEx.LoadScene(SceneType.Title);
        Managers.Sound.SFX2DPlay(_btnClip);

    }
}