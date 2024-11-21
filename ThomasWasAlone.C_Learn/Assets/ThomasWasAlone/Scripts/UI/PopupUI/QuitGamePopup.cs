using UnityEngine;


public class QuitGamePopup : BasePopupUI
{
    [SerializeField] private AudioClip _btnClip;
    
    public void OnClickContinueGame()
    {
        Managers.Sound.SFX2DPlay(_btnClip);
        Close();
    }
    
    
    public void OnClickQuitGame()
    {
        Managers.Sound.SFX2DPlay(_btnClip);
        Application.Quit();
    }
}