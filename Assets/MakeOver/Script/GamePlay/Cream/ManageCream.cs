using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCream : ManageObjecBePulledOut
{

    public List<Cream> creams;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Cream i in creams)
        {
            ObjecBePulledOuts.Add(i);
        }
    }
    public override void ShowCollider()
    {
        foreach (var i in creams)
        {
            i.ShowBox();
        }
    }
    public override void HideCollider()
    {
        foreach (var i in creams)
        {
            i.HideBox();
        }
    }
}
