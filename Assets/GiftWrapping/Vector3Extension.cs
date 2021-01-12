using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Vector3Extension
{ 

    public static Vector3[,] CalculateVectorsForQuads(GameObject[] objectArray , float lineThickness)
    {
        Transform[] objectTransformArray = new Transform[objectArray.Length];

        for (int i = 0; i < objectArray.Length; i++)
        {
            objectTransformArray[i] = objectArray[i].transform;
        }

        Vector3[,] quadVectors = CalculateVectorsForQuads(objectTransformArray , lineThickness);

        return quadVectors;
    }

    public static Vector3[,] CalculateVectorsForQuads(Transform[] objectArray , float lineThickness)
    {
        Vector3[,] quadVectors = new Vector3[objectArray.Length-1, 4];
        Vector3[] objectPositions = new Vector3[objectArray.Length];

        for (int i = 0; i < objectArray.Length; i++)
        {
            objectPositions[i] = objectArray[i].position;
        }

        quadVectors = CalculateVectorsForQuads(objectPositions, lineThickness);

        return quadVectors;
    }

    public static Vector3[,] CalculateVectorsForQuads(Vector3[] objectPositions , float lineThickness)
    {
        Vector3[,] quadVectors = new Vector3[objectPositions.Length-1, 4];

        for (int i = 0; i < objectPositions.Length - 1; i++)
        {
            Vector3 tempVec = objectPositions[i];
            quadVectors[i, 0] = new Vector3(tempVec.x - lineThickness / 2f, tempVec.y, tempVec.z);
            quadVectors[i, 1] = new Vector3(tempVec.x + lineThickness / 2f, tempVec.y, tempVec.z);

            Vector3 tempVec2 = objectPositions[i + 1];
            quadVectors[i, 3] = new Vector3(tempVec2.x - lineThickness / 2f, tempVec2.y, tempVec2.z);
            quadVectors[i, 2] = new Vector3(tempVec2.x + lineThickness / 2f, tempVec2.y, tempVec2.z);
        }

        return quadVectors;
    }


}
