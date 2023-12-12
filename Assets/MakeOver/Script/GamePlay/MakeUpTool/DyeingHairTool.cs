using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeingHairTool : MakeUpTool
{
    public SpriteRenderer headCream;
    public SpriteRenderer cream;
    public Texture2D tex;
    // Start is called before the first frame update
    
    public override void AnimStart()
    {
        base.AnimStart();
        var TextOrigin = Character.instance.GetBodyPart(Manager.Instance.GetCurrentAction().nameBodyPart).sprite.sprite.texture;
        var origin_Colors_Mask = TextOrigin.GetPixels();
        tex = new Texture2D(30, 30, TextureFormat.ARGB32, false);
        tex.filterMode = FilterMode.Bilinear;
        tex.wrapMode = TextureWrapMode.Clamp;
        Color[] colors = new Color[30 * 30];
        int cnt = 0;
        float rate = 0.5f;
        for (int x = 0; x < origin_Colors_Mask.Length; x++)
        {
            if (cnt < 900)
            {
                if (origin_Colors_Mask[x].a != 0 && (origin_Colors_Mask[x].r> rate || origin_Colors_Mask[x].g> rate || origin_Colors_Mask[x].b> rate))
                {
                    colors[cnt] = origin_Colors_Mask[x];
                    cnt++;
                }
            }
            else
                break;
        }
        tex.SetPixels(colors);
        tex.Apply();
        headCream.material.SetTexture("_TextureInput", tex);
        cream.material.SetTexture("_TextureInput", tex);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
