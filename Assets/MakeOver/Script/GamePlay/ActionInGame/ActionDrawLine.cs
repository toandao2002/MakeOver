using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDrawLine : ActionMakeOver, IEffectFinish
{ 
    public MakeUpTool makeUpTool;
    public int sizeDraw = 10;
  
    public override void StartAction()
    {
        base.StartAction();
        CurMakeUpTool = SpanwMakeUpTool(makeUpTool , gameObject); ;
        var config = ShowSelection(nameBodyPart);
        if (config != null && config.sprites.Count > 1)
        {
            ManageAction.EndChoseItemBodyPart += ShowMakeUpTool;
        }
        else if(config != null && config.sprites.Count == 1)
        {
            ShowMakeUpTool(config.sprites[0]);
        }
        else
        {
            ShowMakeUpTool(null);
        }
       
    }

    public void ShowMakeUpTool(Sprite spr = null)
    {
        var tmp = nameBodyPart;
        if (spr != null)
            Manager.Instance.GetCurrentAction().SetSpirteForBodyPart(spr);
        Manager.Instance.ZoomInCam(() => {
            CurMakeUpTool.AnimStart();
            SetUpDrawLine(sizeDraw, nameBodyPart, StateDraw.Show);
        });
        ManageAction.EndChoseItemBodyPart -= ShowMakeUpTool;
    }
    public override void NextChildAction()
    {
        base.NextChildAction();
        
        Manager.Instance.ZoomOutCam(()=>ManageAction.EndAction?.Invoke());
    }
    public override void EndAction()
    {
        base.EndAction();
        ShowEffectFinish();
    }
    public void ShowEffectFinish()
    {
        ObjectPoolFx.instance.GetObjectFx(NameFx.StunExplosion, Character.instance.GetBodyPart(nameBodyPart).GetPosition());
        AudioAssistant.Shot(TypeSound.Ting);
    }
}
