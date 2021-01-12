using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GiftWrapping : MonoBehaviour
{

    [SerializeField]
    static Material lineMat;

    public float lineThickness = 0.1f;

    public Vector3 center = Vector3.zero;
    public Vector2 size;
    public int numberOfPoints;
    public Vector3 pointSize = new Vector3(0.1f, 0.1f, 0.1f);

    GameObject[] points;

    List<GameObject> pointsList;

    [SerializeField]
    Vector3[,] quadsPos;

    Vector3 leftMostVert;
    Vector3 currVert;
    Vector3 nextVert;
    Vector3 checkingVert;

    Vector3 firstVector;
    Vector3 nextVector;
    Vector3 firstCrossProd;

    List<GameObject> hullObjects;

    List<Vector3> hullPositions;
    List<Vector3> hullPosAddedLine;

    Vector3[] objPos;

    bool ifFullHull;

    int keyEnter = 0;
    int index = 2;
    int loopNumber;

    // Start is called before the first frame update

    private void Awake()
    {
        lineMat = Resources.Load("lineMat") as Material;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            keyEnter++;
            Debug.Log("Nacisnalem spacje lol");
            Debug.Log("HULL OBJ COUNT: " + hullPositions.Count);
            CalculateConvexHull();
        }
        */
        //CalculateConvexHull();
    }

    public void CalculateProperties(float xVal = 5 , float zVal  = 5 , int numOfPoints  = 10 , float pointsSize = 0.1f)
    {
        size = new Vector3(xVal, zVal);
        numberOfPoints = numOfPoints;
        pointSize = new Vector3(pointsSize, pointsSize, pointsSize);
    }

    public void StartAlgorithm()
    {
        GeneratePoints();
        SetupConvexHull();
        InvokeRepeating("CalculateConvexHull", 1f, 10f / (float)numberOfPoints);
    }

    public void GeneratePoints()
    {
        points = new GameObject[numberOfPoints];
        points = PointsRandomizer.GenerateRandomPoints(center, numberOfPoints, size, pointSize);

        pointsList = new List<GameObject>();

        for (int i = 0; i < points.Length; i++)
        {
            pointsList.Add(points[i]);
        }

        quadsPos = Vector3Extension.CalculateVectorsForQuads(points, lineThickness);

        SortPointsByXVal();
    }

    public void CalculateConvexHull()
    {

        if (!ifFullHull)
        {
            checkingVert = objPos[index];

            firstVector = nextVert - currVert;
            nextVector = checkingVert - currVert;
            firstCrossProd = Vector3.Cross(firstVector, nextVector);

            if (firstCrossProd.y < 0)
            {
                nextVert = checkingVert;
            }
            index += 1;

            if (index == objPos.Length)
            {
                if (nextVert == leftMostVert)
                {
                    hullPositions.Add(nextVert);
                    ifFullHull = true;
                }
                else
                {
                    hullPositions.Add(nextVert);
                    currVert = nextVert;
                    index = 0;
                    nextVert = leftMostVert;
                }
            }

            loopNumber++;

        }

        /*
        for (int i = 0; i < hullPositions.Count; i++)
        {
            GameObject newObject = new GameObject("Hull Object: " + i);
            newObject.transform.position = hullPositions[i];
        }
        */
    }

    private void SetupConvexHull()
    {
        keyEnter = 0;
        index = 2;
        loopNumber = 0;

        hullObjects = new List<GameObject>();
        hullPositions = new List<Vector3>();
        hullPosAddedLine = new List<Vector3>();
        objPos = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            objPos[i] = points[i].transform.position;
        }

        leftMostVert = objPos[0];
        currVert = leftMostVert;
        nextVert = objPos[index - 1];
        checkingVert = objPos[index];

        hullObjects.Add(points[0]);
        hullPositions.Add(objPos[0]);

        ifFullHull = false;
    }

    private void SortPointsByXVal()
    {
        Array.Sort(points, delegate (GameObject point1, GameObject point2)
        {
            return point1.transform.position.x.CompareTo(point2.transform.position.x);
        });
    }

    private void OnDrawGizmos()
    {
        Vector3 newVec = new Vector3(size.x, 1f, size.y);
        Gizmos.DrawWireCube(center, newVec);

        Gizmos.color = Color.red;
        Gizmos.DrawCube(currVert, pointSize * 1.5f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(nextVert, pointSize * 1.35f);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(checkingVert, pointSize * 1.2f);

    }

    private void OnRenderObject()
    {
        CreateLineMaterial();

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        GL.MultMatrix(transform.localToWorldMatrix);
        //GL.wireframe = true;
        // Draw lines

        #region GL QUADS
        /*
        GL.Begin(GL.QUADS);
        GL.Color(Color.cyan);

         //Quad Lines
        if(quadsPos.GetLength(0) > 0)
        {
            for (int i = 0; i < quadsPos.GetLength(0); i++)
            {
                GL.Vertex(quadsPos[i , 0]);
                GL.Vertex(quadsPos[i, 1]);
                GL.Vertex(quadsPos[i, 2]);
                GL.Vertex(quadsPos[i, 3]);
            }
        }
        */

        #endregion

        //1px thick line
        lineMat.SetPass(0);

        GL.Begin(GL.LINES);
        GL.Color(Color.cyan);

        if (hullPositions != null && hullPositions.Count > 0)
        {
            for (int i = 1; i < hullPositions.Count; i++)
            {
                GL.Vertex(hullPositions[i - 1] - this.transform.position);
                GL.Vertex(hullPositions[i] - this.transform.position);
            }
        }
        GL.End();

        /*
        for (int i = 1; i < points.Length; i++)
        {
            GL.Vertex(points[i - 1].transform.position - this.transform.position);
            GL.Vertex(points[i].transform.position - this.transform.position);
        }
        */

        /*
        GL.Color(Color.blue);

        if( hullPositions != null && hullPositions.Count > 0)
        {
            for (int i = 1; i < hullPositions.Count; i++)
            {
                GL.Vertex(hullPositions[i-1] - this.transform.position);
                GL.Vertex(hullPositions[i] - this.transform.position);
            }
            GL.Vertex(hullPositions[0] - this.transform.position);
        }
        */

        lineMat.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(Color.yellow);

        GL.Vertex(currVert - this.transform.position);
        GL.Vertex(nextVert - this.transform.position);

        GL.End();

        lineMat.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(Color.white);

        GL.Vertex(currVert - this.transform.position);
        GL.Vertex(checkingVert - this.transform.position);

        GL.End();  
        //GL.wireframe = false;
        GL.PopMatrix();

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

    public void TestFunc()
    {
        Vector3 st1 = new Vector3(0, 0, 0);
        Vector3 st2 = new Vector3(2, 0, -3);
        Vector3 st3 = new Vector3(2, 0, 4);

        Vector3 vector1 = st2 - st1;
        Vector3 vector2 = st3 - st1;

        Debug.Log("Cross1: " + Vector3.Cross(vector1, vector2));
        Debug.Log("Cross2: " + Vector3.Cross(vector2, vector1));
    }
}
