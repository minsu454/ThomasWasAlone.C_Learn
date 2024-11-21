using Common.EnumExtensions;
using Common.Event;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;


public class CursorChanger : MonoBehaviour
{
    [SerializeField] private GameObject cubeImagesParent;
    [SerializeField] private Image cursor;
    
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    
    private int _maxCubeQuantity;
    private int _currentCursorIndex = 0;

    
    public void Init(CubeType[] cubeType)
    {  
        _maxCubeQuantity = cubeType.Length;
        SetCube(cubeType);
        UpdateCursorPosition();

        EventManager.Subscribe(GameEventType.ChangeCube, ChangeNextIndex);
    }

    private void SetCube(CubeType[] cubeType)
    {
        for (int i = 0; i < cubeType.Length; i++)
        {
            string objName = EnumExtensions.EnumToString(cubeType[i]);
            
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/Cube/{objName}");
            
            if (prefab == null)
            {
                Debug.LogWarning($"{objName} UI is None.");
                return;
            }

            GameObject clone = Instantiate(prefab, cubeImagesParent.transform);
            clone.GetComponent<Image>().color = CubeFactory.TypeByColor(cubeType[i]);
        }
    }

    public void ChangeNextIndex(object args)
    {
        _currentCursorIndex = (_currentCursorIndex + 1) % _maxCubeQuantity;

        UpdateCursorPosition();
    }

    
    private void UpdateCursorPosition()
    {
        float cursorXPosition = (_currentCursorIndex - _maxCubeQuantity + 1) * (gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x); // 현재 spacing은 0

        cursor.rectTransform.anchoredPosition = new Vector2(cursorXPosition, cursor.rectTransform.anchoredPosition.y);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(GameEventType.ChangeCube, ChangeNextIndex);
    }
}