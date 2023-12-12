using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDragDeadLip : ActionMakeOver
{
    public MakeUpTool tweezer;
    public ManageObjecBePulledOut manageDeadLip;
    public override void StartAction()
    {
        base.StartAction();
        CurMakeUpTool = SpanwMakeUpTool(tweezer, gameObject); ;
        CurMakeUpTool.AnimStart();
        Character.instance.SetManageObjecBePulledOutIsDeadLip();
        manageDeadLip = Character.instance.GetCurrentManageObjectBePulledOut();
        manageDeadLip.ShowCollider();
    }
    public override void NextChildAction()
    {
        base.NextChildAction();
        manageDeadLip.HideCollider();
        ManageAction.EndAction?.Invoke(); 
    }

}
