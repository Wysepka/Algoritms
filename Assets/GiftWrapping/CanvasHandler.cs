using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    public float xVal;
    public float zVal;

    public int pointsNum;
    public float pointsSize;

    GiftWrapping giftWrapping;

    private void Awake()
    {
        giftWrapping = FindObjectOfType<GiftWrapping>();
        
    }

    public void AssignXVal(float xVal)
    {
        this.xVal = xVal;
    }

    public void AssignZVal(float zVal)
    {
        this.zVal = zVal;
    }

    public void AssignPointsNumber(int points)
    {
        this.pointsNum = points;
    }

    public void AssignPointsSize(float sizePoint)
    {
        this.pointsSize = sizePoint;
    }

    public void PassPropertiesToAlgorithm()
    {
        giftWrapping.CalculateProperties(xVal, zVal, pointsNum, pointsSize);
    }
}
