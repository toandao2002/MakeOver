using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBodyPart : MonoBehaviour
{
    public Image icon;
    public int id;
    public HCButton button;
    public bool stateWatchAds;
    public GameObject iconWatchAds;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ChoseItem);
    }
    private void OnEnable()
    {

        iconWatchAds.SetActive(stateWatchAds);

    }
    public void SetIcon(Sprite spr)
    {
        icon.sprite = spr;
    }
    public void ChoseItem()
    {
        Action call = () => {
            ManageAction.EndChoseItemBodyPart?.Invoke(icon.sprite);
            PopupShowSelection.Instance.Hide();
        };
        if (stateWatchAds)
        {
            AdManager.Instance.ShowRewardedAds("GetToolMakeUp",call, call);
            return;
        }
        call.Invoke();
    }
}
