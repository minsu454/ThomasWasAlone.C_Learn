using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopupUI : MonoBehaviour
{
    public virtual void Init<T>(T option) where T : PopupOption
    {
    }

    public virtual void Close()
    {
        Managers.UI.ClosePopup();
    }
}
