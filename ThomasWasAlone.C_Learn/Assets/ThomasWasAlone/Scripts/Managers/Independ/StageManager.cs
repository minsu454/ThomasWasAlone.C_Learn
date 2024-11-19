using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerObj = new List<GameObject>();

    private void Awake()
    {
        CreateMap();
    }

    private void CreateMap()
    {
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/Map/SaveMap/SaveMap/{Managers.Data.MapName}");
        GameObject mapGo = Instantiate(prefab);

        MapData mapData = mapGo.GetComponent<Map>().mapData;

        foreach (var spawnData in mapData.startDic)
        {
            GameObject cubePrefab = Managers.Cube.ReturnCube(spawnData.Type);
            GameObject cubeGo = Instantiate(cubePrefab, spawnData.Pos, Quaternion.identity);
        }
    }
}
