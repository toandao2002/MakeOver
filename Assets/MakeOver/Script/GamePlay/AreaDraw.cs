using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateDraw
{
    Show,
    Hide,
}
public class AreaDraw : MonoBehaviour
{
    public static AreaDraw instance;
    public StateDraw stateDraw;
   
    public MakeUpTool makeUpTool;
    public BodyPart bodyPart;
    [SerializeField] public Texture2D texture;
    private Color[] colors;// contain inf of pixel after draw
    private Color[] origin_Colors_Mask;// inf of pixel at first
    private Color[] origin_Colors;// inf of pixel at first
    private int[] visited;// if pixel has update  = 1, otherwise = 0
    public int countPixelDone;
    //public SpriteRenderer sprite_renderer;
    int width, height, WOrigin,HOrigin;
    private HashSet<int> index_colored = new HashSet<int>(); // contain inf of pixel Has drawn
    RaycastHit2D hit;
    public string MyTag;

    public int erSize; // dimension of  draw line
    public Vector2Int lastPos;
    public bool drawing = false;
    private bool stop_drawing;
    public bool CompleteDrawing;
    public int countFrame;
    float percentDraw;
    int mask;
    public int FrequencyPlaySound ;
    public int[] posRamdon = new int[100];
    public Sprite sprMask;
    public Sprite SprOrigin;
    Material mtr; 
    private void Awake()
    {
        instance = this;
        for(int i = 0; i< 100; i++)
        {
            posRamdon[i] = Random.Range(0, erSize) * erSize;
        }
       
    }
    private void Start()
    {
        mask = LayerMask.GetMask("AreaDraw");
    }
    private void OnEnable()
    {
        
    }
    public Vector2 rectTex= new Vector2(100,100);
    public void FirstSetUpData()
    {
        erSize = 15;
        if(bodyPart.canStopDrawLine)
            PlayScreen.Instance?.ShowBTNStopDrawLine();
        bodyPart.mtrDraw = Instantiate( Character.instance.MtrDraw);
        mtr = bodyPart.mtrDraw;
        bodyPart.ChangeMaterialDraw();
        bodyPart.drawing = true;

        SprOrigin = bodyPart.GetSpriteRenderer().sprite;
      
        var texMask = sprMask.texture;
        var TexOrigin = SprOrigin.texture;
        width = texMask.width;
        height = texMask.height;
        WOrigin = TexOrigin.width;
        HOrigin = TexOrigin.height;
        texture = new Texture2D(100, 100, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Bilinear;
        texture.wrapMode = TextureWrapMode.Clamp;
        origin_Colors_Mask = texMask.GetPixels();
         
        texture.name = sprMask.name;
        colors = new Color[origin_Colors_Mask.Length];
        visited = new int[origin_Colors_Mask.Length];
        countPixelDone = 0;
        if (stateDraw == StateDraw.Show)
        {
            /*for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.clear;
                origin_Colors_Mask = TexOrigin.GetPixelBilinear(float(x)/ HOrigin, (float)y)
                if (origin_Colors_Mask[i].a == 0) countPixelDone++;
                else
                {
                    colors[i].a = 0.05f;
                }
            }*/
            int index;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    index = x * width + y;
                    colors[index] = Color.clear;
                    origin_Colors_Mask[index]  = TexOrigin.GetPixelBilinear((float)y / height, (float)x / width);
                    if (origin_Colors_Mask[index].a == 0) countPixelDone++;
                    else
                    {
                        colors[index].a = 0.05f;
                    }
                }
            }
        }
        else if (stateDraw == StateDraw.Hide)
        {
            /* for (int i = 0; i < colors.Length; i++)
             {
                 colors[i] = origin_Colors_Mask[i];
                 if (origin_Colors_Mask[i].a == 0) countPixelDone++;
             }*/
      
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    index = x * width + y;
                    colors[index] = TexOrigin.GetPixelBilinear((float)y / height,(float)x / width);
                    origin_Colors_Mask[index] = colors[index];
                    if (origin_Colors_Mask[index].a == 0) countPixelDone++;
                }
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
        bodyPart.UpdateTextureInput();
        mtr.SetTexture("_TextureInput", texture);
       // sprite_renderer.sprite = Sprite.Create(texture, sprite_renderer.sprite.rect, new Vector2(0.5f, 0.5f));
        stop_drawing = false;
        stop_drawing = false;
        index_colored.Clear();
        CompleteDrawing = false;
        percentDraw = 0.8f;
        percentDraw *= (origin_Colors_Mask.Length - countPixelDone);
        countPixelDone = 0; 
    }
/*    public void FirstSetUpData2()
    {
        PlayScreen.Instance?.ShowBTNStopDrawLine();
       
        sprite_renderer = bodyPart.GetSpriteRenderer();
        var tex = sprite_renderer.sprite.texture;
        width = tex.width;
        height = tex.height;
        texture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Bilinear;
        texture.wrapMode = TextureWrapMode.Clamp;
        origin_Colors = tex.GetPixels();
        texture.name = sprite_renderer.sprite.name;
        colors = new Color[origin_Colors.Length];
        visited = new int[origin_Colors.Length];
        countPixelDone = 0;
        if (stateDraw == StateDraw.Show)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.clear;
                if(origin_Colors[i].a == 0) countPixelDone++;
                else
                {
                    colors[i].a = 0.05f;
                }
            }
        }
        else if (stateDraw == StateDraw.Hide)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = origin_Colors[i];
                if (origin_Colors[i].a == 0) countPixelDone++;
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
        sprite_renderer.sprite = Sprite.Create(texture, sprite_renderer.sprite.rect, new Vector2(0.5f, 0.5f));
        stop_drawing = false;
        stop_drawing = false;
        index_colored.Clear();
        CompleteDrawing = false;
        percentDraw = 0.8f;
        percentDraw *= (origin_Colors.Length- countPixelDone);
        countPixelDone = 0;
    }*/
   /* public void ResetDraw( )
    {
     
        sprite_renderer = bodyPart.GetSpriteRenderer();
        var tex = sprite_renderer.sprite.texture;
        texture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Bilinear;
        texture.wrapMode = TextureWrapMode.Clamp;
      
        colors = new Color[origin_Colors.Length];
        if (stateDraw == StateDraw.Show)
        {  
            origin_Colors = tex.GetPixels();
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.clear;
            }
        }
        else if (stateDraw == StateDraw.Hide)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = origin_Colors[i];
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
        sprMask = Sprite.Create(texture, sprMask.rect, new Vector2(0.5f, 0.5f));
        stop_drawing = false;
        index_colored.Clear();
        CompleteDrawing = false;
       
    }
*/

    public void FinishDraw()
    {
        StartCoroutine(StopDrawing());
    }
 
    void Update()
    {
       
        if (stop_drawing || makeUpTool == null) return;
        if (Input.GetMouseButton(0)&&makeUpTool.GetStateUse()  )
        {
          
            hit = Physics2D.Raycast(makeUpTool.GetPositionDraw().position, Vector2.zero,10, mask);
            if (hit.collider != null)
            {
                if (countFrame % 2 == 0)
                {
                    UpdateTexture();
                    drawing = true;
                    
                }
              
                countFrame++;
                if (countFrame >= 20000) countFrame = 0;
                
            }
            else
            {
                drawing = false;
            }
        }
        else
        {
            drawing = false;
        }

    }
    
    


    Vector2 PosMouseOnSprite;
    Vector2Int start;
    Vector2Int end;
    Vector2Int p;
    Vector2 dir;
    Vector2 pixel;
    Vector2 linePos;
    int index;
    int PosX, PosY;
    public void UpdateTexture()
    {
        PosMouseOnSprite = hit.point - (Vector2)hit.collider.bounds.min;
        PosMouseOnSprite.x *= width / hit.collider.bounds.size.x;
        PosMouseOnSprite.y *= height / hit.collider.bounds.size.y;
        p = new Vector2Int((int)PosMouseOnSprite.x, (int)PosMouseOnSprite.y);
        if (!drawing)
            lastPos = p;
        start.x = Mathf.Clamp(Mathf.Min(p.x, lastPos.x) - erSize, 0, width);
        start.y = Mathf.Clamp(Mathf.Min(p.y, lastPos.y) - erSize, 0, height);
        end.x = Mathf.Clamp(Mathf.Max(p.x, lastPos.x) + erSize, 0, width);
        end.y = Mathf.Clamp(Mathf.Max(p.y, lastPos.y) + erSize, 0, height);
        dir = p - lastPos;
        int PreCountPixelHadDraw = countPixelDone;
        UpdatePixel();
        if(PreCountPixelHadDraw < countPixelDone )
        {
            Manager.Instance.GetCurrentAction().CurMakeUpTool.ActionMakeUpTool();
            Manager.Instance.GetCurrentAction().CurMakeUpTool.PlaySound();
        }
        else
        {
            Manager.Instance.GetCurrentAction().CurMakeUpTool.StopAction(); 
        }
        lastPos = p;
        texture.SetPixels(colors);
        texture.Apply(); 
        mtr.SetTexture("_TextureInput", texture);
    }
 
    public void UpdatePixel()
    {
        float d = 0;
        int erS = erSize * erSize;
        bool draw = true;
    
        float dirMagnitude = dir.sqrMagnitude;

        for (PosX = start.x; PosX < end.x; PosX++)
        {
            for (PosY = start.y; PosY < end.y; PosY++)
            {
                index = PosX + PosY * width;
                if (visited[index] == 1 || origin_Colors_Mask[index].a == 0) continue;
              
                draw = Probability(95);
                if (!draw)
                {
                    continue;
                }
 

                pixel = new Vector2(PosX, PosY);
                linePos = p;
                erS = erSize * Random.Range(erSize/2, erSize);
                d = Vector2.Dot(pixel - lastPos, dir) / dirMagnitude;
               // d = Mathf.Clamp01(d);
                linePos = Vector2.Lerp(lastPos, p, d);
                if (draw && (pixel - linePos).sqrMagnitude <= erS)
                {
                    if (stateDraw == StateDraw.Show)
                        colors[index] = origin_Colors_Mask[index];
                    else if (stateDraw == StateDraw.Hide)
                    {
                        colors[index] = Color.clear;
                    }
                    visited[index] = 1;
                    countPixelDone++;
                    if (countPixelDone > percentDraw)
                    {
                        StartCoroutine(StopDrawing());
                        return;
                    }
                }
            }
        }
    }
    public bool Probability(int val)
    {
        return (Random.Range(0, 100) <= val);
    }
    IEnumerator StopDrawing()
    {
        if (CompleteDrawing == true)
        {
            yield break;
        }
        stop_drawing = true;
        var step = colors.Length / 10;

        for (int i = 0; i < colors.Length; i++)
        {
            if (stateDraw == StateDraw.Show)
            {
                if (colors[i] != origin_Colors_Mask[i])
                {
                    colors[i] = origin_Colors_Mask[i];
                }
            }
            else if (stateDraw == StateDraw.Hide)
            {
                if (colors[i] == origin_Colors_Mask[i])
                {
                    colors[i] = Color.clear;
                }
            }
           
            if (i % step == 0)
            {
                yield return null;
                texture.SetPixels(colors);
                texture.Apply(); 
                mtr.SetTexture("_TextureInput", texture);
            }
        }
        texture.SetPixels(colors);
        texture.Apply(); 
        mtr.SetTexture("_TextureInput", texture);
        CompleteDrawing = true;
        if (stateDraw == StateDraw.Hide)
        {
            bodyPart.HideSprite();
        }else
        {
            bodyPart.ChangeMaterialNormal();

        }
        yield return new WaitForSeconds(1);
        ResetDataBodyPartAndEndAction();
    }
    public void ResetDataBodyPartAndEndAction()
    {
        bodyPart.ChangeStateCollider(false); // hide collider 
        PlayScreen.Instance.HideBTNStopDrawLine();
        Manager.Instance.GetCurrentAction().NextChildAction();// event finish
    }
    
    public void StopActionDraw()
    {
        ResetDataBodyPartAndEndAction();
    }

   
}
