using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private CubeManager cubeManager;

    private void Awake()
    {
        CreateMap();
    }

    private void CreateMap()
    {
        GameObject prefab = Managers.Map.ReturnData(Managers.Data.MapName);
        GameObject mapGo = Instantiate(prefab);

        MapData data  = mapGo.GetComponent<Map>().mapData;

        cubeManager.Init(data.startDic);
    }
}
