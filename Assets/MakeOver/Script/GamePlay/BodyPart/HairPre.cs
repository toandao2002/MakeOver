using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairPre : BodyPart
{
    public override void SetSprite(Sprite spr)
    {
        //base.SetSprite(spr);
        //sprite.gameObject.SetActive(true);
        //StartCoroutine(efHideHair(spr));
        sprite.sprite = spr;
    }
 
    IEnumerator efHideHair(Sprite spr)
    {
        sprite.DOFade(0.5f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        sprite.DOKill();
        sprite.sprite = spr;

        sprite.DOFade(1, 0.5f).From(0.5f);
        DoneAction = true;
    }
     
}
