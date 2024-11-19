using Common.SceneEx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMapPopup : BasePopupUI
{
    public void OnClickSelectMap(int mapNumber)
    {
        SceneManager.LoadScene(mapNumber);
    }
}
