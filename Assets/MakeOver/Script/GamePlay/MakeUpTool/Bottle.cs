using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bottle : MakeUpTool
{
    public GameObject lid;
    public Transform PosS;
    public Transform PosE;
    bool finishActioin = true;
    public bool hasAnim;
    public override void ActionMakeUpTool()
    {
        base.ActionMakeUpTool();
      
        if (finishActioin && hasAnim)
        {
            lid.transform.DOKill();
            finishActioin = false;
            lid.transform.DOLocalMoveY(PosE.localPosition.y, 0.2f).From(PosS.localPosition.y).SetLoops(2, LoopType.Yoyo).OnComplete(()=> finishActioin = true);
            AudioAssistant.Shot(TypeSound.SprayShamboo);
        }
        else if (finishActioin)
        {
            AudioAssistant.Shot(TypeSound.SprayShamboo);
        }
    }
}
