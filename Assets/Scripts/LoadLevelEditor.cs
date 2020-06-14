#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LoadLevel))]
public class LoadLevelEditor : Editor
{
    LoadLevel script;
    public void OnEnable()
    {
        script = (LoadLevel)target;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();
        if (script.loadDest == LoadDestination.number)
        {
            script.level = EditorGUILayout.IntField("Level Index", script.level);
        }
        script.trigger = (LoadTrigger)EditorGUILayout.EnumPopup("Trigger", script.trigger);
        if (script.trigger == LoadTrigger.buttonPress)
        {
            script.button = EditorGUILayout.TextField("Button", script.button);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif