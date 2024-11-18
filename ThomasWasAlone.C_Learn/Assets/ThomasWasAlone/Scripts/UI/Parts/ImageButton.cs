using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private string resourcesBlockName;
    public GameObject resourcesBlock;
    public void Initialize(string blockName, Texture2D texture , GameObject[] Objs)
    {
        resourcesBlockName = blockName;
        // RawImage 추가 및 설정
        RawImage rawImage = gameObject.AddComponent<RawImage>();
        rawImage.texture = texture;

        // RectTransform 크기 설정
        RectTransform rectTransform = rawImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(128, 128);

        // 버튼 추가 및 이벤트 설정
        button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(() => OnClick(Objs));
    }

    public void OnClick(GameObject[] objs)
    {
        MapManager.Instance.Input.objectToSpawn = ClickGameObject(objs);
    }

    public GameObject ClickGameObject(GameObject[] objs)
    {
        foreach (GameObject obj in objs)
        {
            if (obj.name == resourcesBlockName)
            {
                return obj; // 일치하는 오브젝트 반환
            }
        }
        return null;
    }
}
