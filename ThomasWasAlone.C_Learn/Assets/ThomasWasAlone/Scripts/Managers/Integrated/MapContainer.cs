using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapContainer
{
    private readonly Dictionary<string, GameObject> containerDic = new Dictionary<string, GameObject>();

    public void Init()
    {
        GameObject[] prefabArr = Resources.LoadAll<GameObject>($"Prefabs/Map/SaveMap/SaveMap");

        foreach (GameObject go in prefabArr)
        {
            containerDic[go.name] = go;
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

    public string[] KeyToArrayAll()
    {
        return containerDic.Keys.ToArray();
    }
}