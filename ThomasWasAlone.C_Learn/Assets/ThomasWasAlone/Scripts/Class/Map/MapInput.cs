using UnityEngine;

public class MapInput : MonoBehaviour
{
    public GameObject DefaultObj;    
    public GameObject objectToSpawn; // 생성할 오브젝트
    public float spawnDistance = 1f; // 생성 거리
    private GameObject startBlock;   // 시작 블록
    private GameObject endBlock;     // 끝 블록
    private bool isStartSelected = false; // 시작 블록이 선택되었는지 여부
    [SerializeField] private LayerMask layMask;
    private void Start()
    {
        objectToSpawn = DefaultObj;
    }
    public void OnCreate()
    {
        switch (objectToSpawn.name)
        {
            case "MovingPlatform":
                MovingPlatformIns();
                break;

            default:
                // 일반적인 오브젝트 생성
                CreateBlock("Nomal");
                break;
        }
    }
    public void OnDelete()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Destroy(hit.collider.gameObject);
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
            startBlock = CreateBlock("Start");
            if (startBlock == null)
            {
                Debug.Log("block없음");
                return;
            }
            SetTransparency(startBlock, 0.7f);
            isStartSelected = true;
        }
        else
        {
            endBlock = CreateBlock("End");
            if (endBlock == null)
            {
                Debug.LogWarning("block없음");
                return;
            }

            SetTransparency(endBlock, 0.7f);
            // 무빙 플랫폼 생성
            GameObject platform = Instantiate(objectToSpawn, Vector3.zero, Quaternion.identity);
            platform.transform.SetParent(MapManager.Instance.MapObject.transform);

            // MovingPlatform 컴포넌트 추가
            MovingPlatform movingPlatformComponent = platform.AddComponent<MovingPlatform>();

            // Start와 End에 해당 블록을 설정
            movingPlatformComponent.SetStartAndEnd(startBlock.transform.position, endBlock.transform.position);

            // 선택 초기화
            isStartSelected = false;
        }
    }

    // 블럭을 생성하고 반환하는 메서드 (Start 또는 End)
    private GameObject CreateBlock(string blockType)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layMask))
        {
            Vector3 spawnPosition = hit.collider.bounds.center + hit.normal * spawnDistance;

            GameObject block = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            block.transform.SetParent(MapManager.Instance.MapObject.transform);
            block.name = blockType;  // 블럭 이름을 지정 (Start 또는 End)

            return block;
        }
        return null;
    }
    private void SetTransparency(GameObject obj, float alpha)
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

            // 빨간색으로 설정하고 알파값을 적용
            Color color = Color.red;  // 빨간색
            color.a = Mathf.Clamp(alpha, 0f, 1f);  // 알파값이 0에서 1 사이로 제한
            material.color = color;
        }
    
    }

}