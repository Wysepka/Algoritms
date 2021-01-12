using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierTwoPointData))]
public class BezierCurveDataEditor : UnityEditor.Editor
{
    private bool ifVis = true;

    
    public override void OnInspectorGUI()
    {
        /*
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("startPoint"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("endPoint"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("controlPoint1"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("controlPoint1"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("numOfPoints"));
        serializedObject.ApplyModifiedProperties();
        */
    }


    public void ListIterator(string listName)
    {
        //List object
        SerializedProperty listIterator = serializedObject.FindProperty(listName);
        Rect drawZone = GUILayoutUtility.GetRect(0f, 16f);
        bool showChildren = EditorGUI.PropertyField(drawZone, listIterator);
        listIterator.NextVisible(showChildren);

        //List size
        drawZone = GUILayoutUtility.GetRect(0f, 16f);
        showChildren = EditorGUI.PropertyField(drawZone, listIterator);
        bool toBeContinued = listIterator.NextVisible(showChildren);
        //Elements
        int listElement = 0;
        while (toBeContinued)
        {
            drawZone = GUILayoutUtility.GetRect(0f, 16f);
            showChildren = EditorGUI.PropertyField(drawZone, listIterator);
            toBeContinued = listIterator.NextVisible(showChildren);
            listElement++;
        }
    }

    public void ListIterator(string propertyPath, ref bool visible)
    {
        SerializedProperty listProperty = serializedObject.FindProperty(propertyPath);
        visible = EditorGUILayout.Foldout(visible, listProperty.name);
        if (visible)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < listProperty.arraySize; i++)
            {
                SerializedProperty elementProperty = listProperty.GetArrayElementAtIndex(i);
                Rect drawZone = GUILayoutUtility.GetRect(0f, 16f);
                bool showChildren = EditorGUI.PropertyField(drawZone, elementProperty);
            }
            EditorGUI.indentLevel--;
        }
    }
}
