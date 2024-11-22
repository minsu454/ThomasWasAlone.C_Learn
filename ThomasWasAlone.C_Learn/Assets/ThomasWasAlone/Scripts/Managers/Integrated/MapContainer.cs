using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapContainer
{
    private readonly Dictionary<string, GameObject> containerDic = new Dictionary<string, GameObject>();

    public void Init()
    {
        foreach (string name in Managers.Data.stageSO.stageNameArr)
        {
            GameObject go = Resources.Load<GameObject>($"Prefabs/Map/SaveMap/SaveMap/{name}");

            if (go == null)
            {
                Debug.LogWarning($"Player Prefab is Null : {name}");
                continue;
            }

            containerDic[name] = go;
        }
    }

    public GameObject ReturnData(string key)
    {
        if (!containerDic.TryGetValue(key, out GameObject data))
        {
            Debug.LogError($"MapData is Null : {data}");
            return null;
        }

        return data;
    }
}