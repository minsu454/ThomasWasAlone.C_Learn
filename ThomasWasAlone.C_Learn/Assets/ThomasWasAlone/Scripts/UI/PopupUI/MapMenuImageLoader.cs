using System;
using UnityEngine;
using UnityEngine.UI;

public class MapMenuImageLoader : MonoBehaviour
{
    public Transform parentObject; // 부모 오브젝트
    [SerializeField]public Texture2D[] textures;
    private void Start()
    {
        textures = Resources.LoadAll<Texture2D>("Prefabs/Map/MapImage");
        LoadAndDisplayImages();
    }
    private void LoadAndDisplayImages()
    {
        foreach (var texture in textures)
        {
            // 부모에 RawImage 추가
            GameObject imageObject = new GameObject("MapImage");
            imageObject.transform.SetParent(parentObject);
            imageObject.AddComponent<ImageButton>();
            RawImage rawImage = imageObject.AddComponent<RawImage>(); // RawImage 컴포넌트 추가
            rawImage.texture = texture;

            // 크기 설정
            RectTransform rectTransform = rawImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(128, 128);

        }

        // 필요 없는 리소스 해제
        Resources.UnloadUnusedAssets();
    }

}