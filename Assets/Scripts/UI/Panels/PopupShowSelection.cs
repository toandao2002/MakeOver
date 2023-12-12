using System.Collections.Generic;

public class PopupShowSelection : MyUiPanel
{
    public static PopupShowSelection Instance { get; private set; }
    public List<ItemBodyPart> buttonItems;
    public ItemBodyPart ItemBodyPartPrefab;
    public HCButton next;
    private void Awake()
    {
        Instance = this;
    }
    public void ShowPopUp(bool canNext = false){
        Show();
        next.gameObject.SetActive(canNext);

    }
    public void Next()
    {
        Manager.Instance.GetCurrentAction().endAtionImediately();
        ManageAction.EndChoseItemBodyPart = null;
        Hide(); 
    }
    public void SetdataAndAddActionForBtnItem( ConfigBodyPart configBodyParts)
    {
        for (int i = 0;i <configBodyParts.sprites.Count; i++)
        {
            buttonItems[i].SetIcon(configBodyParts.sprites[i]);
            buttonItems[i].id = i;
            buttonItems[i].gameObject.SetActive(true) ;
        } 
    }
    public override void Hide()
    {
        base.Hide();
        /*for (int i = 0; i < buttonItems.Count; i++)
        {
            
            buttonItems[i].gameObject.SetActive(false);
        }*/
    }

}