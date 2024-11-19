using UnityEngine;

public class CubeItem : MonoBehaviour
{
    [SerializeField] private CubeType targetCubeType;
    [SerializeField] private IMapBlockLogic logic;
    public MapObjType mapObjType;

    public void StartMapObj()
    {
        StartCoroutine(logic.MapLogicCoroutine());
    }
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
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollected(BaseCube cube)
    {
        // 확장 시 자식 클래스에서 이 메서드를 오버라이드
    }
} 