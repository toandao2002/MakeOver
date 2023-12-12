using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    public void LoadSceneGamePlay()
    {
        LoadingManager.Instance.LoadScene(SceneIndex.Gameplay);
    }
}
