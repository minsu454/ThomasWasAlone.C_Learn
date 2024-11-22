using Common.SceneEx;
using UnityEngine;


public class PausePopup : BasePopupUI
{
    [SerializeField] private AudioClip _btnClip;
    
    
    /// <summary>
    /// 게임으로 복귀
    /// </summary>
    public void OnClickContinueGame()
    {
        Time.timeScale = 1;
        Close();
        Managers.Sound.SFX2DPlay(_btnClip);

    }
    
    

    /// <summary>
    /// 세팅 팝업 Create
    /// </summary>
    public void OnClickSettings()
    {
        Managers.UI.CreatePopup<SettingsPopup>();
        Managers.Sound.SFX2DPlay(_btnClip);
    }
    
    
    /// <summary>
    /// 타이틀로 복귀
    /// </summary>
    public void OnClickQuitGame()
    {
        Time.timeScale = 1;
        Close();
        SceneManagerEx.LoadScene(SceneType.Title);
        Managers.Sound.SFX2DPlay(_btnClip);
    }
}