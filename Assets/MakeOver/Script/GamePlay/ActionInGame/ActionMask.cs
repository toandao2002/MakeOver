using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionMask : ActionMakeOver,IEffectFinish
{
    public MakeUpTool brush;
    public MakeUpTool sponge;// bot bien
    public int sizeDraw = 10;
    public bool hasMoisturize;
    public Moisturize moisturize;
    public override void StartAction()
    {
        base.StartAction();
        CurMakeUpTool = SpanwMakeUpTool(brush, gameObject);
        CurMakeUpTool.AnimStart();
        SetUpDrawLine(sizeDraw, nameBodyPart, StateDraw.Show);
    }
    public override void NextChildAction()
    {
        base.NextChildAction();
        if (nameAction == NameAction.Action1 ||!hasMoisturize)
        {
            ManageAction.EndAction?.Invoke();
          
            return;
        }
      //  if (!hasMoisturize) return;
        StartCoroutine(MoisturizingCouroutine());
        nameAction = NameAction.Action1;
  
        CurMakeUpTool = SpanwMakeUpTool(sponge , gameObject); ;
        
       
    }
    IEnumerator MoisturizingCouroutine()
    {
        Moisturizing();
        yield return new WaitUntil(()=> moisturize.finish);
        SetUpDrawLine(sizeDraw, nameBodyPart, StateDraw.Hide);
        //Character.instance.PopMask();
        Character.instance.ShowMask();
        CurMakeUpTool.AnimStart();
    }
    public void Moisturizing()
    {
        moisturize.ActionMoisturize(Character.instance.GetBodyPart(NameBodyPart.Eye).GetPosition(),
                Character.instance.GetBodyPart(NameBodyPart.LipStick).GetPosition(),1);
    }
    public override void EndAction()
    {
        base.EndAction();
        ShowEffectFinish();
    }
    public void ShowEffectFinish()
    {
        if(Probability(50))
            AudioAssistant.Shot(TypeSound.Amazing);
        else 
            AudioAssistant.Shot(TypeSound.Ting);
        ObjectPoolFx.instance.GetObjectFx(NameFx.StunExplosion, Character.instance.GetBodyPart(nameBodyPart).GetPosition());
    }
    public bool Probability(int val)
    {
        return (Random.Range(0, 11) < val);   
    }
}
