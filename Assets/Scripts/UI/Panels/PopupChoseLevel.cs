public class PopupChoseLevel : UIPanel
{
    public static PopupChoseLevel Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupChoseLevel;
    }

    public static void Show()
    {
        var newInstance = (PopupChoseLevel) GUIManager.Instance.NewPanel(UiPanelType.PopupChoseLevel);
        Instance = newInstance;
        newInstance.OnAppear();
    }

    public override void OnAppear()
    {
        if (isInited)
            return;

        base.OnAppear();

        Init();
    }

    private void Init()
    {
    }

    protected override void RegisterEvent()
    {
        base.RegisterEvent();
    }

    protected override void UnregisterEvent()
    {
        base.UnregisterEvent();
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }
}