using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionComb : ActionMakeOver
{
    public MakeUpTool makeUpTool;
    public override void StartAction()
    {
        base.StartAction();
        CurMakeUpTool = SpanwMakeUpTool(makeUpTool, gameObject);
        Character.instance.GetMangageGarbage().ShowCollider();
        Character.instance.SetHintGarbage();
        CurMakeUpTool.AnimStart();

    }
    public override void NextChildAction()
    {
        base.NextChildAction();
        ManageAction.EndAction?.Invoke();
        Character.instance.GetMangageGarbage().HideCollider();
    }
}
