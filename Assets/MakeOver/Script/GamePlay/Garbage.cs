using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    public BoxCollider2D box;
    public bool BeOut;
    public SpriteRenderer sprite;
    public Material mtrPre;
    private void Start()
    {
        mtrPre = sprite.material;
    }
    public void Drop()
    {
        float duration = 1;
        gameObject.transform.DOMoveY(-0.5f + gameObject.transform.position.y, duration).OnComplete(() => {
            gameObject.SetActive(false);
        });
    }
    public void DropByDir()
    {
        float duration = 1;
        gameObject.transform.DOMove(gameObject.transform.position+gameObject.transform.position.normalized, duration).OnComplete(() => {
            gameObject.SetActive(false);
        });
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ToolPull")&&Manager.Instance.GetCurrentAction().IsMakeUpToolBeUsed())
        {
            if (!BeOut)
            {
                BeOut = true;
                if (Character.instance.GetMangageGarbage().ChecBeAllOut())
                {
                    Character.instance.GetMangageGarbage().DropGarbageFolowDir();
                    Manager.Instance.GetCurrentAction().NextChildAction();
                }
            }
           
        }
    }
    
    public bool CheckBeOut()
    {
        return BeOut;
    }
    public void HideBox()
    {
        box.enabled = false;
    }
    public void ShowBox()
    {
        box.enabled = true;
        StartCoroutine(ChangeMtr());
    }
    public void ChangeMaterialHighLight(Material mtr)
    {
        sprite.material = mtr;
    }
    public void ChangeMaterialNormal()
    {
        sprite.material = mtrPre;
    }
    IEnumerator ChangeMtr()
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
