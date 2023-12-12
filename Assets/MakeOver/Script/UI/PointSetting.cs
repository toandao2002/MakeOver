using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointSetting : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform PointS;
    public Transform PointE;
    public bool Sound;
    public float dis;
    public Image bar;
    public bool isHold;
    private void OnEnable()
    {

        if (Sound)
            bar.fillAmount = GameManager.Instance.data.setting.soundVolume;
        else
        {
            bar.fillAmount = GameManager.Instance.data.setting.musicVolume;
        }
        dis = -(PointS.position.x - PointE.position.x);
        gameObject.transform.position = new Vector2(bar.fillAmount * dis + PointS.position.x, gameObject.transform.position.y);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isHold = true;
    }

 
    public void OnPointerUp(PointerEventData eventData)
    {
        isHold = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHold)
        {
            Vector2 pos = Input.mousePosition;
            if (pos.x > PointE.position.x)
            {
                pos.x = PointE.position.x;
            }
            if (pos.x < PointS.position.x)
            {
                pos.x = PointS.position.x;
            }
            gameObject.transform.position = new Vector2(pos.x, gameObject.transform.position.y);
            float ra = (pos.x - PointS.position.x) / dis;
            if (Sound)
                GameManager.Instance.data.setting.soundVolume = ra;
            else
            {
                GameManager.Instance.data.setting.musicVolume = ra;
            }
            EventGlobalManager.Instance.OnUpdateSetting.Dispatch();
            bar.fillAmount = ra;
        }   
    }
}
