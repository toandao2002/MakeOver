using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Incubation : MakeUpToolChangeBodyPart
{


    public override void OnEnable()
    {
        base.OnEnable();
        AudioAssistant.Shot(TypeSound.Switch);
        AudioAssistant.Instance.PlaySoundLoop(TypeSound.EngineHair);
    }
    private void OnDisable()
    {

        AudioAssistant.Instance.StopSoundLoop(TypeSound.HairDryer);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("PosCorrect") && beUsing)
        {

            tool.transform.DOMove(Character.instance.PosIncubation.transform.position,1).OnComplete(()=> {
                tool.gameObject.transform.position = Character.instance.PosIncubation.transform.position;
                PlayAnimChange();
                AudioAssistant.Instance.StopSoundLoop(TypeSound.EngineHair);
                AudioAssistant.Instance.PlaySoundLoop(TypeSound.HairDryer);
            });
             
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }



}
