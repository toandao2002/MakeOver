using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCreamMask : ActionMakeOver
{
    public MakeUpTool bottle;
  
    public ManageObjecBePulledOut manageCream;
    public override void StartAction()
    {
        base.StartAction();
        CurMakeUpTool = SpanwMakeUpTool(bottle, gameObject); ;
        CurMakeUpTool.AnimStart();
        Character.instance.SetManageObjecBePulledOutIsCream();
        manageCream = Character.instance.GetCurrentManageObjectBePulledOut();
        manageCream.ShowCollider();
        Character.instance.TurnOnSpriteMask();// only unse for cream or st be hide same time
    }
    public override void NextChildAction()
    {
        base.NextChildAction();
        ManageAction.EndAction?.Invoke();
        
        manageCream.HideCollider();
    }
    public IEnumerator DelayCall(float dur, Action Call)
    {
        yield return new WaitForSeconds(dur);
        Call?.Invoke();
    }
}
