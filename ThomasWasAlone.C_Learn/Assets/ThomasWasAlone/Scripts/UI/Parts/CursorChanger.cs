using UnityEngine;
using UnityEngine.UI;


public class CursorChanger : MonoBehaviour
{
    private CameraController _cameraController;
    
    [SerializeField] private RectTransform cubeImages;
    [SerializeField] private Image cursor;
    private const int MaxCursorIndex = 4;
    private int _currentCursorIndex;
    
    private float _eachSize => cubeImages.rect.width / MaxCursorIndex;
    private float pivotOffset = -25f;
    
    
    private void Start()
    {
        UpdateCursorPosition();
    }

    
    private void ChangeCursor() // 이벤트 등록
    {
        _currentCursorIndex = (_currentCursorIndex + 1) % MaxCursorIndex;
        
        UpdateCursorPosition();
    }

    
    private void UpdateCursorPosition()
    {
        float cursorXPosition = -cubeImages.rect.width / 2 + _eachSize * (_currentCursorIndex + 0.5f) + pivotOffset;
        
        cursor.rectTransform.anchoredPosition = new Vector2(cursorXPosition, cursor.rectTransform.anchoredPosition.y);
    }
}