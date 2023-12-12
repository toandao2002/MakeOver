using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NameFx
{
    MagicBuffBlue,
    StunExplosion,

}
public class ObjectPoolFx : MonoBehaviour
{
    public static  ObjectPoolFx instance;
    public List<GameObject> objectFxPrefab;
    public List<GameObject> objectFx;
    private void Awake()
    {
        instance = this;
    }
    public GameObject GetObjectFx(NameFx namefx, Vector3 pos)
    {
        var obj = Instantiate(objectFxPrefab[(int)namefx], transform);
        objectFx.Add(obj);
        obj.transform.position = pos;
        return obj;
    }
    public void DesTroyFx( GameObject _obj)
    {
        objectFx.Remove(_obj);
        Destroy(_obj);
    }
}
