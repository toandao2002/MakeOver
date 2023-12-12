using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDeadLip : ManageObjecBePulledOut
{
    public List<DeadSkin> worms;
    // Start is called before the first frame update
    void Start()
    {
        foreach (DeadSkin i in worms)
        {
            ObjecBePulledOuts.Add(i);
        }
    }
    public override void ShowCollider()
    {
        foreach (DeadSkin i in worms)
        {
            i.ShowBox();
        }
    }
    public override void HideCollider()
    {
        foreach (DeadSkin i in worms)
        {
            i.HideBox();
        }
    }

}
