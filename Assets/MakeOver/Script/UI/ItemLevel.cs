using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLevel : MonoBehaviour
{
    public int level;
    public void LoadLevel()
    {
        GameManager.Instance.data.user.level = level;
        PopupChoseLevel.Instance.Close();
        LoadingManager.Instance.LoadScene(SceneIndex.Gameplay);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
