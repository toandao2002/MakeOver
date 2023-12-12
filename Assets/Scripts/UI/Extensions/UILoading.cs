#region

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class UILoading : MonoBehaviour
{
    public Image loadingBg;
    public TMP_Text loadingLb;
    public Image progressBar;
    public Canvas root;

    public void ChangeLoadingBackground(Sprite sprite)
    {
        loadingBg.sprite = sprite;
    }

    public void Write(string s)
    {
        loadingLb.text = s;
    }

    public void Progress(float p, float speed)
    {
        DOTween.Kill(this); 
        DOTween.To(() => progressBar.fillAmount, x => progressBar.fillAmount = x, p, speed)
                    .SetSpeedBased(true).SetEase(Ease.Linear).SetTarget(this);
    }
}