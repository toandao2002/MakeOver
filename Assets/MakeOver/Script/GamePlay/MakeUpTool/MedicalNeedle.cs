using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MedicalNeedle : MakeUpTool
{
    public GameObject bar;
    private void OnEnable()
    {
  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("PosCorrect") && beUsing)
        {
            DoneAnim = false;
            AudioAssistant.Shot(TypeSound.Injection);
            bar.transform.DOLocalMove(new Vector3(0, 0, 0.5f), 1).OnComplete(()=> {
                ObjectPoolFx.instance.GetObjectFx(NameFx.MagicBuffBlue, Character.instance.GetBodyPart(NameBodyPart.FaceMask).GetPosition() - new Vector3(0, 2.5f, 0));
                Character.instance.ShowMask();
                AudioAssistant.Shot(TypeSound.VoiceWibu);
                Manager.Instance.GetCurrentAction().NextChildAction();
            });
            collision.gameObject.SetActive(false);
           // StartCoroutine(PopMask());
            /* Character.instance.PopMask();

             Manager.Instance.GetCurrentAction().NextChildAction();*/

        }
    }
    IEnumerator PopMask()
    {
        yield return new WaitUntil(() => DoneAnim);
       
    }
}
