using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "ConfigLevelDetail", menuName = "Data/ConfigLevelDetail")]
public class ConfigActionMakeOver :ScriptableObject
{
    public GameObject character;
    public List<ConfigBodyPart> configBodyParts;// if has data in it ,when play game, player can select 1 in 3 type assset for makeUp
    public List<ActionMakeOver> actionMakeOvers;
    public Sprite Bgr;
 
}
[System.Serializable]
public class ConfigBodyPart
{
    public NameBodyPart nameBodyPart;
    public List<Sprite> sprites;
}
 
 
 