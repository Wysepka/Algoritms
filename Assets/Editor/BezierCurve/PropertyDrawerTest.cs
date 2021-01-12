using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestScript))]
public class PropertyDrawerTest : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("someClassArray") , true);
        serializedObject.ApplyModifiedProperties();
    }
}
