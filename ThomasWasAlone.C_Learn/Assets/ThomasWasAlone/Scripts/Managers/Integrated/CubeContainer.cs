using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.EnumExtensions;

public class CubeContainer
{
    private readonly Dictionary<CubeType, GameObject> containerDic = new Dictionary<CubeType, GameObject>();

    public void Init()
    {
        foreach (CubeType type in Enum.GetValues(typeof(CubeType)))
        {
            GameObject go = Resources.Load<GameObject>($"Prefabs/Player/{type.EnumToString()}");

            if (go == null)
            {
                Debug.LogWarning($"Player Prefab is Null : {type}");
                continue;
            }

            containerDic[type] = go;
        }
    }

    public GameObject ReturnCube(CubeType type)
    {
        if (!containerDic.TryGetValue(type, out GameObject go))
        {
            Debug.LogError($"Player Prefab is Null : {type}");
            return null;
        }

        return go;
    }
}
