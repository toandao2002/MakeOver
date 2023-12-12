using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum NameBodyPart
{
    LipStick,
    LipDeadSkin,
    Eye,
    Nose,
    Eyebrow,
    EyeLash,
    EyeShadow,
    Blush,
    Worm,
    HairPre,
    FaceMask,
    PosInjection,
    MaskWithMask1,// skin face 
    HairDyeing,
    Garbage,
    Acessory,
    Tooth,
    Beard,
    MaskBeard,
    HairDefault,
    DirtyHair,
    Mask1,
    LipDefault,
    HairWashing,
    MaskCream,
    MashWashing,
}
public class Character : MonoBehaviour
{
    public static Character instance;
    
    public GameObject PosIncubation;
    public List<BodyPart> bodyParts;
    public List<SpriteRenderer> maskNormals;// use for actions dont use drawline, only hide after implement action
    public List<BodyPart> MaskUseSrpiteMask;// use for actions dont use drawline, only hide after implement action
    public Material mtrHighLight;
    public Material mtrHighLightNormal;
    public Material MtrDraw;
    public float durationHighLight; 
    int currentMaskTop;
    [Header("Body Part")]
    public GameObject defaultFace;
    public BodyPart lipStick;
    public BodyPart lipDefault;
    public BodyPart hairPre;
    public BodyPart hairDyeing;
    public BodyPart hairDefault;
    public BodyPart EyeShadow;
    public BodyPart EyeLash;
    public BodyPart eyeBrow;
    public BodyPart faceMask;
    public BodyPart blush;
    public BodyPart eye;
    public BodyPart posInjection;
    public BodyPart nose;
    public BodyPart acessory;
    public BodyPart tooth;
    public BodyPart beard;
    public BodyPart dirtyPart;
    public BodyPart maskCream;
    public BodyPart maskWashing;
    [Header("BodyPart Mask With Sprite mask")]
    public BodyPart maskWithMask1;

    [Header("Body Part Mask normal to Remove")]
    public BodyPart maskBeard;
    public BodyPart mask1;

    [Header("Manage obj")]
    public ManageObjecBePulledOut manageObjecBePulledOut;
    //ManageObjecBePulledOut////////////////
    public ManageWorm manageWorm; 
    public ManageDeadLip manageDeadLip;
    public ManageAcne manageAcne;
    public ManageCream manageCream;
    public ManageCream manageCreamMask;// wash face
    ////////////////////////////////////////
    public ManageGarbage manageGarbage;
    
    ManageSoft CurrentManageSoft;
    public ManageSoft manageSoftHair;
    public ManageSoft manageSoftTooth;

    // hint
    IHintHighLight hintHighLight;

    [Header("Clothes")]
    public SpriteRenderer shirtDirty;
    public SpriteRenderer shirtBeaty;

    public MySpriteMask spriteMask;

    public GameObject dirtySmoke;
    private void Awake()
    {
        instance = this;
        currentMaskTop =0;
    }
    private void OnEnable()
    {
         foreach(var i in maskNormals)
        {
            i.gameObject.SetActive(false);
        }
    }
    public BodyPart GetBodyPart(NameBodyPart nameBodyPart)
    {
        switch (nameBodyPart)
        {
            case NameBodyPart.LipStick:
                return lipStick;
            case NameBodyPart.HairPre:
                return hairPre;
            case NameBodyPart.EyeShadow:
                return EyeShadow;
            case NameBodyPart.EyeLash:
                return EyeLash;
            case NameBodyPart.FaceMask:
                return faceMask;
            case NameBodyPart.Eyebrow:
                return eyeBrow;
            case NameBodyPart.Blush:
                return blush;
            case NameBodyPart.Eye:
                return eye;
            case NameBodyPart.PosInjection:
                return posInjection;
            case NameBodyPart.MaskWithMask1:
                return maskWithMask1;
            case NameBodyPart.Nose:
                return nose;
            case NameBodyPart.HairDyeing:
                return hairDyeing;
            case NameBodyPart.Acessory:
                return acessory;
            case NameBodyPart.Tooth:
                return tooth;
            case NameBodyPart.Beard:
                return beard;
            case NameBodyPart.MaskBeard:
                return maskBeard;
            case NameBodyPart.HairDefault:
                return hairDefault;
            case NameBodyPart.DirtyHair:
                return dirtyPart;
            case NameBodyPart.Mask1:
                return mask1;
            case NameBodyPart.LipDefault:
                return lipDefault;
            case NameBodyPart.HairWashing: // same with hair pre
                return hairPre;
            case NameBodyPart.MaskCream:
                return maskCream;
            case NameBodyPart.MashWashing:
                return maskWashing;
        }
        return null;
            
    }
    SpriteRenderer maskcurRemove;
    public void PopMask()
    {
        float duratoin = 1;
        if (currentMaskTop - 1 >= 0)
        {
            maskcurRemove = maskNormals[currentMaskTop - 1];
            maskcurRemove.DOFade(0, duratoin).OnComplete(() => {
                maskcurRemove.gameObject.SetActive(false);
            });
        }
        currentMaskTop--;
        
    }
    public void ShowMask()
    {
        float duratoin = 1;
        if(currentMaskTop ==0 ) 
            defaultFace.SetActive(false);    
        if (currentMaskTop < maskNormals.Count)
        {
            maskcurRemove = maskNormals[currentMaskTop ];
            maskcurRemove.gameObject.SetActive(true);
            maskcurRemove.DOFade(1, duratoin).OnComplete(() => {
             
            });
        }
        currentMaskTop++;

    }
    public ManageObjecBePulledOut GetCurrentManageObjectBePulledOut()
    {
        return manageObjecBePulledOut;
    }
    public void SetManageObjecBePulledOutIsWorm()
    {
        manageObjecBePulledOut = manageWorm;
        hintHighLight = manageWorm;
    }
    public void SetManageObjecBePulledOutIsDeadLip()
    {
        manageObjecBePulledOut = manageDeadLip;
        hintHighLight = manageDeadLip;
    }
    public void SetManageObjecBePulledOutIsCream()
    {
        manageObjecBePulledOut = manageCream;
        hintHighLight = manageCream ;
    }
    public void SetHintGarbage()
    {
        hintHighLight = manageGarbage;
    }
    public void SetManageObjecBePulledOutIsAcne()
    {
        manageObjecBePulledOut = manageAcne;
        hintHighLight = manageAcne;
    }
    public ManageSoft GetMangageSoftCurrent()
    {
        return CurrentManageSoft;
    }
    public ManageSoft SetMangageSoftHair()
    {
        CurrentManageSoft = manageSoftHair;
        hintHighLight = manageSoftHair;
        return manageSoftHair;
    }

    public ManageSoft SetMangageSoftTooth()
    {
        CurrentManageSoft = manageSoftTooth;
        hintHighLight = manageSoftTooth;

        return manageSoftTooth;
    }
    public ManageGarbage GetMangageGarbage()
    {
        return manageGarbage;
    }
    public IHintHighLight GetCurrentShowHint()
    {
        return hintHighLight;
    }
    public void SetHintiHghLight(IHintHighLight hint)
    {
        hintHighLight = hint;
    }
    public void TurnOnSpriteMask()// only unse for cream or st be hide same time
    {
        spriteMask.StartSpriteMask();
    }
    public void TurnOffSpriteMask()
    {
        spriteMask.StopSpritrMask();
    }
    public void ShowCompleteMakeOver()
    {
        float duration = 1;
        shirtDirty.DOFade(0, duration);
        shirtBeaty.gameObject.SetActive(true);
        shirtBeaty.DOFade(1, duration).From(0);
    }
    public void HidedirtySmoke()
    {
        dirtySmoke.SetActive(false);
    }
}
