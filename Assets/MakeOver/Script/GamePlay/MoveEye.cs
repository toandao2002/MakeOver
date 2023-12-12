using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MoveEye : MonoBehaviour
{
    public List<GameObject> positions;
    public GameObject PosRoot;
    public GameObject Parent;
    public GameObject child;
    public int count = 0;
    public int state;
    public Animator ani;
    public bool anib;
    private void Start()
    {
        
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (anib)
        {
            PlayAnim();
        }
        else Move();
    }

    public void test()
    {

    }
    void PlayAnim()
    {
        if(state == 1)
        {
            ani.Play("FakeMoveEye");
        }
        else
        {
            ani.Play("EyeNormal");
        }
    }
    public void AddPos()
    {
        var tmp = Instantiate(child, Parent.transform);
        tmp.transform.localPosition = PosRoot.transform.localPosition;
        positions.Add(tmp);
    }
    public void Undo()
    {
        if (positions.Count == 0) return;
        var tmp = positions[positions.Count - 1];
        positions.RemoveAt(positions.Count - 1);
        PosRoot.transform.localPosition = tmp.transform.localPosition;
        DestroyImmediate(tmp);
    }
    public void Move()
    {
        Vector3[] pos = new Vector3[positions.Count];

        for (int i = 0 ; i < positions.Count; i++)
        {
            pos[i] = positions[i].transform.position;
        }
        PosRoot.transform.DOPath(pos, positions.Count * 2, PathType.Linear,PathMode.Full3D).SetLoops(-1,LoopType.Restart); ;
    }
    Vector3 prePos;
    public void Update()
    {
        if (anib) return;
        Vector3 dir = prePos - PosRoot.transform.position;
        float angel = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg+ 90 ;
        PosRoot.transform.localRotation = Quaternion.Euler(0, 0, angel);
        prePos = PosRoot.transform.position;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(MoveEye))]
class  MoveEyeEditor: Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var eye = (MoveEye)target;
        if (eye == null) return;
        if(GUILayout.Button("Add pos")){
            eye.AddPos();
        }
        if (GUILayout.Button("Undo"))
        {
            eye.Undo();
        }
    }
}
#endif