using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDrawHide : ActionMakeOver
{
    public MakeUpTool makeUpToolLipStick;
    public bool hasSpriteMask;
  
    public int sizeDraw = 10;
    public override void StartAction()
    {
        base.StartAction();
        CurMakeUpTool  = SpanwMakeUpTool(makeUpToolLipStick , gameObject); ;
        CurMakeUpTool.AnimStart();
        SetUpDrawLine(sizeDraw, nameBodyPart,StateDraw.Hide);
    }
    public override void NextChildAction()
    {
        base.NextChildAction();
        ManageAction.EndAction.Invoke();
        if (hasSpriteMask) Character.instance.TurnOffSpriteMask();
    }

}
