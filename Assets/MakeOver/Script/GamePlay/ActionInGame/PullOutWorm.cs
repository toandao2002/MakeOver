using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullOutWorm : ActionMakeOver
{
    public MakeUpTool tweezer;
    public MakeUpTool medicine;
    public ManageObjecBePulledOut manageWorm;
    public override void StartAction()
    {
        base.StartAction();
        CurMakeUpTool = SpanwMakeUpTool(tweezer, gameObject); ;
        Manager.Instance.ZoomInCam(() =>
        CurMakeUpTool.AnimStart());
        Character.instance.SetManageObjecBePulledOutIsWorm();
        manageWorm = Character.instance.GetCurrentManageObjectBePulledOut();
        manageWorm.ShowCollider();
    }
    public override void NextChildAction()
    {
        base.NextChildAction();
        if (nameAction == NameAction.Action1)
        {
            Manager.Instance.ZoomOutCam(() =>
                ManageAction.EndAction?.Invoke());

            manageWorm.HideCollider();
            return;
        }
        AudioAssistant.Instance.StopSoundLoop(TypeSound.Worm);// has all worm be pulled out
        nameAction = NameAction.Action1;
        CurMakeUpTool = SpanwMakeUpTool(medicine , gameObject); ;
        StartCoroutine(DelayCall(1,()=> { CurMakeUpTool.AnimStart(); }));
    }
    public IEnumerator DelayCall(float dur ,Action Call)
    {
        yield return new WaitForSeconds(dur);
        Call?.Invoke();
    }
}
