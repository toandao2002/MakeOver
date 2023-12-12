using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSkin : ObjectHint, ICanPulledOut, IBoxCollider
{
    public Collider col;
    public bool beingPullOut;
    public bool finish;
    public bool HasAnim;
    public GameObject sprPre;

 
    public bool playingFinish;

    [Header("Spine")]
    [SpineAnimation]
    public String pullOut;
    public GameObject wormSpine;
    public MakeUpTool tweezer;
    public GameObject headWorm;
    public GameObject tailWorm;
    public Spine.AnimationState aniSpine;
    public Vector3 posTweezerFirst;
    public Vector3 distanceWromFirst;
    public GameObject makeUpTool;
    public BoxCollider2D boxTweezer;
    public bool CheckPickUp()
    {
        return beingPullOut;
    }
    private void OnEnable()
    {
        aniSpine = wormSpine.GetComponent<SkeletonAnimation>().AnimationState;
        posTweezerFirst = tweezer.transform.position;
        distanceWromFirst = headWorm.transform.position - tailWorm.transform.position;
 
    }
    public Vector3 GetPosCenter()
    {
        return tweezer.transform.position;
    }
    public bool firstSound;
    private void Update()
    {
        if (beingPullOut && !playingFinish && tweezer.beUsing)
        {
            float dis = Vector3.Distance(posTweezerFirst, tweezer.transform.position);
            if (dis > 2)
            {

                playingFinish = true;
                StartCoroutine(ReShowMakeUpTool(makeUpTool));
                AudioAssistant.Shot(TypeSound.StrecthSnap);
                AudioAssistant.Instance.StopSoundLoop(TypeSound.StrecthStay);
            }
            else if (dis > 0.2f || firstSound)
            {
                AudioAssistant.Instance.PlaySoundLoop(TypeSound.StrecthStay);
            }
            else
            {
                AudioAssistant.Shot(TypeSound.StrecthFirst);
                firstSound = true;
            }
        }
        else
        {
            firstSound = false;
        }
        if (playingFinish)
        {
            tailWorm.transform.position = headWorm.transform.position - distanceWromFirst;
        }
    }

    public void PickUp(GameObject mkt)
    {
        if (beingPullOut) return;
        sprPre.SetActive(false);
        wormSpine.SetActive(true);
        makeUpTool = mkt;
        aniSpine.SetAnimation(0, pullOut, true);
        boxTweezer.enabled = true;
        beingPullOut = true;
        playingFinish = false;
        //makeUpTool = mkt;
    }

    IEnumerator ReShowMakeUpTool(GameObject mkt)
    {
        yield return new WaitForSeconds(1);
        mkt.SetActive(true);
        mkt.transform.position = tweezer.gameObject.transform.position;
        wormSpine.SetActive(false); 
        Character.instance.GetCurrentManageObjectBePulledOut().CheckFinishPulledOut();
        gameObject.SetActive(false);
    }
    public IEnumerator DelayCall(float dur, Action call)
    {
        yield return new WaitForSeconds(dur);
        call?.Invoke();
    }
    public void RemoveHole()
    {
        col.enabled = false;
    }

  /*  public IEnumerator ShowMedicine()
    {
        float duration = 1;
        medicine.gameObject.SetActive(true);
        medicine.transform.DOScale(1, duration).From(0.3f);
        yield return new WaitForSeconds(duration);
        medicine.DOFade(0, duration);
        hole.SetActive(false);
        medicine.gameObject.SetActive(false);
        finish = true;
        Character.instance.GetCurrentManageObjectBePulledOut().CheckFinish();// check all of hole of worm has been removed yet
        gameObject.SetActive(false);
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tweezer") && Manager.Instance.GetCurrentAction().CurMakeUpTool.beUsing)
        {
            if (beingPullOut) return;
            Tweezer tweezer = (Tweezer)other.GetComponent<DetectCollision>().GetMakeUpTool();
            tweezer.SetObjectBePickUp(this);
            tweezer.MoveToPos(GetPosCenter(), 1f);
        }
     
    }

    public bool CheckFinish()
    {
        return finish;
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
