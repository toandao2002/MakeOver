
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "Asset Processor Setting", menuName = "HyperCat/Asset Processor Setting")]
public class HCAssetProcessorSetting : SerializedScriptableObject
{
#if UNITY_EDITOR
    public DefaultAsset[] automatePaths;
    
    [Space] 
    public AssetLabel[] spriteLabels;
    public AssetLabel[] animationModelLabels;
    [ReadOnly] public string importedLabel = "Imported";
#endif
}

[Serializable]
public class AssetLabel
{
    public string[] labelArray;
}

public static class AssetLabelUtils
{
    public static bool HasLabel(this string[] label, AssetLabel[] assetLabels)
    {
        foreach (var assetLabel in assetLabels)
        {
            if (label.HasLabel(assetLabel))
                return true;
        }

        return false;
    }

    public static bool HasLabel(this string[] label, AssetLabel assetLabel)
    {
        bool containLabel = true;

        foreach (var l in assetLabel.labelArray)
        {
            if (!label.HasLabel(l))
            {
                containLabel = false;
                break;
            }
        }

        return containLabel;
    }

    public static bool HasLabel(this string[] label, string l) => label.Contains(l, StringComparer.OrdinalIgnoreCase);
}