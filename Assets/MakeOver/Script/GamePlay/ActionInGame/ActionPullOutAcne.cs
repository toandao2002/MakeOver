using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPullOutAcne : ActionMakeOver
{
    public MakeUpTool tweezer;
    public ManageObjecBePulledOut manageAcne;
    public override void StartAction()
    {
        base.StartAction();
        CurMakeUpTool = SpanwMakeUpTool(tweezer , gameObject); ;
        Manager.Instance.ZoomInCam(()=>
        CurMakeUpTool.AnimStart());
        Character.instance.SetManageObjecBePulledOutIsAcne();
        manageAcne = Character.instance.GetCurrentManageObjectBePulledOut();
        manageAcne.ShowCollider();
    }
    public override void NextChildAction()
    {
        base.NextChildAction();
        Manager.Instance.ZoomOutCam( ()=>
        ManageAction.EndAction?.Invoke());
        manageAcne.HideCollider();
    }
    public IEnumerator DelayCall(float dur, Action Call)
    {
        yield return new WaitForSeconds(dur);
        Call?.Invoke();
    }
}
