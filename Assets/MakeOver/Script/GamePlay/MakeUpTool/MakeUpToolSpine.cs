using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeUpToolSpine : MakeUpTool
{
    [SpineAnimation]
    public String brush;
    public Spine.AnimationState aniSpine;
    public GameObject spineObject;
    public SkeletonAnimation ske;
    private void OnEnable()
    {
        ske = spineObject.GetComponent<SkeletonAnimation>();
        aniSpine = ske.AnimationState;
    }
    public override void ActionMakeUpTool()
    {
        base.ActionMakeUpTool(); 
        ske.timeScale = 1;
      
    }
    public override void StopAction()
    {
        base.StopAction();
        ske.timeScale = 0.1f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
