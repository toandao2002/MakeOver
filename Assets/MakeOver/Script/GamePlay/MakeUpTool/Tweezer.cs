using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Tweezer : MakeUpTool
{
    [SerializeField]
    public ICanPulledOut ObjectBePickUp;
    public GameObject tweezer;
    public bool hasAnim;
    
    // Start is called before the first frame update
   
    // Update is called once per frame
 
    public void MoveToPos( Vector3  pos, float dur)
    {
        pos.z = tweezer.transform.position.z;
        StopMove();
        BeLock = true;
        tweezer.transform.DOMove(pos, dur).OnComplete(() => {
            
            gameObject.SetActive(false);
            ObjectBePickUp.PickUp(gameObject);
            BeLock = false;
            AudioAssistant.Shot(TypeSound.Tweezer);
            DoneAnim = true;
        });
    }
    
    public void SetObjectBePickUp(ICanPulledOut obj)
    {
        ObjectBePickUp = obj;
    }
    
}
