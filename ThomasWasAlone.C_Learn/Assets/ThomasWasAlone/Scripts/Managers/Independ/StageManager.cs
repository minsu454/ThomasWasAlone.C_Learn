using Common.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.ProBuilder.Shapes;

public class StageManager : MonoBehaviour
{
    [SerializeField] private CubeManager cubeManager;

    private EndCube[] endCubeArr;

    private int curInEndCubeCount = 0;      //현재 end장소에 도착한 큐브 숫자 변수
    private int maxInEndCubeCount = 0;      //stage에 있는 end장소의 총 숫자 변수

    private void Awake()
    {
        CreateMap();

        EventManager.Subscribe(GameEventType.InEndCube, CheckClear);
        EventManager.Subscribe(GameEventType.KillAllCubes, InitCurInEndCubeCount);
    }

    /// <summary>
    /// 클리어 체크 함수
    /// </summary>
    private void CheckClear(object args)
    {
        curInEndCubeCount += (int)args;

        if (curInEndCubeCount == maxInEndCubeCount)
        {
            GameClear();
        }
    }

    /// <summary>
    /// 큐브 죽을 때 호출 함수
    /// </summary>
    private void InitCurInEndCubeCount(object args)
    {
        curInEndCubeCount = 0;
        
        foreach (var cube in endCubeArr)
        {
            cube.ResetMaterial();
        }
    }

    /// <summary>
    /// 게임클리어시 호출 함수
    /// </summary>
    private void GameClear()
    {
        Managers.UI.CreatePopup<StageClearPopup>();
    }

    /// <summary>
    /// 맵 만드는 함수
    /// </summary>
    private void CreateMap()
    {
        GameObject prefab = Managers.Map.ReturnData(Managers.Data.MapName);
        GameObject mapGo = Instantiate(prefab);

        MapData data  = mapGo.GetComponent<Map>().mapData;

        maxInEndCubeCount = data.startList.Count;

        cubeManager.Init(data.startList);
        CreateEndCube(data.endList);

        CreateDeadzone(data.mapSize);
    }

    /// <summary>
    /// 끝지점 큐브 만드는 함수
    /// </summary>
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

    /// <summary>
    /// 바닥 아래있는 데드존 만들어주는 함수
    /// </summary>
    private void CreateDeadzone(Vector2 mapsize)
    {
        GameObject deadzone = new GameObject("Deadzone");
        deadzone.AddComponent<Deadzone>();

        BoxCollider col = deadzone.AddComponent<BoxCollider>();

        col.isTrigger = true;
        col.size = new Vector3(mapsize.x * 5f, 1, mapsize.y * 5f);

        deadzone.transform.position += new Vector3(mapsize.x / 2, -8, mapsize.y  / 2);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(GameEventType.InEndCube, CheckClear);
        EventManager.Unsubscribe(GameEventType.KillAllCubes, InitCurInEndCubeCount);
    }
}
