using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GiftWrapping))]
public class GiftWrappingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawDefaultInspector();
        GiftWrapping giftWrapping = (GiftWrapping)target;

        if(GUILayout.Button("Generate Points"))
        {
            giftWrapping.GeneratePoints();
        }

        if (GUILayout.Button("Test"))
        {
            giftWrapping.TestFunc();
        }
    }
}
