using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Moisturize : MonoBehaviour
{
    public GameObject cucumber;
    public GameObject lip;
    public bool finish;
    public float disMove;
    public void ActionMoisturize(Vector3 posEye,Vector3 posLip, float duration)
    {
        StartCoroutine(ActionDetail(  posEye,   posLip,   duration));
    }
    IEnumerator ActionDetail(Vector3 posEye, Vector3 posLip, float duration)
    {
        cucumber.SetActive(true);
        cucumber.transform.DOMove(posEye, duration).From(posEye+new Vector3(0,disMove,0));
        yield return new WaitForSeconds(1);
        lip.SetActive(true);
        lip.transform.DOMove(posLip, duration).From(posEye + new Vector3(disMove,0, 0));
        yield return new WaitForSeconds(3);


      

        cucumber.transform.DOMove(posEye - new Vector3(0, disMove, 0), duration).OnComplete(()=> {
            cucumber.SetActive(false);
        });
        lip.transform.DOMove(posLip - new Vector3(disMove, 0, 0), 2).OnComplete(() => {
            lip.SetActive(false);
            finish = true;
        });
        Character.instance.GetBodyPart(NameBodyPart.LipDefault).SetSpriteAnim(Manager.Instance.GetSpriteHasOnlyOneForChangeBodyConfig(NameBodyPart.LipDefault));

    }
}
