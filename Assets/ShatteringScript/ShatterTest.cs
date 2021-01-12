using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterTest : MonoBehaviour
{
    Vector3 pointPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if(pointPos != null)
        {
            Gizmos.DrawSphere(pointPos, 0.1f);
        }
    }

    public void GeneratePointInsideCube()
    {
        Bounds cubeBounds = GetComponent<Renderer>().bounds;

        Vector3 newPointInsideCube;

        float xRandVal = cubeBounds.center.x + Random.Range(-cubeBounds.extents.x, cubeBounds.extents.x);
        float yRandVal = cubeBounds.center.y + Random.Range(-cubeBounds.extents.y, cubeBounds.extents.y);
        float zRandVal = cubeBounds.center.z + Random.Range(-cubeBounds.extents.z, cubeBounds.extents.z);


        pointPos = new Vector3(xRandVal, yRandVal, zRandVal);

        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

        Mesh newMesh = new Mesh();
        newMesh.name = "Shattered Mesh";

        newMesh.vertices = mesh.vertices;
        //newMesh.vertices.
        Vector3[] verts = newMesh.vertices;
        Debug.Log(verts.Length);
        for (int i = 0; i < verts.Length; i++)
        {
            Debug.Log(verts[i]);
        }
        Debug.Log(pointPos);


    }
}
