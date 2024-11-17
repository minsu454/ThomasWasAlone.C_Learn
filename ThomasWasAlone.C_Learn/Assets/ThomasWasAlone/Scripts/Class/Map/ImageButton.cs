using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour
{
    private Button button;
    private string resourcesBlockName;
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
        Debug.Log(gameObject.GetComponent<RawImage>().texture.name);
    }
    public GameObject ClickGameObject()
    {
        GameObject blockObj = Resources.Load<GameObject>($"Prefabs/Map/MapBlock/{resourcesBlockName}");
        return blockObj;
    }
}
