using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(BezierTwoPointData))]
public class BezierCurveDataDrawer : PropertyDrawer
{
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        /*
        BezierTwoPointData bezierTwoPointData =  (BezierTwoPointData)fieldInfo.GetValue(property.serializedObject.targetObject);
        GUILayout.BeginScrollView(position.position);
        List<Vector3> bezierPoints = bezierTwoPointData.ReturnBezierPointsList();
        for (int i = 0; i < bezierPoints.Count; i++)
        {
            EditorGUILayout.Vector3Field("Point: " + i, bezierPoints[i]);
        }
        GUILayout.EndScrollView();
        */

        
        
        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        if (position.height > 16f)
        {
            position.height = 16f;
            EditorGUI.indentLevel += 1;
            contentPosition = EditorGUI.IndentedRect(position);
            contentPosition.y += 18f;
        }
        contentPosition.width *= 0.75f;
        EditorGUI.indentLevel = 0;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("endPoint"), GUIContent.none);
        contentPosition.x += contentPosition.width;
        contentPosition.width /= 3f;
        contentPosition.y += 16f;
        EditorGUIUtility.labelWidth = 10f;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("startPoint"), new GUIContent("S"));
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("controlPoint1"), new GUIContent("C1"));

        EditorGUI.EndProperty();
        
    }
    
}
