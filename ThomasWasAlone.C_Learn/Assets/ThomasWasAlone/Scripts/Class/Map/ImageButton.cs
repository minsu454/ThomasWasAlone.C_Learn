using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = gameObject.AddComponent<Button>();
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClick()); // 버튼 클릭 시 이벤트 처리

    }
    public void OnClick()
    {
        Debug.Log(gameObject.GetComponent<RawImage>().texture.name);
    }
}
