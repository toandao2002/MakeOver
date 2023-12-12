using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushTooth : ActionMakeOver
{
    public MakeUpTool cleanser;
    public MakeUpTool shower;
    public MakeUpTool comb;
    public ManageSoft manageSoftTooth;

    public StateSoft stateSoft = StateSoft.DontShow;
    public override void StartAction()
    {
        base.StartAction();
        CurMakeUpTool = SpanwMakeUpTool(cleanser, gameObject); ;
        CurMakeUpTool.AnimStart();
        manageSoftTooth = Character.instance.SetMangageSoftTooth();
        manageSoftTooth.ShowCollider();

    }
    public override void NextChildAction()
    {
        base.NextChildAction();

        switch (stateSoft)
        {

            case StateSoft.DontShow:
                stateSoft = StateSoft.Soft;
                CurMakeUpTool = SpanwMakeUpTool(comb, gameObject); ;
                break;
            case StateSoft.Soft:
                stateSoft = StateSoft.SoftWater;
                CurMakeUpTool = SpanwMakeUpTool(shower, gameObject); ;
                break;
            case StateSoft.SoftWater:
                ManageAction.EndAction?.Invoke();
                manageSoftTooth.HideCollider();
                var tooth = (Tooth)Character.instance.GetBodyPart(NameBodyPart.Tooth);
                tooth.ChangeStateTooth(1);// change state white tooth
                return;

        }
        CurMakeUpTool.AnimStart();
    }
}
