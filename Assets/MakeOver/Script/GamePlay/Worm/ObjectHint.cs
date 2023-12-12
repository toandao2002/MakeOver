using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHint : MonoBehaviour
{

    public SpriteRenderer spriteHighLight;
    public Material mtrPre;
    public virtual void Start()
    {
        mtrPre = spriteHighLight.material;
    }
    public void ChangeMaterialHighLight(Material mtr)
    {
        spriteHighLight.material = mtr;
    }
    public void ChangeMaterialNormal()
    {
        spriteHighLight.material = mtrPre;
    }
    public IEnumerator ChangeMtr()
    {
        ChangeMaterialHighLight(Character.instance.mtrHighLight);
        yield return new WaitForSeconds(Character.instance.durationHighLight);
        ChangeMaterialNormal();
    }
    public void ShowHint()
    {
        StartCoroutine(ChangeMtr());
    }
}
