using System;
using UnityEngine;
using UnityEngine.UI;

public class MapMenuImageLoader : MonoBehaviour
{
    public Transform parentObject; // 부모 오브젝트
    [SerializeField] public Texture2D[] textures;
    [SerializeField] public GameObject[] resourcesObj;
    private void Start()
    {
        LoadAndDisplayImages();
    }
    // 오로지 생성만
    private void LoadAndDisplayImages()
    {
        GameObject resObj = MapManager.Instance.Input.DefaultObj; 
        foreach (var texture in textures)
        {
            // 새로운 게임 오브젝트 생성
            GameObject imageObject = new GameObject("MapImage");
            imageObject.transform.SetParent(parentObject, false);

            foreach (GameObject obj in resourcesObj)
            {
                if (obj.name == texture.name)
                {
                    resObj = obj; break;
                }
            }
            // ImageButton 추가 및 초기화
            ImageButton imageButton = imageObject.AddComponent<ImageButton>();
            imageButton.Initialize(texture.name, texture, resObj);
        }

        // 필요 없는 리소스 해제
        Resources.UnloadUnusedAssets();
    }
}