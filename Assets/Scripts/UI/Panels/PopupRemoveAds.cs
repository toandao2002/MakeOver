public class PopupRemoveAds : UIPanel
{
    public static PopupRemoveAds Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupRemoveAds;
    }

    public static void Show()
    {
        var newInstance = (PopupRemoveAds) GUIManager.Instance.NewPanel(UiPanelType.PopupRemoveAds);
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