using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWorm : ManageObjecBePulledOut
{
    public List<WormSpine> worms;
    // Start is called before the first frame update
    void Start()
    {
        foreach(WormSpine i in worms)
        {
            ObjecBePulledOuts.Add(i);
        }
    }
    public override void ShowCollider()
    {
        foreach (WormSpine i in worms)
        {
            i.ShowBox();
        }
    }
    public  override void HideCollider()
    {
        foreach (WormSpine i in worms)
        {
            i.HideBox();
        }
    }
     

}
