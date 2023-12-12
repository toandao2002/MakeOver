using Sirenix.OdinInspector;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acne : ObjectHint, ICanPulledOut, IBoxCollider
{
    public Transform PosCenter;
    public Collider col;
    public bool bePullOut;
    public bool finish;
    public bool HasAnim;
    public Animator anim;
    public GameObject hole;
    public bool playingFinish;
    public GameObject needle;
    public GameObject anceSpine;
    [Header("Spine")]
    [SpineAnimation]
    public String small;
    public Spine.AnimationState aniSpine;
    public SkeletonAnimation ske;
 
    private void OnEnable()
    {
        aniSpine = anceSpine.GetComponent<SkeletonAnimation>().AnimationState;
        ske = anceSpine.GetComponent<SkeletonAnimation>();
       
    }
    
    public bool CheckFinish()
    {
        return bePullOut;
    }
    [Button]
    public void PickUp1()
    {
        if (HasAnim && !bePullOut)
        {
            /*anim.enabled = true;
            anim.Play("PickUp", -1, 0);*/
            //StartCoroutine(ReShowMakeUpTool(mkt));
            ske.loop = false;
            aniSpine.SetAnimation(0, small, true);
            needle.gameObject.SetActive(false);
           
            aniSpine.Complete += delegate {
                //aniSpine.SetAnimation(aniSpine.SetAnimation(0, "None", true););
                anceSpine.SetActive(false);
             
                needle.gameObject.SetActive(true);
                needle.transform.position = PosCenter.position;
                Character.instance.GetCurrentManageObjectBePulledOut().CheckFinishPulledOut();
                hole.SetActive(false);
            };
       
        }
        else
            hole.SetActive(false);
        col.enabled = false;
        bePullOut = true;

    }
    public void PickUp(GameObject mkt)
    {
        if (HasAnim&& !bePullOut )
        {
            /*anim.enabled = true;
            anim.Play("PickUp", -1, 0);*/
            //StartCoroutine(ReShowMakeUpTool(mkt));
            anceSpine.SetActive(true);
            aniSpine.SetAnimation(0, small, true);
            AudioAssistant.Shot(TypeSound.AncePush);
            needle = mkt;
            aniSpine.Complete += delegate {
                anceSpine.SetActive(false);
                needle.gameObject.SetActive(true);
                needle.transform.position = PosCenter.position;
                Character.instance.GetCurrentManageObjectBePulledOut().CheckFinishPulledOut();
                hole.SetActive(false);
            };
        }
        else
        {
            Debug.Log("Done Action Pull out acne or mkt Dont be using");
        }
        col.enabled = false;
        bePullOut = true;
     
    }
   
    public void end1()
    {

    }
  
    IEnumerator ReShowMakeUpTool(GameObject mkt)
    {
        yield return new WaitUntil(() =>playingFinish );
        mkt.SetActive(true);
        mkt.transform.position = PosCenter.position;
        Character.instance.GetCurrentManageObjectBePulledOut().CheckFinishPulledOut();
    }
    public bool CheckPickUp()
    {
        return bePullOut;
    }

    public Vector3 GetPosCenter()
    {
        return PosCenter.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Needle") && Manager.Instance.GetCurrentAction().CurMakeUpTool.beUsing)
        {
            Tweezer tweezer = (Tweezer)other.GetComponent<DetectCollision>().GetMakeUpTool();
            tweezer.SetObjectBePickUp(this);
            tweezer.MoveToPos(GetPosCenter(), 1f);
        }
         
    }

    public void HideBox()
    {
        col.enabled = false;
    }

    public void ShowBox()
    {
        col.enabled = true;
        StartCoroutine(ChangeMtr());
    }
   
}
