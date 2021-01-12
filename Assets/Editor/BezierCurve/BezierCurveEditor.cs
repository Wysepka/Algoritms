using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveEditor : Editor
{
    BezierCurve bezierCurve;

    private void Awake()
    {
        bezierCurve = FindObjectOfType<BezierCurve>();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //DrawDefaultInspector();
        BezierCurve bezierCurve = (BezierCurve)target;

        if (GUILayout.Button("Generate"))
        {
            bezierCurve.CalculateNewBezierCurve();
        }

        if(GUILayout.Button("Reset Bezier Curve"))
        {
            bezierCurve.ResetBezierCurve();
        }

        if(GUILayout.Button("Open Editor"))
        {
            BezierCurveWindow bezierCurveWindow = EditorWindow.GetWindow<BezierCurveWindow>();
            bezierCurveWindow.UpdateProperties(bezierCurve.bezierPointsList);
        }
    }
}
