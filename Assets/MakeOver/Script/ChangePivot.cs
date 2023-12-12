using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


#if UNITY_EDITOR

using UnityEditor;

public class ChangePivot : MonoBehaviour
{
    public Sprite sprite;
    public GameObject pos;
    [Button]
    public void Change()
    {
        var sr = GetComponent<SpriteRenderer>();
        var sprite = sr.sprite;

        string path = AssetDatabase.GetAssetPath(sr.sprite.texture);
        TextureImporter ti = (TextureImporter)AssetImporter.GetAtPath(path);
        Debug.Log(path);// return path of sprite
        var curPos = transform.localPosition;
        Debug.Log(transform.localPosition);
        var srSize = sprite.rect.size;
        Debug.Log(srSize);
        var srSizeUnit = srSize / sprite.pixelsPerUnit;
        Debug.Log(srSizeUnit);
        var ratioX = curPos.x / srSizeUnit.x;
        var ratioY = curPos.y / srSizeUnit.y;
        Vector2 newPivot = new Vector2(0.5f, 0.5f) - new Vector2(ratioX, ratioY);
        ti.spritePivot = newPivot;
        TextureImporterSettings texSettings = new TextureImporterSettings();
        ti.ReadTextureSettings(texSettings);
        texSettings.spriteAlignment = (int)SpriteAlignment.Custom;
        ti.SetTextureSettings(texSettings);
        ti.SaveAndReimport();
        transform.localPosition = Vector3.zero;
    }
}
#endif