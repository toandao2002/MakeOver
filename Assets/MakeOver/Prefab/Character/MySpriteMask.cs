 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MySpriteMask : MonoBehaviour
{
    public SpriteMask sprM;
    public SpriteRenderer spr;
    public int count;
    // Start is called before the first frame update
    void Start()
    {

      
    }
  
    public void StartSpriteMask()
    {
        sprM.enabled = true;
        enabled = true;
    }
    public void StopSpritrMask()
    {
        sprM.enabled = false;
        enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        count++;
        if (count % 5 == 0)
        {
            sprM.sprite = spr.sprite;
        }
        if (count >= 200000) count = 0;
    }
}
