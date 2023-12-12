using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGarbage : MonoBehaviour, IHintHighLight
{
    public List<Garbage> garbages;
    public void DropGarbage()
    {
        float duration = 1;
        for (int i = 0; i < garbages.Count; i++)
        {
            garbages[i].Drop();
        }
    }
    public void DropGarbageFolowDir()
    {
        float duration = 1;
        for (int i = 0; i < garbages.Count; i++)
        {
            garbages[i].DropByDir();
        }
    }
    public bool ChecBeAllOut( )
    {
        int cnt = 0;
        foreach (var i in garbages)
        {
           

            if (i.CheckBeOut()) cnt++; 
        }
        if (cnt >= garbages.Count - garbages.Count / 2) return true;
        return false;
    }
    public void ShowCollider()
    {
        foreach (var i in garbages)
        {
            i.ShowBox();
        }
    }
    public void HideCollider()
    {
        foreach (var i in garbages)
        {
            i.HideBox();
        }
    }

    public void ShowHint()
    {
        for (int i = 0; i < garbages.Count; i++)
        {
            garbages[i].ShowHint();
        }
    }
}
