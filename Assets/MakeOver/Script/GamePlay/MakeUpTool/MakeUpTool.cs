using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MakeUpTool : MonoBehaviour 
{
    public Transform positionDraw;
    public GameObject drawPen;
    public bool finishMakeUp;
    public bool beUsing;
    public bool BeLock;
    public bool beOut;
    public Animator ani;
    public bool DoneAnim;
    public bool BeingRunAnim;
    Camera cam;
    //public float TimeEndStart =1;
    public TypeSound typeSound;
    public TypeSound typeSoundAnimStart;
    public TypeSound typeSoundMachine;
    public bool hasSound;
    public bool hasSoundMachine;

    float durationTimePlaySound = 0;
    float timeSound;
 
    bool canPlaySound = true;
   
  
    void Start()
    {
        cam = Camera.main;
        ani = GetComponent<Animator>();
        timeSound = AudioAssistant.Instance.GetTimeAudioClip(typeSound);
        durationTimePlaySound = timeSound ;

    }
  
    public Transform GetPositionDraw()
    {
        return positionDraw;
    }
    public Transform GetPosition()
    {
        return transform;
    }
    private void OnMouseDown()
    {
        if (BeLock) return;
        beUsing = true;
        StartCoroutine(MoveFollowMouse());
        EventMouseDown();
    }
    public virtual void EventMouseDown() { }
    public virtual void EventMouseUp() { }
    public virtual void ActionMakeUpTool()
    {

    }
    public virtual void StopAction() { }
    public IEnumerator DeLayCall(float time, Action call)
    {
        yield return new WaitForSeconds(time);
        call?.Invoke();
    }
    public IEnumerator DeLayCallBool(BodyPart bodyPart, Action call)
    {
        yield return new WaitUntil(()=> bodyPart.DoneAction);
        call?.Invoke();
    }
    private void OnMouseUp()
    {
        StopMove();
        EventMouseUp();
    }
    public void StopMove()
    {
        beUsing = false;
        StopCoroutine(MoveFollowMouse());
    }
    public bool GetStateUse()
    {
        return beUsing;
    }
    public virtual void DoMakeUp() 
    { 
        
    }
    public virtual void AnimStart()
    {
        gameObject.SetActive(true);
        ani.enabled = true;
     
        ani.Play("Start",-1,0);
        StartCoroutine(DelayCall(  () => StopAnim()));
        if (hasSoundMachine)
        {
            AudioAssistant.Shot(TypeSound.Switch);
            AudioAssistant.Instance.PlaySoundLoop(typeSoundMachine);
        }

    }
    public virtual void AnimOut()
    {
        BeLock = true;
        StopMove();
        beOut = false;
        gameObject.transform.DOMove(gameObject.transform.position + new Vector3(4, 0, 0), 1).OnComplete(()=> {
            gameObject.SetActive(false);
        });
        if (hasSoundMachine)
        {
            AudioAssistant.Instance.StopSoundLoop(typeSoundMachine);
        }

    }
    public IEnumerator DelayCall(  Action Call)
    {
        yield return new WaitUntil(() => DoneAnim); ;
        Call?.Invoke();
    }
    public IEnumerator DelayCall(float time , Action Call)
    {
        yield return new WaitForSeconds(time);
        Call?.Invoke();
    }
    public void StopAnim()
    {

        ani.enabled = false;
    }
    IEnumerator MoveFollowMouse()
    { 
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -cam.transform.position.z;
        Vector3 posChange ;
        Vector3 posMouseFirst = cam.ScreenToWorldPoint(mousePos) ;
        Vector3 posToolFirst = drawPen.transform.position;
        
        while (beUsing && DoneAnim)
        {
            mousePos = Input.mousePosition;
            mousePos.z = -cam.transform.position.z; ;
            posChange = cam.ScreenToWorldPoint(mousePos) - posMouseFirst;
            posChange.z = 0;
            drawPen.transform.position = posToolFirst +  posChange;
           
            yield return null;
        }
       
    }
  

    public void PlaySound()
    {
        if (hasSound && canPlaySound  )
        {
            AudioAssistant.Shot(typeSound);
            canPlaySound = false;
            StartCoroutine(DelayPlaySound());
        }  
      
    }

    public void PlaySoundAnimStart()
    { 
         
        AudioAssistant.Shot(typeSoundAnimStart);
        

    }
    IEnumerator DelayPlaySound()
    {
        yield return new WaitForSeconds(durationTimePlaySound);
        canPlaySound = true;
    }
}
