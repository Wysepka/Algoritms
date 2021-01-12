using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RubicksCube))]
public class RubicksCubeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawDefaultInspector();
        RubicksCube rubicksCube = (RubicksCube)target;
        
        if(GUILayout.Button("Generate Cube"))
        {
            rubicksCube.GenerateRubicksCube();
        }
        if(GUILayout.Button("Test Func"))
        {
            rubicksCube.TestFunc2();
        }
    }
}
