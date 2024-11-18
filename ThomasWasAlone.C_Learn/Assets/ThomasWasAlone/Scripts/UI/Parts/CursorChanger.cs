using System;
using UnityEngine;
using UnityEngine.UI;


public class CursorChanger : MonoBehaviour
{
    [SerializeField] private RectTransform cubeImages;
    [SerializeField] private Image cursor;
    
    private int _maxCubeQuantity;
    private int _currentCursorIndex = 1;

    private float _eachSize;
    
    
    public void Init(int maxCubeQuantity = 4)
    {  
        _maxCubeQuantity = maxCubeQuantity;
        _eachSize = cubeImages.rect.width / 4;
        UpdateCursorPosition();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangePreviousIndex();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ChangeNextIndex();
        }
    }
    
    
    public void ChangePreviousIndex()
    {
        if(_currentCursorIndex == 1) return;
        
        _currentCursorIndex--;

        ChangeCursor();
    }


    public void ChangeNextIndex()
    {
        if(_currentCursorIndex >= _maxCubeQuantity) return;
        
        _currentCursorIndex++;

        UpdateCursorPosition();
    }


    private void ChangeCursor()
    {
        _currentCursorIndex %= _maxCubeQuantity;
        
        UpdateCursorPosition();
    }

    
    private void UpdateCursorPosition()
    {
        float cursorXPosition = -cubeImages.rect.width + _eachSize * _currentCursorIndex;
        
        cursor.rectTransform.anchoredPosition = new Vector2(cursorXPosition, cursor.rectTransform.anchoredPosition.y);
    }
}