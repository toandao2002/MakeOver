using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ManageSoft : MonoBehaviour, IHintHighLight
{
    public List<Soft> softs;
    public bool ChecOutkState(StateSoft st)
    {
        int cnt = 0;
        foreach (Soft i in softs)
        {
            if (i.CheckState(st)) cnt++;
        }
        return cnt == 0;
    }
    
    public void ShowCollider()
    {
        foreach (Soft i in softs)
        {
            i.ShowBox();
        }
    }
    public void HideCollider()
    {
        foreach (Soft i in softs)
        {
            i.HideBox();
        }
    }

    public void ShowHint()
    {
        foreach (Soft i in softs)
        {
            i.ShowHint();
        }
    }
}
