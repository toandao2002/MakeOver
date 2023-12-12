using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScreen : UIPanel
{
    
    public static PlayScreen Instance { get; private set; }

    public HCButton stopDrawLine;
    public HCButton hint;
    public TMP_Text level;
    public HCButton restart;
    public InputField input;
    public int num1, num2;
    private void OnEnable()
    {
        level.text = "Level: " + GameManager.Instance.data.user.level; 
    }
    private void Start()
    {
        stopDrawLine.onClick.AddListener(() => {
            AreaDraw.instance.StopActionDraw();
            HideBTNStopDrawLine();
        });
        hint.onClick.AddListener(
            () => {
                Character.instance.GetCurrentShowHint().ShowHint();
            }
        );
    }
    public void ShowBTNStopDrawLine()
    {
        stopDrawLine.Show();
    }
    public void HideBTNStopDrawLine()
    {
        stopDrawLine.Hide();

    }
    public void BackHome()
    {
        PlayScreen.Instance.Hide();
        LoadingManager.Instance.LoadScene(SceneIndex.Home);
        MainScreen.Show();
        AudioAssistant.Instance.CloseAllSoundLoop();
        AudioAssistant.Instance.PlayMusic("Home");
    }
    public void ReloadScene()
    {
        ManageAction.EndAction = null;
        if (input.text.Trim().Length != 0)
        {
            GameManager.Instance.data.user.level = Int32.Parse(input.text);
            Database.SaveData();
            AudioAssistant.Instance.CloseAllSoundLoop();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }


    }
    public override UiPanelType GetId()
    {
        return UiPanelType.PlayScreen;
    }

    public static void Show()
    {
        var newInstance = (PlayScreen) GUIManager.Instance.NewPanel(UiPanelType.PlayScreen);
        Instance = newInstance;
        newInstance.OnAppear();
    }

    public override void OnAppear()
    {
        base.OnAppear();
        Init();
    }

    private void Init()
    {
    }

    public void ShowSetting()
    {
        PopupSetting.Show();
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }
}