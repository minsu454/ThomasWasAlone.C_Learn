using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.EnumExtensions;

public class CubeContainer
{
    private readonly Dictionary<CubeType, GameObject> playerContainerDic = new Dictionary<CubeType, GameObject>();
    private readonly Dictionary<CubeType, GameObject> EndContainerDic = new Dictionary<CubeType, GameObject>();

    public void Init()
    {
        CreateDic(playerContainerDic, "Prefabs/Player");
        CreateDic(EndContainerDic, "Prefabs/EndCube");
    }

    private void CreateDic(Dictionary<CubeType, GameObject> dic, string path)
    {
        foreach (CubeType type in Enum.GetValues(typeof(CubeType)))
        {
            GameObject go = Resources.Load<GameObject>($"{path}/{type.EnumToString()}");

            if (go == null)
            {
                Debug.LogWarning($"Player Prefab is Null : {type}");
                continue;
            }

            dic[type] = go;
        }
    }

    public GameObject ReturnPlayer(CubeType type)
    {
        if (!playerContainerDic.TryGetValue(type, out GameObject go))
        {
            Debug.LogError($"Player Prefab is Null : {type}");
            return null;
        }

        return go;
    }

    public GameObject ReturnEndCube(CubeType type)
    {
        if (!EndContainerDic.TryGetValue(type, out GameObject go))
        {
            Debug.LogError($"Player Prefab is Null : {type}");
            return null;
        }

        return go;
    }
}
