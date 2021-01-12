using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShatterTest))]
public class ShatterTestEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ShatterTest shatterTest = (ShatterTest)target;

        if(GUILayout.Button("Generate Point"))
        {
            shatterTest.GeneratePointInsideCube();
        }
    }
}
