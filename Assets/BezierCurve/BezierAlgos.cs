using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezierAlgos
{
    public static List<Vector3> GenerateBezierPoints(int numOfPoints , Transform point1 , Transform point2 , Transform point3 , Transform point4)
    {
        List<Vector3> bezierPoints = new List<Vector3>();
        bezierPoints.Add(point1.position);

        for (int i = 1; i < numOfPoints; i++)
        {
            Vector3 newPoint = DeCasteljausAlgorithm(i / numOfPoints, point1, point2, point3, point4);
        }

        bezierPoints.Add(point4.position);

        return bezierPoints;
    }

    public static Vector3 DeCasteljausAlgorithm(float t , Transform point1 , Transform point2 , Transform point3 , Transform point4)
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
