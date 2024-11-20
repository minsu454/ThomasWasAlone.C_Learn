using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class UIScrollViewButton : MonoBehaviour
{
    private object key;                                 //키
    [SerializeField] private TextMeshProUGUI text1;     //메인 텍스트
    [SerializeField] private TextMeshProUGUI text2;     //서브 텍스트
    public event Action<object> OnClickEvent;           //클릭시 이벤트
        
    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Init(object key)
    {
        this.key = key;
        text1.text = key.ToString();
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Init(object key, string str)
    {
        this.key = key;

        text1.text = key.ToString();
        text2.text = str;
    }

    /// <summary>
    /// 버튼 입력시 이벤트
    /// </summary>
    public void OnButton()
    {
        OnClickEvent?.Invoke(key);
    }
}
