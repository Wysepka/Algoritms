using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PointsRandomizer
{
    public static GameObject[] GenerateRandomPoints(Vector3 center , int numberOfPoints , Vector2 randomizeSize , Vector3 pointsSize , bool wholeNumb = false)
    {
        GameObject[] pointsArray = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            float xValue;
            float zValue;
            if (wholeNumb)
            {
                xValue = Random.Range(Mathf.CeilToInt(center.x - randomizeSize.x / 2f), Mathf.CeilToInt(center.x + randomizeSize.x / 2f));
                zValue = Random.Range(Mathf.CeilToInt(center.z - randomizeSize.y / 2f), Mathf.CeilToInt(center.z + randomizeSize.y / 2f));
            }
            else
            {
                xValue = Random.Range(center.x - randomizeSize.x / 2f, center.x + randomizeSize.x / 2f);
                zValue = Random.Range(center.z - randomizeSize.y / 2f, center.z + randomizeSize.y / 2f);
            }
            Vector3 position = new Vector3(xValue, center.y, zValue);
            GameObject pos = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pos.name = "Position: " + (i + 1);
            pos.transform.position = position;
            pos.transform.localScale = pointsSize;
            pointsArray[i] = pos;
        }
        return pointsArray;
    }
}
