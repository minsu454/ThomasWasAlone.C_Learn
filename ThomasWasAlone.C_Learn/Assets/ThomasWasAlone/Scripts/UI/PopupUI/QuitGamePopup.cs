using UnityEngine;


public class QuitGamePopup : BasePopupUI
{
    public void OnClickContinueGame()
    {
        Close();
    }
    
    
    public void OnClickQuitGame()
    {
        Application.Quit();
    }
}
