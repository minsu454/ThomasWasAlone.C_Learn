using UnityEngine;

public class CubeItem : MonoBehaviour
{
    [SerializeField] private CubeType targetCubeType;
    [SerializeField] public GameObject targetinteractionMap;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<BaseCube>(out BaseCube cube)) return;

        bool isCorrectCube = targetCubeType switch
        {
            CubeType.BigCube => cube is BigCube,
            CubeType.SmallCube => cube is SmallCube,
            CubeType.JumpBoostCube => cube is JumpBoostCube,
            CubeType.LightCube => cube is LightCube,
            _ => false
        };
        
        if (isCorrectCube)
        {
            // OnCollected(cube);
            Debug.Log($"{targetCubeType} 아이템 습득");
            OnCollected(cube);
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollected(BaseCube cube)
    {
        // 확장 시 자식 클래스에서 이 메서드를 오버라이드
        StartMapObj();
    }
    public void StartMapObj()
    {
        IMapBlockLogic logicComponent = targetinteractionMap.GetComponent<IMapBlockLogic>();
        logicComponent.StartCoroutine();
    }
} 

// 맵 가장자리 못 넘어가게.
// 움직이는 함정 박스 레이.