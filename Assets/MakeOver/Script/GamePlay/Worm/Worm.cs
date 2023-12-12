using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Worm : MonoBehaviour, ICanPulledOut, IBoxCollider
{
    public Transform PosCenter;
    public Collider col;
    public bool bePullOut;
    public bool finish;
    public bool HasAnim;
    public Animator anim;
    public GameObject Core;
    public GameObject hole;
    public SpriteRenderer medicine;
    public bool playingFinish;
    public List<SpriteRenderer> worms;
    public bool CheckPickUp()
    {
        return bePullOut;
    }

    public Vector3 GetPosCenter()
    {
        return PosCenter.position;
    }
    public void ChangeStateSpRToVisibleInsideMask(int id)
    {
        worms[id].maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }
    public void ChangeStateSpRToNone(int id)
    {
        worms[id].maskInteraction = SpriteMaskInteraction.None;
    }
    public void PickUp(GameObject mkt)
    {
        
        if (bePullOut) return;
        bePullOut = true;
       
        if (HasAnim){
            playingFinish = false;
            anim.enabled = true;
            anim.Play("PickUp", -1, 0);
            StartCoroutine(ReShowMakeUpTool(mkt));
        }
        else
            Core.SetActive(false);
    }
    IEnumerator ReShowMakeUpTool(GameObject mkt)
    {
        yield return new WaitUntil (()=> playingFinish);
        mkt.SetActive(true);
        mkt.transform.position = PosCenter.position;
        Character.instance.GetCurrentManageObjectBePulledOut().CheckFinishPulledOut();
    }
    public IEnumerator DelayCall(float dur, Action call)
    {
        yield return new WaitForSeconds(dur);
        call?.Invoke();
    }
    public void RemoveHole()
    {
        hole.gameObject.SetActive(false);
        col.enabled = false;
    }
    
    public IEnumerator ShowMedicine()
    {
        float duration=1;
        medicine.gameObject.SetActive(true);
        medicine.transform.DOScale(1, duration).From(0.3f);
        yield return new WaitForSeconds(duration);
        medicine.DOFade(0, duration);
        hole.SetActive(false);
        medicine.gameObject.SetActive(false);
        finish = true;
        Character.instance.GetCurrentManageObjectBePulledOut().CheckFinish();// check all of hole of worm has been removed yet

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tweezer")&& Manager.Instance.GetCurrentAction().CurMakeUpTool.beUsing)
        {
            if (bePullOut) return;
            Tweezer tweezer = (Tweezer)other.GetComponent<DetectCollision>().GetMakeUpTool();
            tweezer.SetObjectBePickUp(this);
            tweezer.MoveToPos(GetPosCenter(), 1f);
        }
        if (other.gameObject.CompareTag("Medicine") &&Manager.Instance.GetCurrentAction().CurMakeUpTool.beUsing)
        {
            StartCoroutine( ShowMedicine());
            col.enabled = false;
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
    }
}
