using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanPulledOut 
{
    public Vector3 GetPosCenter();
    
    public void PickUp(GameObject mkt);
    public bool CheckPickUp();
    public bool CheckFinish();
}
