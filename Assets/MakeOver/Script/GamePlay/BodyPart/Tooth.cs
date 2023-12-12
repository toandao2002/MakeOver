using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tooth : BodyPart
{
    public List<Sprite> stateTooth;
    public GameObject brace;
    public SpriteRenderer lipDefault;
    public SpriteRenderer lipCur;
    /*
     * 0 is yellow
     * 1 is white
     * 2 is brace
     * 3 is normal
     */
    public void ChangeStateTooth(int state ) 
    {
        sprite.sprite = stateTooth[state];
      
    }
    public override void SetSprite(Sprite spr)
    {
        sprite.sprite = spr;
       
        StartCoroutine(ChangeStateNormal());
    }
    IEnumerator  ChangeStateNormal()
    {
        
        yield return new WaitForSeconds(2);
        brace.SetActive(true);
        ChangeStateTooth(3);
        
        yield return new WaitForSeconds(1);

        brace.transform.DOLocalMoveX(brace.transform.localPosition.x + 1.5f, 1f).OnComplete(() =>
        {
            DoneAction = true;
            brace.SetActive(false);
            sprite.gameObject.SetActive(false);
          
        });
        yield return new WaitForSeconds(1);
        lipDefault.DOFade(0, 1).OnComplete(() =>
        {
            lipDefault.gameObject.SetActive(false);
        });
       /* lipCur.gameObject.SetActive(true);
        lipCur.DOFade(1, 1).From(0).OnComplete(() =>
        {

        });*/
    }
}
