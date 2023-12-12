using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MyUiPanel : MonoBehaviour
{
    public GameObject Root;
    public float duration;
    public virtual void Show()
    {
        Root.SetActive(true);
        Root.transform.DOScale(1, duration).SetEase(Ease.InOutBounce).From(0);
    }
    public virtual void Hide()
    {
        Root.transform.DOScale(0, duration).SetEase(Ease.InOutBounce);
      
    }
    public virtual void ResetData()
    {
        
    }
}
