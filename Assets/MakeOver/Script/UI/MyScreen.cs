using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyScreen : MonoBehaviour
{
    public static MyScreen Instance;
    public HCButton stopDrawLine;
    public HCButton hint;

    public HCButton restart;
    public InputField  input;
    public int num1, num2;
  
    private void Awake()
    {
        Instance = this;
    }
    public void Start()
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
    public void  ReloadScene()
    {
        ManageAction.EndAction = null;
        if (input.text.Trim().Length != 0)
        {
            GameManager.Instance.data.user.level = Int32.Parse(  input.text);
            Database.SaveData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        
        
    }
}
