using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HairDefault : BodyPart
{
    public override void SetSprite(Sprite spr)
    {
        //base.SetSprite(spr);
        //sprite.gameObject.SetActive(true);
        StartCoroutine(efHideHair(spr));
        Character.instance.GetBodyPart(NameBodyPart.HairPre).SetSprite(null);
    }
    public override void ChangeMaterialHighLight(Material mtr)
    {
        base.ChangeMaterialHighLight(mtr);
        Character.instance.GetBodyPart(NameBodyPart.HairPre).ChangeMaterialHighLight(mtr);
    }
    IEnumerator efHideHair(Sprite spr)
    { 
            sprite.sprite = spr;
 
            DoneAction = true;
  
        yield return null;
       
    }
    public override void ChangeMaterialNormal()
    {
        base.ChangeMaterialNormal();
        Character.instance.GetBodyPart(NameBodyPart.HairPre).ChangeMaterialNormal();

    }
}
