using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NameAction
{
    Action0,
    Action1,
    Actiin2,
    Action3,
}
public abstract class ActionMakeOver : MonoBehaviour
{
    public MakeUpTool CurMakeUpTool;
    public bool endAction;
    public NameAction nameAction;
    List<MakeUpTool> makeUpTools;
    public NameBodyPart nameBodyPart;
    public bool has3Item;
    public bool IsMakeUpToolBeUsed()
    {
        return CurMakeUpTool.GetStateUse();
    }

    public virtual void StartAction() {
        ManageAction.EndAction += EndAction;
    }
    public virtual void NextChildAction() {
        CurMakeUpTool.AnimOut();
    }
    public void endAtionImediately()
    {
        has3Item = false;
        EndAction();
    }
    public virtual void EndAction() {
        ManageAction.EndAction -= EndAction;
        if (has3Item )
        {
            StartAction(); 
            return;
        }
        
        StartCoroutine(NextAction());
        
    }
    public IEnumerator  NextAction()
    {
        yield return new WaitForSeconds(1);
        Manager.Instance.NextAction();
    }
    void CheckOutAllMakeUpTool()
    {
        for (int i =0;i <makeUpTools.Count; i++)
        {
            if (!makeUpTools[i].beOut)
            {
                makeUpTools[i].AnimOut();
            }
        }
    }
    public void SetSpirteForBodyPart(Sprite spr) // only use for action has selection 
    {
        Character.instance.GetBodyPart(nameBodyPart).SetSprite(spr);
    }
    public void SetUpDrawLine(int sizeDraw, NameBodyPart nameBodyPart, StateDraw stateDraw)
    {
        AreaDraw.instance.bodyPart = Character.instance.GetBodyPart(nameBodyPart);
        AreaDraw.instance.makeUpTool = CurMakeUpTool;
        AreaDraw.instance.erSize = sizeDraw;
        AreaDraw.instance.stateDraw = stateDraw;
        AreaDraw.instance.FirstSetUpData();
    }
    private bool firstShow = true; 
    public ConfigBodyPart ShowSelection(NameBodyPart nbd)
    {
        var configBodypart = Manager.Instance.CheckHasSelectionForBodyPart(nbd);
        if (configBodypart != null && configBodypart.sprites.Count >1)
        {
            has3Item = true;
            PopupShowSelection.Instance.SetdataAndAddActionForBtnItem(configBodypart);
            PopupShowSelection.Instance.ShowPopUp(!firstShow);
            firstShow = false;
            return configBodypart;
        }
        return configBodypart;
    }
    public MakeUpTool SpanwMakeUpTool(MakeUpTool mkt, GameObject par)
    {
        var mktSp = Instantiate(mkt, par.transform);
        mktSp.gameObject.SetActive(false);
        return mktSp;
    }
}
