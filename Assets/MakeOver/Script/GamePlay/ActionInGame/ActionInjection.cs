using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionInjection : ActionMakeOver
{
    public MakeUpTool makeUpToolChange;
    public GameObject PosCorrect;

    public int sizeDraw = 10;
    public override void StartAction()
    {
        base.StartAction();
        PosCorrect.transform.position = Character.instance.GetBodyPart(nameBodyPart).GetPosition();
        CurMakeUpTool = SpanwMakeUpTool(makeUpToolChange, gameObject);
        CurMakeUpTool.AnimStart();
        PosCorrect.SetActive(true);


    }
    public override void NextChildAction()
    {
        base.NextChildAction();
        ManageAction.EndAction.Invoke();
    }
}
