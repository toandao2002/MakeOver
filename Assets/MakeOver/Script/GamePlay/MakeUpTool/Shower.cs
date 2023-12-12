using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : MakeUpTool
{
    public List<GameObject> waters;
    public int idWater;
    public int divideFrame = 5;
    int currentFrame;
    public bool isBrushTooth;
    private void OnEnable()
    {
        AudioAssistant.Shot(TypeSound.ShowerStartRuning);
        if(isBrushTooth)
           AudioAssistant.Instance.PlaySoundLoop(TypeSound.SprayWater);
        else 
            AudioAssistant.Instance.PlaySoundLoop(TypeSound.ShowerStayRuning);
       
    }
    public override void AnimOut()
    {
        base.AnimOut();
        if (isBrushTooth)
            AudioAssistant.Instance.StopSoundLoop(TypeSound.SprayWater);
        else 
            AudioAssistant.Instance.StopSoundLoop(TypeSound.ShowerStayRuning); 
    }

    private void Update()
    {
        if (currentFrame % divideFrame == 0)
        {
            for (int i = 0; i < waters.Count; i++)
            {
                if (idWater == i)
                    waters[i].SetActive(true);
                else
                    waters[i].SetActive(false);
            }
            idWater += 1;
            idWater %= waters.Count;
        }
        currentFrame++;
        if (currentFrame >= 20000) currentFrame = 0;

    }
}
