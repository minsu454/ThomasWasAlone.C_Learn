using Common.Event;
using Common.SceneEx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMapPopup : BasePopupUI
{
    [Header("UIScrollView")]
    [SerializeField] private UIScrollView mapScrollView;                    //맵 스크롤뷰

    public override void Init<T>(T option)
    {
        base.Init(option);

        SetScrollView();
    }

    public void SetScrollView()
    {
        string[] mapNameArr = Managers.Map.KeyToArrayAll();

        string[] clearArr = new string[mapNameArr.Length];

        for (int i = 0; i < mapNameArr.Length; i++)
        {
            if (Managers.Data.GetClearData(mapNameArr[i]))
            {
                clearArr[i] = "Clear";
            }
            else
            {
                clearArr[i] = "";
            }

        }

        mapScrollView.CreateItem(mapNameArr, clearArr, CustomClickEvent);
    }

    /// <summary>
    /// 버튼 클릭 이벤트
    /// </summary>
    /// <param name="type"></param>
    public void CustomClickEvent(object type)
    {
        EventManager.Dispatch(GameEventType.StageChoice, type);

        SceneManagerEx.LoadScene(SceneType.InGame);
    }
}
