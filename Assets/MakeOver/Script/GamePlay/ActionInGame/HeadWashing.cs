using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadWashing : ActionMakeOver
{
    public MakeUpTool cleanser;
    public MakeUpTool shower;
    public MakeUpTool comb;
    public ManageSoft manageSoft;
 
    public StateSoft stateSoft =StateSoft.DontShow;
    public override void StartAction()
    {
        base.StartAction();
        CurMakeUpTool = SpanwMakeUpTool(cleanser , gameObject); ;
        CurMakeUpTool.AnimStart();
        manageSoft = Character.instance.SetMangageSoftHair();
        manageSoft.ShowCollider();
      
    }
    public override void NextChildAction()
    {
        base.NextChildAction();
      
        switch (stateSoft)
        {
           
            case StateSoft.DontShow:
                stateSoft = StateSoft.Soft;
                CurMakeUpTool = SpanwMakeUpTool(comb , gameObject); ;
                break;
            case StateSoft.Soft:
                stateSoft = StateSoft.SoftWater;
                CurMakeUpTool = SpanwMakeUpTool(shower , gameObject); ;
                break;
            case StateSoft.SoftWater:
                var cf = ShowSelection(NameBodyPart.HairWashing);
                if (cf != null) // make hair wet
                {
                    Character.instance.GetBodyPart(NameBodyPart.HairWashing).SetSprite(cf.sprites[0]);
                    Character.instance.GetBodyPart(NameBodyPart.HairDefault).sprite.gameObject.SetActive(true);
                }
                ManageAction.EndAction?.Invoke();
                manageSoft.HideCollider();
                Character.instance.GetMangageGarbage().DropGarbage();
                Character.instance.GetBodyPart(NameBodyPart.DirtyHair).HideAnimFade();
                return;

        }
        CurMakeUpTool.AnimStart();
    }
  
}
