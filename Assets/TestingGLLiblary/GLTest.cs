using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLTest : MonoBehaviour
{
    public int lineCount = 100;
    public float radius = 3.0f;
    public float quadThickness = 2f;
    public int numberOfQuads = 1;

    [SerializeField]
    static Material lineMat;

    [SerializeField]
    Vector3 startPos;

    [SerializeField]
    Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void CreateLineMaterial()
    {
        if (!lineMat)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMat = new Material(shader);
            lineMat.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMat.SetInt("_ZWrite", 0);
        }
    }


    private void OnRenderObject()
    {

        CreateLineMaterial();
        Vector3[] quadVert = CalculateQuadVectors();
        // Apply the line material
        lineMat.SetPass(0);

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        GL.MultMatrix(transform.localToWorldMatrix);
        //GL.wireframe = true;
        // Draw lines
        GL.Begin(GL.QUADS);
        GL.Color(Color.cyan);
        for (int i = 0; i < quadVert.Length; i++)
        {
            GL.Vertex(quadVert[i]);
        }

        GL.End();
        //GL.wireframe = false;
        GL.PopMatrix();
    }

    private Vector3[] CalculateQuadVectors()
    {
        Vector3[] vectors = new Vector3[4];
        vectors[0] = new Vector3(startPos.x, startPos.y - quadThickness / 2f, startPos.z);
        vectors[1] = new Vector3(startPos.x, startPos.y + quadThickness / 2f, startPos.z);
        vectors[2] = new Vector3(endPos.x, endPos.y + quadThickness / 2f, endPos.z);
        vectors[3] = new Vector3(endPos.x, endPos.y - quadThickness / 2f, endPos.z);
        return vectors;
    }
}
