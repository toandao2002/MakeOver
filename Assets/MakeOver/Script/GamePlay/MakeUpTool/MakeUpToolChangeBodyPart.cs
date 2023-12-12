using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MakeUpToolChangeBodyPart : MakeUpTool
{
    public Sprite spriteData;
    public SpriteRenderer SR;
    public bool HasAnimChange;
    public BoxCollider2D box;
    public GameObject tool;
    Vector2 pivot;
    public virtual void OnEnable()
    {
        box.size = new Vector2( SR.sprite.bounds.size.x , SR.sprite.bounds.size.y  >=2 ? SR.sprite.bounds.size.y /2 : SR.sprite.bounds.size.y ) ;
      
    }
    bool done = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("PosCorrect") && beUsing&& !done)
        {

            done = true;
            if (HasAnimChange)
            {
                tool.transform.DOMove(Character.instance.GetBodyPart(NameBodyPart.Nose).sprite.gameObject.transform.position  , 1).OnComplete(() => {

                    ani.applyRootMotion = true;
                    tool.gameObject.transform.position = Character.instance.GetBodyPart(NameBodyPart.Nose).sprite.gameObject.transform.position;
                    PlayAnimChange();
                    tool.gameObject.transform.position = Character.instance.GetBodyPart(NameBodyPart.Nose).sprite.gameObject.transform.position;
                });
             
            }
            else
            {
                var bodyPart = Character.instance.GetBodyPart(Manager.Instance.GetCurrentAction().nameBodyPart);
                bodyPart.SetSprite(spriteData);
                StartCoroutine(DeLayCallBool(bodyPart, ()=>{ Manager.Instance.GetCurrentAction().NextChildAction(); }));
                SR.enabled = false;
                AudioAssistant.Shot(TypeSound.Poof);
               // Character.instance.GetBodyPart(Manager.Instance.GetCurrentAction().nameBodyPart).SetSprite(spriteData);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PosCorrect") && beUsing&& !done)
        {

            done = true;
            if (HasAnimChange)
            {
                tool.transform.DOMove(Character.instance.GetBodyPart(NameBodyPart.Nose).sprite.gameObject.transform.position, 1).OnComplete(() => {

                    ani.applyRootMotion = true;
                    tool.gameObject.transform.position = Character.instance.GetBodyPart(NameBodyPart.Nose).sprite.gameObject.transform.position;
                    PlayAnimChange();
                    tool.gameObject.transform.position = Character.instance.GetBodyPart(NameBodyPart.Nose).sprite.gameObject.transform.position;
                });

            }
            else
            {
                var bodyPart = Character.instance.GetBodyPart(Manager.Instance.GetCurrentAction().nameBodyPart);
                bodyPart.SetSprite(spriteData);
                StartCoroutine(DeLayCallBool(bodyPart, () => { Manager.Instance.GetCurrentAction().NextChildAction(); }));
                SR.enabled = false;
                AudioAssistant.Shot(TypeSound.Poof);
                // Character.instance.GetBodyPart(Manager.Instance.GetCurrentAction().nameBodyPart).SetSprite(spriteData);
            }
        }
    }

    public void PlayAnimChange()
    {
        DoneAnim = false;
        ani.enabled = true;
        ani.Play("ActionChange",-1);
        
        StartCoroutine(delay());
    }
    public IEnumerator delay()
    {
        yield return new WaitUntil(() => DoneAnim);
        Manager.Instance.GetCurrentAction().NextChildAction();
        Character.instance.GetBodyPart(Manager.Instance.GetCurrentAction().nameBodyPart).SetSprite(spriteData);
    }
    public void SetDataSprite(Sprite spr)
    {
        spriteData = spr;
        if(!HasAnimChange)   
            SR.sprite = spr;
        pivot = spriteData.pivot;
        float per = spriteData.pixelsPerUnit;
   
        Vector3 dis = ((Vector3)pivot /per -  (spriteData.bounds.size/2 ));
         

        SR.transform.position +=  dis;
    }
}
