using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapInput : MonoBehaviour
{
    public GameObject DefaultObj;    
    public GameObject objectToSpawn; // 생성할 오브젝트
    public float spawnDistance = 1f; // 생성 거리
    private GameObject startBlock;   // 시작 블록
    private GameObject endBlock;     // 끝 블록
    private bool isStartSelected = false; // 시작 블록이 선택되었는지 여부
    Vector3 startpos;
    [SerializeField] private LayerMask layMask;
    private void Start()
    {
        objectToSpawn = DefaultObj;
    }
    private GameObject CreateBlock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layMask) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 spawnPosition = hit.collider.bounds.center + hit.normal * spawnDistance;

            GameObject block = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            block.transform.SetParent(MapManager.Instance.MapObject.transform);
            block.name = objectToSpawn.name;
            return block;
        }
        return null;
    }

    public void OnCreate()
    {
        switch (objectToSpawn.name)
        {
            case "MovingPlatform":
                MovingPlatformIns();
                break;
            case "Tower":
                TowerIns();
                break;
            case "BigCube":
                ChracterStartEndIns(CubeType.BigCube);
                break;
            case "LightCube":
                ChracterStartEndIns(CubeType.LightCube);
                break;
            case "SmallCube":
                ChracterStartEndIns(CubeType.SmallCube);
                break;
            case "JumpBoostCube":
                ChracterStartEndIns(CubeType.JumpBoostCube);
                break;
            case "MapItem":// 맵 아이템
                MapItemIns();
                break;
            default:
                // 일반적인 오브젝트 생성
                MapManager.Instance.map.groundObjs.Add(CreateBlock());
                break;
        }
    }
    public void OnDelete()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject obj = hit.collider.gameObject;
            if (MapManager.Instance.map.groundObjs.Contains(obj))
            {
                MapManager.Instance.map.groundObjs.Remove(obj);
            }
            Destroy(obj);
        }
    }
    //public void BlockIns()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out RaycastHit hit))
    //    {
    //        Vector3 spawnPosition = hit.collider.bounds.center + hit.normal * spawnDistance;

    //        GameObject Ins = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    //        Ins.transform.SetParent(MapManager.Instance.MapObject.transform);
    //    }
    //}
    public void MovingPlatformIns()
    {
        if (!isStartSelected)
        {
            startBlock = CreateBlock();
            if (startBlock == null)
            {
                Debug.Log("block없음");
                return;
            }
            SetTransparency(startBlock, 0.7f, Color.yellow);
            isStartSelected = true;
        }
        else
        {
            endBlock = CreateBlock();

            if (endBlock == null)
            {
                Debug.LogWarning("block없음");
                return;
            }
            SetTransparency(endBlock, 0.7f, Color.yellow);
            // 무빙 플랫폼 생성
            GameObject platform = Instantiate(objectToSpawn, Vector3.zero, Quaternion.identity);
            platform.transform.SetParent(MapManager.Instance.MapObject.transform);

            // MovingPlatform 컴포넌트 추가
            MovingPlatform movingPlatformComponent = platform.AddComponent<MovingPlatform>();

            // Start와 End에 해당 블록을 설정
            movingPlatformComponent.SetStartAndEnd(startBlock.transform.position, endBlock.transform.position);

            // 선택 초기화
            isStartSelected = false;
            objectToSpawn = DefaultObj;
        }

    }
    public void ChracterStartEndIns(CubeType type)
    {
        if (!isStartSelected)
        {
            startBlock = CreateBlock();
            if (startBlock == null)
            {
                Debug.Log("block없음");
                return;
            }
            MapManager.Instance.map.mapData.startDic.Add(new SpawnData(type, startBlock.transform.position));
            startpos = startBlock.transform.position;
            SetTransparency(startBlock, 0.01f, Color.red);
            isStartSelected = true;
        }
        else
        {
            endBlock = CreateBlock();
            if (endBlock == null)
            {
                Debug.LogWarning("block없음");
                return;
            }
            MapManager.Instance.map.mapData.endDic.Add(new SpawnData(type, endBlock.transform.position));
            SetTransparency(endBlock, 0.01f, Color.green);
            //////////////////////////////////////////////////////////////////
            // 플레이어 생성
            //foreach (GameObject obj in MapManager.Instance.map.playerObj)
            //{
            //    if(obj.name == objectToSpawn.name)
            //    {
            //        objectToSpawn = obj;
            //        GameObject Character = Instantiate(objectToSpawn, startpos, Quaternion.identity);
            //        Character.transform.SetParent(MapManager.Instance.MapObject.transform);
            //        point.playerObj = Character;
            //    }
            //}
            /////////////////////////////////////////////////////////////////
            // 선택 초기화
            isStartSelected = false;
            objectToSpawn = DefaultObj;
        }
    }
    public void TowerIns()
    {
        GameObject towerObj = CreateBlock();
        towerObj.AddComponent<Tower>();
        towerObj.GetComponent<Tower>().startPos = transform.position;
        objectToSpawn = DefaultObj;
    }
    public void MapItemIns()
    {
        GameObject itemBlock = CreateBlock();
        objectToSpawn = DefaultObj;
    }
    private void SetTransparency(GameObject obj, float alpha, Color changecolor)
    {
        if (obj == null)
        {
            Debug.Log("게임 오브젝트 없음.");
            return;
        }
        MeshRenderer[] meshRenderers = obj.GetComponentsInChildren<MeshRenderer>();
        // MeshRenderer가 없으면 경고 메시지를 출력하고 함수를 종료
        if (meshRenderers.Length == 0 || meshRenderers.Length == 0)
        {
            Debug.Log("No MeshRenderer found in the GameObject or its children.");
            return;
        }
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            // URP 쉐이더에서 투명 모드를 활성화
            Material material = meshRenderer.material;

            material.SetFloat("_Surface", 1);  // 'Opaque' -> 'Transparent'로 설정
            material.SetFloat("_Blend", 0);    // AlphaBlend 모드로 설정

            Color color = changecolor;//색깔 변경
            color.a = Mathf.Clamp(alpha, 0f, 1f);  // 알파값이 0에서 1 사이로 제한
            material.color = color;
        }
    
    }

}