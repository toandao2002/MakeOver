using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BodyPart : MonoBehaviour, IHintHighLight
{
    public NameBodyPart nameBodyPart;
    public bool finishMakeUp;
    public SpriteRenderer sprite;
    public BoxCollider2D col;
    public bool FirstHide;
    public bool DoneAction;
    public Material mtrPre;
    public Material mtrDraw;
    public Material mtrCur;
    public Material mtrHint;
    public bool drawing; // use when drawing and change state highlight to state draw line, otherwise change state default of material.
    public bool canStopDrawLine;
    private void Awake()
    {
        ChangeStateCollider(false);
        if (FirstHide) sprite.gameObject.SetActive(false);
        mtrHint = Instantiate(Character.instance.mtrHighLight);
    }
    private void Start()
    {
        mtrPre = sprite.material;
    }
    public SpriteRenderer GetSpriteRenderer()
    {
        sprite.gameObject.SetActive(true);
        ChangeStateCollider(true);
        ShowHint();
        Character.instance.SetHintiHghLight(this);
        return sprite;
    }
    public void ChangeStateCollider(bool val)
    {
        col. enabled = val;
    }
    public Vector3 GetPosition()
    {
        return sprite.gameObject.transform.position;
    }
    public virtual void SetSprite(Sprite spr)
    {
        sprite.sprite= spr;
        DoneAction = true;
    }
    public void HideSprite()
    {
        sprite.gameObject.SetActive(false);
    }
    public virtual void SetSpriteAnim(Sprite spr)
    {
        if (spr == null)
        {
            DoneAction = true;
            Debug.LogError("Miss Asset");
            return;
        }
        sprite.sprite = spr;
        sprite.DOFade(1, 1).From(0).OnComplete(()=> { 
            DoneAction = true;
        }); 
    }
    public NameBodyPart GetNameBodyPart()
    {
        return nameBodyPart;
    }
    public void HideAnimFade()
    {
        sprite.DOFade(0, 1);
    }
    public virtual void ChangeMaterialHighLight(Material mtr)
    {
        sprite.material = mtr;
    }
    public virtual void ChangeMaterialNormal()
    {
        sprite.material = mtrPre;
    }
    public virtual void ChangeMaterialDraw()
    {
        sprite.material = mtrDraw;
    }
    IEnumerator ChangeMtr()
    {
       
        ChangeMaterialHighLight(mtrHint);
      
        yield return new WaitForSeconds(Character.instance.durationHighLight);
        if (drawing)
        {
            ChangeMaterialDraw();
        }
        else
            ChangeMaterialNormal();
    }
    public void UpdateTextureInput()
    {
        mtrHint.SetTexture("_TextureInput", AreaDraw.instance.texture);
    }
    public void ShowHint()
    {
        StartCoroutine(ChangeMtr());
    }
}
