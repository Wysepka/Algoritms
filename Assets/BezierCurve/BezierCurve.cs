using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[ExecuteInEditMode]
public class BezierCurve : MonoBehaviour
{
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;
    [SerializeField] Transform point3;
    [SerializeField] Transform point4;

    public int loopsCount = 100;

    [SerializeField] public
    List<Vector3> bezierPointsList;

    List<Vector3> bezierMainPoints;
    List<Vector3> bezierCheckPoints;

    [SerializeField]
    List<BezierTwoPointData> bezierCurves;

    [SerializeField]
    int numOfCheckPoints = 20;

    public BezierTwoPointData bezierTwoPointData;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void CalculateNewBezierCurve()
    {
        if(bezierCurves == null || bezierCurves.Count == 0)
        {
            bezierCurves = new List<BezierTwoPointData>();
        }

        bezierTwoPointData = new BezierTwoPointData(numOfCheckPoints, point1, point2, point3, point4);
        /*
        if (!bezierCurves.Contains(bezierTwoPointData))
        {
            bezierCurves.Add(bezierTwoPointData);
        }
        else
        {
            Debug.Log("The same curve is already in the list");
        }
        */

        if(!bezierCurves.Any<BezierTwoPointData>(x => (x.startPoint.position == bezierTwoPointData.startPoint.position &&
        x.endPoint.position == bezierTwoPointData.endPoint.position)))
        {
            bezierCurves.Add(bezierTwoPointData);
        }
        else
        {
            Debug.Log("The same curve is already in the list");
        }
    }

    public void ResetBezierCurve()
    {
        bezierCurves = new List<BezierTwoPointData>();
    }

    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(point1.position, 0.1f);
        Gizmos.DrawSphere(point4.position, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(point2.position, 0.1f);
        Gizmos.DrawSphere(point3.position, 0.1f);

        if(bezierPointsList != null && bezierPointsList.Count > 0)
        {
            for (int i = 1; i < bezierPointsList.Count; i++)
            {
                Gizmos.DrawLine(bezierPointsList[i], bezierPointsList[i - 1]);
            }
        }

        Gizmos.DrawLine(bezierPointsList[bezierPointsList.Count - 1], point4.position);
        */


    }

    private void OnValidate()
    {
        CalculateBezierCurve();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCurvesTransforms();
    }

    private void CheckCurvesTransforms()
    {
        if(bezierCurves.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < bezierCurves.Count; i++)
        {
            if (bezierCurves[i].CheckIfTransformChanged())
            {
                Debug.Log("Updating Curve: " + i);
                bezierCurves[i].UpdateCurve();
            }
        }
    }

    public void AssignValues()
    {
        Vector3 startPoint = new Vector3(0, 0, 0);
        Vector3 endPoint = new Vector3(0, 0, 5f);
        Vector3 firstControlPoint = new Vector3(0, 2f, 1f);
        Vector3 secondControlPoint = new Vector3(0, 2f, 4f);

        bezierPointsList = new List<Vector3>();

        CalculateBezierCurve();
    }

    public void CalculateBezierCurve()
    {
        float distBetweenPoints = 1f / loopsCount;

        for (int i = 0; i < loopsCount; i++)
        {
            Vector3 newPos = DeCasteljausAlgorithm((float)i / (float)loopsCount);
            Debug.Log((float)i / (float)loopsCount);
            Debug.Log(newPos);
            bezierPointsList.Add(newPos);
        }
    }

    Vector3 DeCasteljausAlgorithm(float t)
    {
        //Linear interpolation = lerp = (1 - t) * A + t * B
        //Could use Vector3.Lerp(A, B, t)

        //To make it faster
        float oneMinusT = 1f - t;

        //Layer 1
        Vector3 Q = oneMinusT * point1.position + t * point2.position;
        Vector3 R = oneMinusT * point2.position + t * point3.position;
        Vector3 S = oneMinusT * point3.position + t * point4.position;

        //Layer 2
        Vector3 P = oneMinusT * Q + t * R;
        Vector3 T = oneMinusT * R + t * S;

        //Final interpolated position
        Vector3 U = oneMinusT * P + t * T;

        return U;
    }




}

[Serializable]
public class BezierTwoPointData : IEquatable<BezierTwoPointData>
{
    List<Vector3> bezierPoints;
    public Transform startPoint;
    public Transform endPoint;
    public Transform controlPoint1;
    public Transform controlPoint2;
    public int numOfPoints;

    public BezierTwoPointData(int numOfPoints , Transform startPoint , Transform endPoint , Transform firstControlPoint , Transform secondControlPoint)
    {
        this.numOfPoints = numOfPoints;
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.controlPoint1 = firstControlPoint;
        this.controlPoint2 = secondControlPoint;
        bezierPoints = new List<Vector3>();

        bezierPoints = BezierAlgos.GenerateBezierPoints(numOfPoints, startPoint, controlPoint1, controlPoint2, endPoint);
        
    }

    public bool Equals(BezierTwoPointData otherBezierData)
    {
        return this.startPoint.position == otherBezierData.startPoint.position &&
                this.endPoint.position == otherBezierData.endPoint.position;
    }

    public void UpdateCurve()
    {
        bezierPoints = BezierAlgos.GenerateBezierPoints(numOfPoints, startPoint, controlPoint1, controlPoint2, endPoint);
    }

    public List<Vector3> ReturnBezierPointsList()
    {
        return bezierPoints;
    }

    public bool CheckIfTransformChanged()
    {
        if (startPoint.hasChanged)
        {
            return true;
        }
        else if (endPoint.hasChanged)
        {
            return true;
        }
        else if (controlPoint1.hasChanged)
        {
            return true;
        }
        else if (controlPoint2.hasChanged)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public override bool Equals(object obj)
    {
        var data = obj as BezierTwoPointData;

        if(data == null)
        {
            return false;
        }

        return (this.startPoint == data.startPoint) && (this.endPoint == data.endPoint);
    }

    public override int GetHashCode()
    {
        return startPoint.position.GetHashCode() + endPoint.position.GetHashCode();
    }

}

