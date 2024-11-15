public class Park : BaseSceneUI
{
    public override void Init()
    {
        base.Init();
        Managers.UI.CreatePopup<BlockInitPopup>();
    }
}
