using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BlockMenu : BasePopupUI
{
    private const string ImageDirectory = "Assets/Resources/Prefabs/Map/MapImage/";
    private const string DirectoryPath = "Assets/Resources/Prefabs/Map/SaveMap/";
    private const int RenderTextureWidth = 128;
    private const int RenderTextureHeight = 128;
    private const int RenderTextureDepth = 24;

    public GameObject blockMenu;
    private Camera renderCamera;

    private RenderTexture renderTexture;
    public void MenuOpen()
    {
        blockMenu.SetActive(!blockMenu.activeSelf);
    }
    private void Start()
    {
        renderCamera = Camera.main;

        renderTexture = new RenderTexture(RenderTextureWidth, RenderTextureHeight, RenderTextureDepth);
    }

    public void SaveMap()
    {
        string baseName = "SaveMap";
        int index = 1;
        string prefabPath;

        do
        {
            prefabPath = $"{DirectoryPath}{baseName}{index}.prefab";
            index++;
        } while (File.Exists(prefabPath));

        // Prefab 저장
        PrefabUtility.SaveAsPrefabAsset(MapManager.Instance.MapObject, prefabPath);
        Debug.Log($"Prefab saved at: {prefabPath}");

        // 맵 이미지를 저장
        SaveMapImage($"{baseName}Image{index}");
    }

    private void SaveMapImage(string mapName)
    {
        string imagePath = $"{ImageDirectory}{mapName}.png";
        // RenderTexture를 설정하고 렌더링
        renderCamera.targetTexture = renderTexture;
        renderCamera.Render();

        // RenderTexture 데이터를 Texture2D로 복사
        RenderTexture.active = renderTexture;
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = null;

        // Texture2D를 PNG 파일로 저장
        File.WriteAllBytes(imagePath, texture.EncodeToPNG());
        Debug.Log($"Map image saved at: {imagePath}");

        // 클린업
        renderCamera.targetTexture = null;
        Destroy(texture);
    }
}
