using Common.SceneEx;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class UIManager : MonoBehaviour, IInit
{
    private readonly List<BasePopupUI> showList = new List<BasePopupUI>();

    public void Init()
    {
        SceneManagerEx.OnLoadCompleted(OnSceneLoaded);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Clear();
        CreateSceneUI(scene.name);
    }

    private void CreateSceneUI(string name)
    {
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/Main/{name}");

        if (prefab == null)
        {
            Debug.LogWarning($"{name} UI is None.");
            return;
        }

        GameObject clone = Instantiate(prefab);

        if (!clone.TryGetComponent(out BaseSceneUI sceneUI))
        {
            Debug.LogError($"GameObject Is Not BaseSceneUI Inheritance : {prefab}");
            return;
        }

        sceneUI.Init();
    }

    public void CreatePopup<T>(PopupOption option = null) where T : BasePopupUI
    {
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/Popup/{typeof(T).Name}");

        if (prefab == null)
        {
            Debug.LogWarning($"{name} Popup is None.");
            return;
        }

        GameObject clone = Instantiate(prefab);

        if (!clone.TryGetComponent(out T popupUI))
        {
            Debug.LogError($"GameObject Is Not BaseSceneUI Inheritance : {prefab}");
            return;
        }

        showList.Add(popupUI);
        popupUI.Init(option);
    }

    public void ClosePopup()
    {
        if (showList.Count == 0)
            return;

        BasePopupUI popup = showList[showList.Count - 1];
        showList.RemoveAt(showList.Count - 1);

        Destroy(popup.gameObject);
    }

    public void ClosePopup<T>(T popup) where T : BasePopupUI
    {
        int idx = showList.Count - 1;

        while (idx == 0)
        {
            if (showList[idx] == popup)
                break;

            idx--;
        }

        if (idx == -1)
            Debug.LogError($"Is Not Found Popup : {typeof(T).Name}");

        showList.RemoveAt(idx);

        Destroy(popup.gameObject);
    }

    private void Clear()
    {
        showList.Clear();
    }
}
