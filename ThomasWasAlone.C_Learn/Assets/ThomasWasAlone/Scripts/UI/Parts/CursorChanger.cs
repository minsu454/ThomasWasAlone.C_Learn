using Common.EnumExtensions;
using UnityEngine;
using UnityEngine.UI;


public class CursorChanger : MonoBehaviour
{
    [SerializeField] private GameObject cubeImagesParent;
    [SerializeField] private Image cursor;
    
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    
    private int _maxCubeQuantity;
    private int _currentCursorIndex = 1;

    
    public void Init(CubeType[] cubeType)
    {  
        _maxCubeQuantity = cubeType.Length;
        SetCube(cubeType);
        UpdateCursorPosition();
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
        }
    }
    
    
    public void ChangePreviousIndex()
    {
        if(_currentCursorIndex == 1) return;
        
        _currentCursorIndex--;
        
        UpdateCursorPosition();
    }


    public void ChangeNextIndex()
    {
        if(_currentCursorIndex >= _maxCubeQuantity) return;
        
        _currentCursorIndex++;
        
        UpdateCursorPosition();
    }

    
    private void UpdateCursorPosition()
    {
        float cursorXPosition = (_currentCursorIndex - _maxCubeQuantity) * (gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x); // 현재 spacing은 0

        cursor.rectTransform.anchoredPosition = new Vector2(cursorXPosition, cursor.rectTransform.anchoredPosition.y);
    }
}