using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAcne : ManageObjecBePulledOut
{
    public List<Acne> acnes;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Acne i in acnes)
        {
            ObjecBePulledOuts.Add(i);
        }
    }
    public override void ShowCollider()
    {
        foreach (Acne i in acnes)
        {
            i.ShowBox();
        }
    }
    public override void HideCollider()
    {
        foreach (Acne i in acnes)
        {
            i.HideBox();
        }
    }
}
