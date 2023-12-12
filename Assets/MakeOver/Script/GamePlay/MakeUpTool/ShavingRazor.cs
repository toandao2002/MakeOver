using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ShavingRazor : MakeUpTool
{
    public ParticleSystem Fx;
    public bool playFx;
    public bool hasAnim;
 /*   public override void EventMouseDown()
    {
        base.EventMouseDown();
        if (playFx) ActionMakeUpTool();
    }
    public override void EventMouseUp()
    {
        base.EventMouseUp();
        StopAction();
    }*/

   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AreaDraw") )
        {
            playFx = true;
            if (beUsing)
                ActionMakeUpTool();
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AreaDraw"))
        {
            playFx = false;
            StopAction();
        }
    }*/
    public override void ActionMakeUpTool()
    {
        base.ActionMakeUpTool();
        if (!BeingRunAnim)
        {
            Fx.Play();
            BeingRunAnim = true;
        }    
    }
    public override void StopAction()
    {
        base.StopAction();
        if (BeingRunAnim)
        {
            Fx.Stop();
            BeingRunAnim = false;

        }
    }
    public override void EventMouseUp()
    {
        base.EventMouseUp();
        StopAction();
    }
    public override void AnimOut()
    {
        base.AnimOut();
        AudioAssistant.Instance.StopSoundLoop(typeSound);
    }

}
