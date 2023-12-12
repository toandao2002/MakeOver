using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum StateSoft
{
    DontShow,
    Soft,
    SoftWater,
    End,

}
public class Soft : MonoBehaviour, IBoxCollider
{
    public Sprite soft;
    public Sprite softWater;
    public float duration;
    public BoxCollider box;
    public SpriteRenderer sprite;
    public SpriteRenderer spriteHighLight;
    public Material mtrPre;
    private Color[] origin_Colors;
    public static bool isPlaySound;
    private void OnEnable()
    {
        isPlaySound = false;
        box.enabled = false;
        mtrPre = spriteHighLight.material;
        spriteHighLight.sprite = soft;
        //ChangeSprite(spriteHighLight, soft); 
    }
    public void ChangeSprite(SpriteRenderer sprite_renderer, Sprite spr)
    {
        var tex = spr.texture;
        float width = tex.width;
        float height = tex.height;
        Texture2D texture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Bilinear;
        texture.wrapMode = TextureWrapMode.Clamp;
        origin_Colors = tex.GetPixels();
        texture.name = sprite_renderer.name;
        for (int i = 0; i < origin_Colors.Length; i++)
        {
            if (!(origin_Colors[i].a == 0))  
             
            {
                origin_Colors[i].a = 0.02f;
            }
        }
        texture.SetPixels(origin_Colors);
        texture.Apply();
        sprite_renderer.sprite = Sprite.Create(texture, sprite_renderer.sprite.rect, new Vector2(0.5f, 0.5f));
    }
    public StateSoft stateSoft = StateSoft.DontShow;
    public bool CheckState(StateSoft st)
    {
        return stateSoft == st;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Manager.Instance.GetCurrentAction().IsMakeUpToolBeUsed()) return; // if dont touch screen or makeup tool dont be used
        if (!isPlaySound) AudioAssistant.Instance.PlaySoundLoop(TypeSound.BubblePop);
        if (stateSoft == StateSoft.DontShow && other.CompareTag("Bottle"))
        {
            sprite.gameObject.SetActive(true);
            Manager.Instance.GetCurrentAction().CurMakeUpTool.ActionMakeUpTool();
            sprite.sprite =  soft;
            spriteHighLight.sprite =  soft;
            sprite.transform.DOScale(1f, duration).From(0.4f);
            stateSoft = StateSoft.Soft; 
            if (Character.instance.GetMangageSoftCurrent().ChecOutkState(StateSoft.DontShow))
            {
                Manager.Instance.GetCurrentAction().NextChildAction();
            }
           
        }
        if (stateSoft == StateSoft.Soft &&other.CompareTag("Comb")){

            stateSoft = StateSoft.SoftWater;
     
            sprite.transform.DOScale(0.3f, 0.2f).OnComplete(() => {
               
            });
            sprite.sprite = softWater ;
            spriteHighLight.sprite = softWater ;
            sprite.transform.DOScale(1, duration).From(0.2f);
            if (Character.instance.GetMangageSoftCurrent().ChecOutkState(StateSoft.Soft))
            {
                Manager.Instance.GetCurrentAction().NextChildAction();
            }
            AudioAssistant.Shot(TypeSound.BrushComb);
        }
        if (stateSoft == StateSoft.SoftWater&& other.CompareTag("Water"))
        {
            Drop();
            if (Character.instance.GetMangageSoftCurrent().ChecOutkState(StateSoft.SoftWater))
            {
                Manager.Instance.GetCurrentAction().NextChildAction();
                AudioAssistant.Instance.StopSoundLoop(TypeSound.BubblePop);

            }
        }
    }
    public void Drop()
    {
        stateSoft = StateSoft.End;
        spriteHighLight.gameObject.SetActive(false);
        sprite.transform.DOMoveY(  sprite.transform.position.y - Random.Range(0.5f,2f), duration).OnComplete(() => {
            gameObject.SetActive(false);
        });
        sprite.GetComponent<SpriteRenderer>().DOFade(0, duration);
    }
    public void HideBox()
    {
        box.enabled = false;
    }

    public void ShowBox()
    {
        box.enabled = true;
        spriteHighLight.gameObject.SetActive(true);
        StartCoroutine(ChangeMtr());
    }
    public void ChangeMaterialHighLight(Material mtr)
    {
        spriteHighLight.material = mtr;
    }
    public void ChangeMaterialNormal()
    {
        spriteHighLight.material = mtrPre;
    }
    IEnumerator ChangeMtr()
    {
        spriteHighLight.gameObject.SetActive(true);
        ChangeMaterialHighLight(Character.instance.mtrHighLightNormal);
        yield return new WaitForSeconds(Character.instance.durationHighLight);
        ChangeMaterialNormal();
        spriteHighLight.gameObject.SetActive(true);
        spriteHighLight.gameObject.SetActive(false);
    }
    public void ShowHint()
    {
        if (enabled == true)
            StartCoroutine(ChangeMtr());
    }
}
