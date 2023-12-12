using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ConfigLevel", menuName = "Data/ConfigLevel")]
public class ConfigLevel : ScriptableObject
{
    public List<ConfigActionMakeOver> configActionMakeOvers;
}
