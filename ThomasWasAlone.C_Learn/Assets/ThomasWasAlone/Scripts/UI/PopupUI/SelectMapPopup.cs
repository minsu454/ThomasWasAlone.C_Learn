using Common.Event;
using Common.SceneEx;
using UnityEngine;


public class SelectMapPopup : BasePopupUI
{
    [Header("UIScrollView")]
    [SerializeField] private UIScrollView mapScrollView;                    //맵 스크롤뷰
    
    [SerializeField] private AudioClip _btnClip;

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
        
        Managers.Sound.SFX2DPlay(_btnClip);
    }


    /// <summary>
    /// 해당 팝업 비활성화
    /// </summary>
    public void OnClickReturnButton()
    {
        Managers.Sound.SFX2DPlay(_btnClip);
        Close();
    }
}