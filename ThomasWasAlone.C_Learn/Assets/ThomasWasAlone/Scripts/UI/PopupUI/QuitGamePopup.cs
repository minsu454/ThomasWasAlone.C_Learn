using UnityEngine;


public class QuitGamePopup : BasePopupUI
{
    [SerializeField] private AudioClip _btnClip;
    
    
    /// <summary>
    /// 퍼즈 상태에서 게임으로 돌아가는 기능
    /// </summary>
    public void OnClickContinueGame() 
    {
        Managers.Sound.SFX2DPlay(_btnClip);
        Close();
    }
    
    
    /// <summary>
    /// 타이틀로 돌아가는 기능
    /// </summary>
    public void OnClickQuitGame()
    {
        Managers.Sound.SFX2DPlay(_btnClip);
        Application.Quit();
    }
}