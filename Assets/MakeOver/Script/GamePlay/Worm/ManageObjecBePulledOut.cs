using System.Collections.Generic;
using UnityEngine;

public class ManageObjecBePulledOut : MonoBehaviour, IHintHighLight
{
    public List<ICanPulledOut> ObjecBePulledOuts = new List<ICanPulledOut>();
    public virtual void ShowCollider() { }
    public virtual void HideCollider() { }
    public bool CheckFinish()
    {
        foreach (ICanPulledOut i in ObjecBePulledOuts)
        {
            if (!i.CheckFinish()) return false;
        }
        Manager.Instance.GetCurrentAction().NextChildAction(); // call when all of womrs be pull out or dead skin be removed
        return true;
    }
    public bool CheckFinishPulledOut()
    {
        foreach(ICanPulledOut i in ObjecBePulledOuts)
        {
            if (!i.CheckPickUp()) return false;
        }
        Manager.Instance.GetCurrentAction().NextChildAction(); // call when all of womrs be pull out or dead skin be removed
        return true;
    }
    public void ShowHint()
    {
        foreach (ICanPulledOut i in ObjecBePulledOuts)
        {
            ((ObjectHint)i).ShowHint();
        }
    }
}
