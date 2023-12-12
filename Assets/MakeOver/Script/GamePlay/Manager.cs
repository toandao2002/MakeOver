using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public List<ActionMakeOver> actions;
    public int idAtionCurrent =-1;
    public ConfigLevel configLevel;
    public int level;
    public SpriteRenderer Bgr;
    private void Awake()
    {
        Instance = this;
    }
    private void  OnEnable()
    {
        SetUpData();
        idAtionCurrent = -1;
        StartCoroutine(startGame());
        PlayScreen.Show();

        ManageAction.EndAction = null;
    }
    public void SetUpData()
    {
        level = GameManager.Instance.data.user.level - 1;
        AudioAssistant.Instance.PlayMusic("Level " + GameManager.Instance.data.user.level);
        //actions = configLevel.configActionMakeOvers[level].actionMakeOvers;
        if (actions.Count ==0 ) 
        foreach (var i in configLevel.configActionMakeOvers[level].actionMakeOvers)
        {
            actions.Add(Instantiate(i, gameObject.transform));
        }
  
       var character = Instantiate(configLevel.configActionMakeOvers[level].character);
        character.transform.position = new Vector3(0, 0, -0.1f);
    }
    IEnumerator startGame()
    {
        yield return new WaitForSeconds(1);
        NextAction();
    }
    public ActionMakeOver GetCurrentAction()
    {
        return actions[idAtionCurrent];
    }
    public void NextAction()
    {
        if (idAtionCurrent >= actions.Count-1)
        {
            Debug.Log("End Level");
            EndLevel();
            Character.instance.ShowCompleteMakeOver();
            GameManager.Instance.data.user.IncLevel();
            return;
        }
        idAtionCurrent++;
        actions[idAtionCurrent].StartAction();
       
    }
    public void EndLevel()
    {
        Camera.main.DOOrthoSize(6.8f, 2);
        Character.instance.HidedirtySmoke();
        Bgr.sprite = configLevel.configActionMakeOvers[level].Bgr;
        CallDelay(7, () => {
            AudioAssistant.Instance.CloseAllSoundLoop();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        });
    }
    public void ZoomInCam(Action call= null)
    {
        Camera.main.DOOrthoSize(5f, 2).OnComplete(() => call?.Invoke()); 
    }
    public void ZoomOutCam(Action call = null)
    {
        Camera.main.DOOrthoSize(6f, 2).OnComplete(() => call?.Invoke());;
    }
    public void CallDelay(float time,Action call)
    {
        StartCoroutine(DelayCall( time,call));
    }
    IEnumerator DelayCall(float time, Action call)
    {
        yield return new WaitForSeconds(time);
        call?.Invoke();
    }
    public List<ConfigBodyPart> GetConfigBodyPart()
    {
        return configLevel.configActionMakeOvers[level].configBodyParts;
    }
    public  ConfigBodyPart CheckHasSelectionForBodyPart(NameBodyPart nameBodyPart)
    {
        for (int i = 0; i <  configLevel.configActionMakeOvers[level].configBodyParts.Count; i++)
        {
            if (configLevel.configActionMakeOvers[level].configBodyParts[i].nameBodyPart == nameBodyPart)
            {
                return configLevel.configActionMakeOvers[level].configBodyParts[i];
            }
        }
        return null;   
    }
    public Sprite GetSpriteHasOnlyOneForChangeBodyConfig(NameBodyPart nameBodyPart)
    {
        for (int i = 0; i < configLevel.configActionMakeOvers[level].configBodyParts.Count; i++)
        {
            if (nameBodyPart == configLevel.configActionMakeOvers[level].configBodyParts[i].nameBodyPart)
            {
                return configLevel.configActionMakeOvers[level].configBodyParts[i].sprites[0];
            }
        }
        return null;
    }
}
