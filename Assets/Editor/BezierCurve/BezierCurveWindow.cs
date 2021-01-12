using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BezierCurveWindow : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    bool needUpdate;

    List<Vector3> bezierPoints;

    Vector2 bezierPointsScrollView = Vector2.zero;

    [MenuItem("Window/BezierCurve")]
    static void Init()
    {
        BezierCurveWindow bezierCurveWindow = (BezierCurveWindow)EditorWindow.GetWindow(typeof(BezierCurveWindow));
        bezierCurveWindow.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
        EditorGUILayout.BeginScrollView(bezierPointsScrollView, GUILayout.Height(250f));
        if (bezierPoints != null || bezierPoints.Count > 0)
        {
            for (int i = 0; i < bezierPoints.Count; i++)
            {
                EditorGUILayout.Vector3Field("Bezier Point" + i, bezierPoints[i]);
            }
        }
        else
        {
            bezierPoints = FindObjectOfType<BezierCurve>().bezierPointsList;
        }

        if (needUpdate)
        {


            needUpdate = false;
        }
        EditorGUILayout.EndScrollView();
    }

    public void UpdateProperties(List<Vector3> bezierPoints)
    {
        this.bezierPoints = bezierPoints;
        needUpdate = true;
    }

}
