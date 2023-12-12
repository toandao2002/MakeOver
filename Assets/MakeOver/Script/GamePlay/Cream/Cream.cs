using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Cream : ObjectHint, ICanPulledOut, IBoxCollider
{
    public Transform PosCenter;
    public BoxCollider2D col;
    public bool bePullOut;
    public bool finish;
    public bool HasAnim;
    public Animator anim;
    public GameObject Core;
    public bool playingFinish;
    private void OnEnable()
    {
        col.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Manager.Instance.GetCurrentAction().IsMakeUpToolBeUsed()) return; // if dont touch screen or makeup tool dont be used
        if (  collision.CompareTag("Bottle"))
        {

            Manager.Instance.GetCurrentAction().CurMakeUpTool.ActionMakeUpTool();
            Core.SetActive(true);
            Core.transform.DOScale(1f, 1.5f).From(0).OnComplete(()=> {
                bePullOut = true;
                if (Character.instance.GetCurrentManageObjectBePulledOut().CheckFinish())
                {


                }
            });
            col.enabled = false;
        }
    }
    public bool CheckFinish()
    {
        return bePullOut;
    }

    public bool CheckPickUp()
    {
        return bePullOut;
    }

    public Vector3 GetPosCenter()
    {
        return PosCenter.position;
    }

    public void HideBox()
    {
        col.enabled = false;
    }

    public void PickUp(GameObject mkt)
    {
       
    }

    public void ShowBox()
    {
        col.enabled = true;
    }

    
}
