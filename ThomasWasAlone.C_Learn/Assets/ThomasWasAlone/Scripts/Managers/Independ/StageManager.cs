using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.ProBuilder.Shapes;

public class StageManager : MonoBehaviour
{
    [SerializeField] private CubeManager cubeManager;

    private EndCube[] endCubeArr;

    private void Awake()
    {
        CreateMap();
    }

    private void CreateMap()
    {
        GameObject prefab = Managers.Map.ReturnData(Managers.Data.MapName);
        GameObject mapGo = Instantiate(prefab);

        MapData data  = mapGo.GetComponent<Map>().mapData;

        cubeManager.Init(data.startList);
        CreateEndCube(data.endList);

        CreateDeadzone(data.mapSize);
    }

    private void CreateEndCube(List<SpawnData> data)
    {
        endCubeArr = new EndCube[data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            GameObject cubePrefab = Managers.Cube.ReturnEndCube(data[i].Type);
            GameObject cubeGo = Instantiate(cubePrefab, data[i].Pos, Quaternion.identity);

            endCubeArr[i] = cubeGo.GetComponent<EndCube>();
        }
    }

    private void CreateDeadzone(Vector2 mapsize)
    {
        GameObject deadzone = new GameObject("Deadzone");
        deadzone.AddComponent<Deadzone>();

        BoxCollider col = deadzone.AddComponent<BoxCollider>();

        col.isTrigger = true;
        col.size = new Vector3(mapsize.x * 5f, 1, mapsize.y * 5f);

        deadzone.transform.position += new Vector3(mapsize.x / 2, -8, mapsize.y  / 2);
    }
}
