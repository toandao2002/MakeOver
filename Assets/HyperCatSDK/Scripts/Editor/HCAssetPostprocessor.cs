
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Castle.Core.Internal;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class HCAssetPostprocessor : AssetPostprocessor
{
    private static HCAssetProcessorSetting _assetProcessorSetting;

    public static HCAssetProcessorSetting AssetProcessorSetting
    {
        get
        {
            if (!_assetProcessorSetting)
                _assetProcessorSetting = HCTools.GetConfig<HCAssetProcessorSetting>("Assets/HyperCatSDK");
            
            return _assetProcessorSetting;
        }
    }

    #region SetLabel

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        foreach (var asset in importedAssets)
        {
            if (CheckAutomatePath(asset))
                ProcessAssetLabels(asset);
        }

        foreach (var asset in movedAssets)
        {
            if (CheckAutomatePath(asset))
                ProcessAssetLabels(asset);
        }
    }
    
    
    public static void ProcessAssetLabels(string assetPath)
    {
        List<string> labels = new List<string>();

        string fileExtension = Path.GetExtension(assetPath).Replace(".", string.Empty);
        var path = assetPath;

        var directoryInfo = Directory.GetParent(path);
        
        while (directoryInfo != null && directoryInfo.Name != "Assets")
        {
            var folderName = directoryInfo.Name;
            labels.Add(folderName.ToLower());
            directoryInfo = directoryInfo.Parent;
        }

        labels.Reverse();
        labels.Add(fileExtension);

        var obj = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
        if (obj)
        {
            if (labels.Count == 0)
            {
                AssetDatabase.ClearLabels(obj);
                return;
            }

            var oldLabels = AssetDatabase.GetLabels(obj);
            if (oldLabels.HasLabel(AssetProcessorSetting.importedLabel))
                labels.Add(AssetProcessorSetting.importedLabel);
                    
            var labelsArray = labels.ToArray();
            if (HaveLabelsChanged(oldLabels, labelsArray))
            {
                AssetDatabase.SetLabels(obj, labelsArray);
            }
        }
    }
    
    private static bool HaveLabelsChanged(string[] oldLabels, string[] newLabels)
    {
        foreach (var oldLabel in oldLabels)
        {
            if (!newLabels.Contains(oldLabel))
                return true;
        }
        
        foreach (var newLabel in newLabels)
        {
            if (!oldLabels.Contains(newLabel))
                return true;
        }

        return false;
    }

    static bool CheckAutomatePath(string path)
    {
        if (!AssetProcessorSetting)
            return false;
        
        var automatePaths = AssetProcessorSetting.automatePaths;

        if (!automatePaths.CheckIsNullOrEmpty() && !path.IsNullOrEmpty())
        {
            foreach (var automatePath in automatePaths)
            {
                if (path.Contains(AssetDatabase.GetAssetPath(automatePath)))
                    return true;
            }
        }
        
        return false;
    }
    
    #endregion

    #region Texture Import Setting

    private void OnPreprocessTexture()
    {
        var folderPath = AssetDatabase.GetAssetPath(assetImporter)
            .Remove(assetPath.LastIndexOf("/", StringComparison.Ordinal));
        
        if (!CheckAutomatePath(folderPath))
            return;
        
        var labels = AssetDatabase.GetLabels(assetImporter);

        if (labels.Length == 0)
            return;

        if (labels.HasLabel(AssetProcessorSetting.importedLabel))
            return;

        if (labels.HasLabel(AssetProcessorSetting.spriteLabels))
        {
            var textureImporter = assetImporter as TextureImporter;
            if (!textureImporter)
                return;

            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.spriteImportMode = SpriteImportMode.Single;
            textureImporter.alphaIsTransparency = true;
            textureImporter.wrapMode = TextureWrapMode.Clamp;
            
            FinishImport(assetImporter);
        }
    }

    #endregion

    #region Model Import Setting

    private void OnPreprocessModel()
    {
        var folderPath = AssetDatabase.GetAssetPath(assetImporter)
            .Remove(assetPath.LastIndexOf("/", StringComparison.Ordinal));
        
        if (!CheckAutomatePath(folderPath))
            return;
        
        var labels = AssetDatabase.GetLabels(assetImporter);

        if (labels.Length == 0)
            return;

        if (labels.HasLabel(AssetProcessorSetting.importedLabel))
            return;

        if (labels.HasLabel(AssetProcessorSetting.animationModelLabels))
        {
            ModelImporter modelImporter = assetImporter as ModelImporter;
            if (!modelImporter)
                return;
            
            modelImporter.materialImportMode = ModelImporterMaterialImportMode.None;
            modelImporter.importCameras = false;
            modelImporter.importLights = false;
            modelImporter.importConstraints = false;
            modelImporter.importVisibility = false;
            modelImporter.importBlendShapes = false;
            modelImporter.SaveAndReimport();
        }
    }

    private void OnPostprocessModel(GameObject model)
    {
        var folderPath = AssetDatabase.GetAssetPath(assetImporter)
            .Remove(assetPath.LastIndexOf("/", StringComparison.Ordinal));
        
        if (!CheckAutomatePath(folderPath))
            return;
        
        var labels = AssetDatabase.GetLabels(assetImporter);

        if (labels.HasLabel(AssetProcessorSetting.animationModelLabels))
        {
            // clean up skinned meshes
            foreach (var skinnedMeshRenderer in model.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (skinnedMeshRenderer.sharedMesh is var sharedMesh)
                    if (sharedMesh)
                        Object.DestroyImmediate(sharedMesh);

                Object.DestroyImmediate(skinnedMeshRenderer);
            }

            // clean up meshes from mesh filters
            foreach (var meshFilter in model.GetComponentsInChildren<MeshFilter>())
            {
                if (meshFilter.sharedMesh is var sharedMesh)
                    if (sharedMesh)
                        Object.DestroyImmediate(sharedMesh);

                Object.DestroyImmediate(meshFilter);
            }

            // clean up all child objects in the imported model
            foreach (Transform child in model.transform)
            {
                if (child)
                    Object.DestroyImmediate(child.gameObject);
            }
        }
    }

    #endregion
    
    void FinishImport(Object obj)
    {
        var labels = AssetDatabase.GetLabels(obj).ToList();
        labels.Add(AssetProcessorSetting.importedLabel);
        AssetDatabase.SetLabels(obj, labels.ToArray());
    }
}
