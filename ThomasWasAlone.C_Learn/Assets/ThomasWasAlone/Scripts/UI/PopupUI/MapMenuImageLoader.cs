using System;
using UnityEngine;
using UnityEngine.UI;

public class MapMenuImageLoader : MonoBehaviour
{
    public Transform parentObject; // 부모 오브젝트
    [SerializeField]public Texture2D[] textures;
    private void Start()
    {
        textures = Resources.LoadAll<Texture2D>("Prefabs/Map/MapBlockImage");
        LoadAndDisplayImages();
    }
    private void LoadAndDisplayImages()
    {
        foreach (var texture in textures)
        {
            // 새로운 게임 오브젝트 생성
            GameObject imageObject = new GameObject("MapImage");
            imageObject.transform.SetParent(parentObject, false);

            // RawImage 추가 및 설정
            RawImage rawImage = imageObject.AddComponent<RawImage>();
            rawImage.texture = texture;

            // RectTransform 크기 설정
            RectTransform rectTransform = rawImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(128, 128);

            // ImageButton 추가 및 초기화
            ImageButton imageButton = imageObject.AddComponent<ImageButton>();
            imageButton.Initialize(texture.name);
        }

        // 필요 없는 리소스 해제
        Resources.UnloadUnusedAssets();
    }
}