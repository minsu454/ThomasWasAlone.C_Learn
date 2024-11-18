using System;
using UnityEngine;
using UnityEngine.UI;

public class MapMenuImageLoader : MonoBehaviour
{
    public Transform parentObject; // 부모 오브젝트
    private Button button;
    private string resourcesBlockName;  //mapMenuImageLoader에 아래 기능 다 때려박기
    private BlockMenu blockMenu;
    [SerializeField] public Texture2D[] textures;
    [SerializeField] public GameObject[] resourcesBlocks;

    private void Start()
    {
        blockMenu = GetComponent<BlockMenu>();
        LoadAndDisplayImages();
    }
    private void LoadAndDisplayImages()
    {
        // textures 배열의 각 텍스처에 대해 이미지와 버튼을 생성
        foreach (var texture in textures)
        {
            // 텍스처를 기반으로 이미지 오브젝트를 생성
            GameObject imageObject = CreateImageObject(texture);

            // 생성된 이미지에 버튼 추가 및 클릭 이벤트 설정
            Button button = imageObject.AddComponent<Button>();
            button.onClick.AddListener(() => OnClick(texture.name));
        }

        // 필요 없는 리소스를 메모리에서 해제하여 최적화
        Resources.UnloadUnusedAssets();
    }

    private GameObject CreateImageObject(Texture2D texture)
    {
        // 새로운 게임 오브젝트 생성 (텍스처 이름을 포함한 이름 설정)
        GameObject imageObject = new GameObject($"MapImage_{texture.name}");
        imageObject.transform.SetParent(parentObject, false); // 부모 오브젝트에 추가

        // RawImage 컴포넌트를 추가하고 텍스처를 설정
        RawImage rawImage = imageObject.AddComponent<RawImage>();
        rawImage.texture = texture;

        // RectTransform 컴포넌트를 사용해 이미지 크기 설정
        RectTransform rectTransform = rawImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(128, 128); // 이미지 크기를 128x128로 설정

        return imageObject;
    }

    private void OnClick(string blockName)
    {
        // 클릭된 텍스처 이름에 해당하는 블록 오브젝트를 검색
        GameObject selectedBlock = FindBlockByName(blockName);

        if (selectedBlock == null)
        {
            // 블록을 찾지 못했을 경우 경고 메시지 출력
            Debug.LogWarning($"블록 이름 '{blockName}'에 해당하는 오브젝트를 찾을 수 없습니다.");
            return;
        }

        // MapManager를 통해 선택된 오브젝트를 지정
        MapManager.Instance.Input.objectToSpawn = selectedBlock;
    }

    private GameObject FindBlockByName(string blockName)
    {
        // resourcesBlocks 배열에서 이름이 일치하는 블록을 검색
        foreach (GameObject block in resourcesBlocks)
        {
            if (block.name.Equals(blockName, StringComparison.OrdinalIgnoreCase)) // 대소문자 구분 없이 비교
            {
                return block;
            }
        }

        // 일치하는 블록이 없을 경우 null 반환
        return null;
    }
}