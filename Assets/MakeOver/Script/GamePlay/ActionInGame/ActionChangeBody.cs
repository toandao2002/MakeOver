using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChangeBody : ActionMakeOver
{
    public MakeUpTool makeUpToolChange;
    public GameObject PosCorrect;
    BodyPart bd;
    public GameObject posTool;
    public int sizeDraw = 10;
    public override void StartAction()
    {
        posTool = Character.instance.PosIncubation;
        base.StartAction();
        bd = Character.instance.GetBodyPart(nameBodyPart);
        PosCorrect.transform.position = bd.GetPosition();
        bd.GetSpriteRenderer();
        var tmp  = (MakeUpToolChangeBodyPart)SpanwMakeUpTool(makeUpToolChange, posTool);
        CurMakeUpTool = tmp;
        var config = ShowSelection(nameBodyPart);
        if (config != null && config.sprites.Count >1)
        {
            ManageAction.EndChoseItemBodyPart += SetUpStartSelect;
        }
        else
        {
            SetUpStartNoSelect(tmp, config.sprites[0]);
        }
     


    }
    public  void SetUpStartNoSelect(MakeUpToolChangeBodyPart tmp, Sprite spr )
    {
        tmp.SetDataSprite(spr);
        CurMakeUpTool.AnimStart();
        PosCorrect.SetActive(true);
       
    }
    public void SetUpStartSelect( Sprite spr )
    {

       ( (MakeUpToolChangeBodyPart)CurMakeUpTool).SetDataSprite(spr);
        CurMakeUpTool.AnimStart();
        PosCorrect.SetActive(true);
        ManageAction.EndChoseItemBodyPart -= SetUpStartSelect;
    }
    public override void NextChildAction()
    {
        base.NextChildAction();
        bd.ChangeStateCollider(false);
        ManageAction.EndAction?.Invoke();
        PosCorrect.SetActive(false);
    }
}
