using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BlockMenu : BasePopupUI
{

    public void SaveMap()
    {
        string baseName = "SaveMap";
        string directoryPath = "Assets/Resources/Prefabs/Map/MapSave/";

        int index = 1;
        string prefabPath;

        do
        {
            prefabPath = $"{directoryPath}{baseName}{index}.prefab";
            index++;
        } while (File.Exists(prefabPath)); // 해당 파일이 존재하면 숫자 증가

        // Prefab 저장
        PrefabUtility.SaveAsPrefabAsset(MapManager.Instance.MapObject, prefabPath);
        Debug.Log($"Prefab saved at: {prefabPath}");
    }
}
