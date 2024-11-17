using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour
{
    private Button button;
    private string resourcesBlockName;
    private Dictionary<string, GameObject> cachedObjects = new Dictionary<string, GameObject>();

    public void Initialize(string blockName)
    {
        resourcesBlockName = blockName;

        // 버튼 추가 및 이벤트 설정
        button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        MapManager.Instance.Input.objectToSpawn = ClickGameObject();
    }
   
    public GameObject ClickGameObject()
    {
        if (!cachedObjects.TryGetValue(resourcesBlockName, out GameObject blockObj))
        {
            blockObj = Resources.Load<GameObject>($"Prefabs/Map/MapBlock/{resourcesBlockName}");
            if (blockObj != null)
            {
                cachedObjects[resourcesBlockName] = blockObj; // 캐싱
            }
            else
            {
                return null;
            }
        }
        return blockObj;
    }
}
