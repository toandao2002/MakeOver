using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissor : ShavingRazor
{
 
    public GameObject left, right;
    public float ls, lf, rs, rf;
    public float duration;
    private void OnEnable()
    {
        left.transform.DOLocalRotate(new Vector3(0, 0, lf), duration).From(new Vector3(0, 0, ls)).SetLoops(-1, LoopType.Yoyo);
        right.transform.DOLocalRotate(new Vector3(0, 0, rf), duration).From(new Vector3(0, 0, rs)).SetLoops(-1, LoopType.Yoyo);
        BeingRunAnim = true;
        StopAction();
    }
    public override void ActionMakeUpTool()
    {
 
        if (BeingRunAnim) return;
        Fx.Play();
        BeingRunAnim = true;
        left.transform.DOPlay();
        right.transform.DOPlay();
        AudioAssistant.Instance.PlaySoundLoop(TypeSound.CutScissor);

    }
    public override void StopAction()
    {
  
        if (!BeingRunAnim) return;
        Fx.Stop();
        BeingRunAnim = false;
       // left.transform.DOKill();
        left.transform.DOPause();
       // right.transform.DOKill(); 
        right.transform.DOPause();
        AudioAssistant.Instance.StopSoundLoop(TypeSound.CutScissor);
    }
 
 
}
