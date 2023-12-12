using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(MonoBehaviour), true)]
public class ButtonEditor : Editor
{
  
 
    public override void OnInspectorGUI()
    {
        var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        foreach (var method in methods)
        {
            var bMethod = method.GetCustomAttribute(typeof(ButtonAttribute), true);
            if (bMethod != null)
            {
                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(target, null);
                }
            }
        }

        DrawDefaultInspector();
    }
    
}
