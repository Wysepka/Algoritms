using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubicksCube : MonoBehaviour
{
    

    GameObject rubickCubeParent;

    public int dimensions = 3;
    public float squareSize = 1f;
    public float qubieOffset = 0.1f;

    GameObject[,,] qubies;

    List<GameObject> threeSidedQubies;
    List<GameObject> twoSidedQubies;
    List<GameObject> oneSidedQubies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRubicksCube()
    {
        float squareCorrectSize = squareSize / 10f;

        /*
        for (int i = 0; i < dimensions; i++)
        {
            for (int j = 0; j < dimensions; j++)
            {
                for (int k = 0; k < dimensions; k++)
                {
                    float xVal = i * squareSize;
                    float yVal = j * squareSize;
                    float zVal = k * squareSize;

                    GameObject square = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    square.transform.localScale = new Vector3(squareCorrectSize , squareCorrectSize , squareCorrectSize);
                    square.transform.position = new Vector3(xVal , yVal , zVal);
                }
            }
        }
        */

        //GenerateUpAndBottomSide();
        //GenerateLeftAndRightSide();
        //GenerateFrondAndBackSide();
        float qubieCorrectOffset = qubieOffset + 1;

        qubies = new GameObject[3,3,3];
        threeSidedQubies = new List<GameObject>();
        twoSidedQubies = new List<GameObject>();
        oneSidedQubies = new List<GameObject>();


        rubickCubeParent = new GameObject("RubickCubeCubes");
        rubickCubeParent.transform.position = Vector3.zero;

        for (int i = 0; i < dimensions; i++)
        {
            for (int j = 0; j < dimensions; j++)
            {
                for (int k = 0; k < dimensions; k++)
                {
                    if(i == 1 && j == 1 && k == 1)
                    {
                        /*
                        GameObject innerCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        innerCube.transform.position = new Vector3(i  * qubieCorrectOffset, j  * qubieCorrectOffset, k *  qubieCorrectOffset);
                        innerCube.transform.localScale = new Vector3(squareSize * 2.8f, squareSize * 2.8f, squareSize * 2.8f);
                        innerCube.transform.parent = rubickCubeParent.transform;
                        */
                    }
                    else
                    {
                        GameObject qubieParent = new GameObject();
                        Qubie qubie = new Qubie(ref qubieParent);
                        qubieParent.transform.localScale = new Vector3(squareSize, squareSize, squareSize);
                        qubieParent.transform.position = new Vector3(i * squareSize * qubieCorrectOffset, j * squareSize * qubieCorrectOffset, k * squareSize * qubieCorrectOffset);
                        qubieParent.transform.parent = rubickCubeParent.transform;
                        qubies[i, j, k] = qubieParent;
                        //qubie.name = ApplyQubieName(i , j , k);
                    }
                }
            }
        }

        ApplyQubieNames();

        ApplyQubiePlaneColors();
    }

    private void ApplyQubiePlaneColors()
    {

        Color32 orange = new Color32(255, 140, 0 , 1);
        Color32 green = new Color32(0, 254, 111, 1);
        Color32 red = new Color32(254, 9, 0, 1);
        Color32 blue = new Color32(0, 122, 254, 1);
        Color32 white = new Color32(255, 255, 255, 1);
        Color32 yellow = new Color32(254, 224, 0, 1);

        for (int x = 0; x < 1; x++)
        {
            for (int y = 0; y < qubies.GetLength(0); y++)
            {
                for (int z = 0; z < qubies.GetLength(1); z++)
                {
                    for (int k = 0; k < qubies[x, y, z].transform.childCount; k++)
                    {
                        if (qubies[x, y, z].transform.GetChild(k).name == "leftPlane")
                        {
                            qubies[x, y, z].transform.GetChild(k).GetComponent<Renderer>().sharedMaterial.SetColor("_Color", orange);
                            qubies[x, y, z].transform.GetChild(k).GetComponent<Renderer>().sharedMaterial.SetColor("_SpecColor", orange);
                        }
                    }
                }
            }
        }

        for (int x = 0; x < qubies.GetLength(0); x++)
        {
            for (int y = 0; y < qubies.GetLength(1); y++)
            {
                for (int z = 0; z < qubies.GetLength(2) - 2; z++)
                {
                    for (int k = 0; k < qubies[x, y, z].transform.childCount; k++)
                    {
                        if (qubies[x, y, z].transform.GetChild(k).name == "frontPlane")
                        {
                            qubies[x, y, z].transform.GetChild(k).GetComponent<Renderer>().sharedMaterial.SetColor("_Color", green);
                            qubies[x, y, z].transform.GetChild(k).GetComponent<Renderer>().sharedMaterial.SetColor("_SpecColor", green);
                        }
                    }
                }
            }
        }

        for (int x = 2; x < qubies.GetLength(0); x++)
        {
            for (int y = 0; y < qubies.GetLength(1); y++)
            {
                for (int z = 0; z < qubies.GetLength(2); z++)
                {
                    for (int k = 0; k < qubies[x, y, z].transform.childCount; k++)
                    {
                        if (qubies[x, y, z].transform.GetChild(k).name == "rightPlane")
                        {
                            qubies[x, y, z].transform.GetChild(k).GetComponent<Renderer>().sharedMaterial.SetColor("_Color", red);
                            qubies[x, y, z].transform.GetChild(k).GetComponent<Renderer>().sharedMaterial.SetColor("_SpecColor", red);
                        }
                    }
                }
            }
        }

        for (int x = 0; x < qubies.GetLength(0); x++)
        {
            for (int y = 0; y < qubies.GetLength(1); y++)
            {
                for (int z = 2; z < qubies.GetLength(2); z++)
                {
                    for (int k = 0; k < qubies[x, y, z].transform.childCount; k++)
                    {
                        if (qubies[x, y, z].transform.GetChild(k).name == "backPlane")
                        {
                            qubies[x, y, z].transform.GetChild(k).GetComponent<Renderer>().sharedMaterial.SetColor("_Color", yellow);
                            qubies[x, y, z].transform.GetChild(k).GetComponent<Renderer>().sharedMaterial.SetColor("_SpecColor", yellow);
                        }
                    }
                }
            }
        }

        for (int x = 0; x < qubies.GetLength(0); x++)
        {
            for (int y = 0; y < qubies.GetLength(1) - 2; y++)
            {
                for (int z = 0; z < qubies.GetLength(2); z++)
                {
                    for (int k = 0; k < qubies[x, y, z].transform.childCount; k++)
                    {
                        if (qubies[x, y, z].transform.GetChild(k).name == "downPlane")
                        {
                            qubies[x, y, z].transform.GetChild(k).GetComponent<Renderer>().sharedMaterial.SetColor("_Color", blue);
                            qubies[x, y, z].transform.GetChild(k).GetComponent<Renderer>().sharedMaterial.SetColor("_SpecColor", blue);
                        }
                    }
                }
            }
        }

        for (int x = 0; x < qubies.GetLength(0); x++)
        {
            for (int y = 2; y < qubies.GetLength(1); y++)
            {
                for (int z = 0; z < qubies.GetLength(2); z++)
                {
                    for (int k = 0; k < qubies[x, y, z].transform.childCount; k++)
                    {
                        if (qubies[x, y, z].transform.GetChild(k).name == "topPlane")
                        {
                            qubies[x, y, z].transform.GetChild(k).GetComponent<Renderer>().sharedMaterial.SetColor("_Color", white);
                            qubies[x, y, z].transform.GetChild(k).GetComponent<Renderer>().sharedMaterial.SetColor("_SpecColor", white);
                        }
                    }
                }
            }
        }

    }

    private void ApplyQubieNames()
    {
        //Three Sided Qubies

        qubies[0, 0, 0].name = "Qubie3SidedLeftDownFront";
        qubies[0, 0, 2].name = "Qubie3SidedLeftDownBack";
        qubies[0, 2, 0].name = "Qubie3SidedLeftUpFront";
        qubies[0, 2, 2].name = "Qubie3SidedLeftUpBack";
        qubies[2, 0, 0].name = "Qubie3SidedRightDownFront";
        qubies[2, 0, 2].name = "Qubie3SidedRightDownBack";
        qubies[2, 2, 0].name = "Qubie3SidedRightUpFront";
        qubies[2, 2, 2].name = "Qubie3SidedRightUpBack";

        threeSidedQubies.Add(qubies[0, 0, 0]);
        threeSidedQubies.Add(qubies[0, 0, 2]);
        threeSidedQubies.Add(qubies[0, 2, 0]);
        threeSidedQubies.Add(qubies[0, 2, 2]);
        threeSidedQubies.Add(qubies[2, 0, 0]);
        threeSidedQubies.Add(qubies[2, 0, 2]);
        threeSidedQubies.Add(qubies[2, 2, 0]);
        threeSidedQubies.Add(qubies[2, 2, 2]);



        //Two Sided Qubies

        qubies[0, 0, 1].name = "Qubie2SidedLeftDownMiddle";
        qubies[0, 1, 0].name = "Qubie2SidedLeftMidFront";
        qubies[0, 1, 2].name = "Qubie2SidedLeftMidBack";
        qubies[0, 2, 1].name = "Qubie2SidedLeftUpMid";

        qubies[1, 0, 0].name = "Qubie2SidedMidDownFront";
        qubies[1, 0, 2].name = "Qubie2SidedMidDownBack";
        qubies[1, 2, 0].name = "Qubie2SidedMidUpFront";
        qubies[1, 2, 2].name = "Qubie2SidedMidUpBack";

        qubies[2, 0, 1].name = "Qubie2SidedRightDownMid";
        qubies[2, 1, 0].name = "Qubie2SidedRightMidFront";
        qubies[2, 1, 2].name = "Qubie2SidedRightMidBack";
        qubies[2, 2, 1].name = "Qubie2SidedRightUpMid";

        twoSidedQubies.Add(qubies[0, 0, 1]);
        twoSidedQubies.Add(qubies[0, 1, 0]);
        twoSidedQubies.Add(qubies[0, 1, 2]);
        twoSidedQubies.Add(qubies[0, 2, 1]);
        twoSidedQubies.Add(qubies[1, 0, 0]);
        twoSidedQubies.Add(qubies[1, 0, 2]);
        twoSidedQubies.Add(qubies[1, 2, 0]);
        twoSidedQubies.Add(qubies[1, 2, 2]);
        twoSidedQubies.Add(qubies[2, 0, 1]);
        twoSidedQubies.Add(qubies[2, 1, 0]);
        twoSidedQubies.Add(qubies[2, 1, 2]);
        twoSidedQubies.Add(qubies[2, 2, 1]);



        //OneSided

        qubies[0, 1, 1].name = "Qubie1SidedLeftMid";
        qubies[1, 0, 1].name = "Qubie1SidedMidDown";
        qubies[1, 1, 0].name = "Qubie1SidedMidFront";
        qubies[1, 1, 2].name = "Qubie1SidedMidBack";
        qubies[1, 2, 1].name = "Qubie1SidedMidUp";
        qubies[2, 1, 1].name = "Qubie1SidedRightMid";

        oneSidedQubies.Add(qubies[0, 1, 1]);
        oneSidedQubies.Add(qubies[1, 0, 1]);
        oneSidedQubies.Add(qubies[1, 1, 0]);
        oneSidedQubies.Add(qubies[1, 1, 2]);
        oneSidedQubies.Add(qubies[1, 2, 1]);
        oneSidedQubies.Add(qubies[2, 1, 1]);


    }

    private string ApplyQubieName(int i, int j, int k)
    {
        string qubieName = "";
        if(i == 0)
        {
            if(j == 0)
            {
                if (k == 0)
                {
                    qubieName = "Qubie3SidesLeftDownFront";
                }
                else if(k == 2)
                {
                    qubieName = "Qubie3SidesLeftDownBack";
                }
            }
            else if(j == 2)
            {
                if(k == 0)
                {
                    qubieName = "Qubie3SidesLeftUpFront";
                }
                else if(k == 2)
                {
                    qubieName = "Qubie3SidesLeftUpBack";
                }
            }
        }


        return qubieName;
    }


    public void TestFunc()
    {
        Mesh newMesh = new Mesh();
        newMesh.name = "New mesh test";

        Vector3[] verts = new Vector3[4];
        verts[0] = new Vector3(0, 0, 0);
        verts[1] = new Vector3(1, 0, 0);
        verts[2] = new Vector3(0, 0, 1);
        verts[3] = new Vector3(1, 0, 1);
        newMesh.vertices = verts;

        Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

        newMesh.uv = uvs;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;
        newMesh.triangles = triangles;

        newMesh.RecalculateNormals();

        GameObject obj = new GameObject("New_Plane_Fom_Script", typeof(MeshRenderer), typeof(MeshFilter));
        obj.GetComponent<MeshFilter>().mesh = newMesh;
    }

    public class Qubie
    {
        GameObject qubie;

        GameObject frontPlane = new GameObject("frontPlane" , typeof(MeshFilter) , typeof(MeshRenderer));
        GameObject backPlane = new GameObject("backPlane", typeof(MeshFilter), typeof(MeshRenderer));
        GameObject topPlane = new GameObject("topPlane", typeof(MeshFilter), typeof(MeshRenderer));
        GameObject downPlane = new GameObject("downPlane", typeof(MeshFilter), typeof(MeshRenderer));
        GameObject leftPlane = new GameObject("leftPlane", typeof(MeshFilter), typeof(MeshRenderer));
        GameObject rightPlane = new GameObject("rightPlane", typeof(MeshFilter), typeof(MeshRenderer));

        public Qubie(ref GameObject parentObject)
        {
            qubie = parentObject;
            GenerateMeshData();
        }

        public void GenerateMeshData()
        {
            GenerateFrontMeshData();
            GenerateBackMeshData();
            GenerateTopMeshData();
            GenerateDownMeshData();
            GenerateLeftMeshData();
            GenerateRightMeshData();

            frontPlane.transform.parent = qubie.transform;
            backPlane.transform.parent = qubie.transform;
            topPlane.transform.parent = qubie.transform;
            downPlane.transform.parent = qubie.transform;
            leftPlane.transform.parent = qubie.transform;
            rightPlane.transform.parent = qubie.transform;

            frontPlane.GetComponent<Renderer>().material = new Material(Shader.Find("Specular"));
            backPlane.GetComponent<Renderer>().material = new Material(Shader.Find("Specular"));
            topPlane.GetComponent<Renderer>().material = new Material(Shader.Find("Specular"));
            downPlane.GetComponent<Renderer>().material = new Material(Shader.Find("Specular"));
            leftPlane.GetComponent<Renderer>().material = new Material(Shader.Find("Specular"));
            rightPlane.GetComponent<Renderer>().material = new Material(Shader.Find("Specular"));
            frontPlane.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.black);
            backPlane.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.black);
            topPlane.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.black);
            downPlane.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.black);
            leftPlane.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.black);
            rightPlane.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.black);
        }

        public void GenerateFrontMeshData()
        {
            Mesh frontMesh = new Mesh();
            Vector3[] verts = new Vector3[4];
            verts[0] = new Vector3(0, 0, 0);
            verts[1] = new Vector3(1, 0, 0);
            verts[2] = new Vector3(1, 1, 0);
            verts[3] = new Vector3(0, 1, 0);
            frontMesh.vertices = verts;

            Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

            frontMesh.uv = uvs;

            int[] triangles = new int[6];
            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 1;
            triangles[3] = 3;
            triangles[4] = 2;
            triangles[5] = 1;

            frontMesh.triangles = triangles;

            frontMesh.RecalculateNormals();

            frontPlane.GetComponent<MeshFilter>().mesh = frontMesh;
        }

        public void GenerateBackMeshData()
        {
            Mesh backMesh = new Mesh();
            Vector3[] verts = new Vector3[4];
            verts[0] = new Vector3(0, 0, 1);
            verts[1] = new Vector3(1, 0, 1);
            verts[2] = new Vector3(1, 1, 1);
            verts[3] = new Vector3(0, 1, 1);
            backMesh.vertices = verts;

            Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

            backMesh.uv = uvs;

            int[] triangles = new int[6];
            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 3;
            triangles[3] = 3;
            triangles[4] = 1;
            triangles[5] = 2;

            backMesh.triangles = triangles;

            backMesh.RecalculateNormals();

            backPlane.GetComponent<MeshFilter>().mesh = backMesh;
        }

        public void GenerateTopMeshData()
        {
            Mesh topMesh = new Mesh();
            Vector3[] verts = new Vector3[4];
            verts[0] = new Vector3(0, 1, 0);
            verts[1] = new Vector3(1, 1, 0);
            verts[2] = new Vector3(1, 1, 1);
            verts[3] = new Vector3(0, 1, 1);
            topMesh.vertices = verts;

            Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

            topMesh.uv = uvs;

            int[] triangles = new int[6];
            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 1;
            triangles[3] = 3;
            triangles[4] = 2;
            triangles[5] = 1;

            topMesh.triangles = triangles;

            topMesh.RecalculateNormals();

            topPlane.GetComponent<MeshFilter>().mesh = topMesh;
        }

        public void GenerateDownMeshData()
        {
            Mesh downMesh = new Mesh();
            Vector3[] verts = new Vector3[4];
            verts[0] = new Vector3(0, 0, 0);
            verts[1] = new Vector3(1, 0, 0);
            verts[2] = new Vector3(1, 0, 1);
            verts[3] = new Vector3(0, 0, 1);
            downMesh.vertices = verts;

            Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

            downMesh.uv = uvs;

            int[] triangles = new int[6];
            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 3;
            triangles[3] = 3;
            triangles[4] = 1;
            triangles[5] = 2;

            downMesh.triangles = triangles;

            downMesh.RecalculateNormals();

            downPlane.GetComponent<MeshFilter>().mesh = downMesh;
        }

        public void GenerateLeftMeshData()
        {
            Mesh leftMesh = new Mesh();
            Vector3[] verts = new Vector3[4];
            verts[0] = new Vector3(0, 0, 1);
            verts[1] = new Vector3(0, 0, 0);
            verts[2] = new Vector3(0, 1, 0);
            verts[3] = new Vector3(0, 1, 1);
            leftMesh.vertices = verts;

            Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

            leftMesh.uv = uvs;

            int[] triangles = new int[6];
            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 1;
            triangles[3] = 3;
            triangles[4] = 2;
            triangles[5] = 1;

            leftMesh.triangles = triangles;

            leftMesh.RecalculateNormals();

            leftPlane.GetComponent<MeshFilter>().mesh = leftMesh;
        }

        public void GenerateRightMeshData()
        {
            Mesh rightMesh = new Mesh();
            Vector3[] verts = new Vector3[4];
            verts[0] = new Vector3(1, 0, 0);
            verts[1] = new Vector3(1, 0, 1);
            verts[2] = new Vector3(1, 1, 1);
            verts[3] = new Vector3(1, 1, 0);
            rightMesh.vertices = verts;

            Vector2[] uvs = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

            rightMesh.uv = uvs;

            int[] triangles = new int[6];
            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 1;
            triangles[3] = 3;
            triangles[4] = 2;
            triangles[5] = 1;

            rightMesh.triangles = triangles;

            rightMesh.RecalculateNormals();

            rightPlane.GetComponent<MeshFilter>().mesh = rightMesh;
        }

    }


    public void TestFunc2()
    {
        /*
        GameObject newObjectParent = new GameObject("Parent Object");
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    GameObject cubieParent = new GameObject(i + "," + j + "," + k);
                    cubieParent.transform.position = new Vector3(i, j, k);
                    Qubie newQubie = new Qubie(ref cubieParent);
                    cubieParent.transform.parent = newObjectParent.transform;
                }
            }
        }
        */
        GameObject newobject = new GameObject("Some obj");
        Qubie newQubie = new Qubie(ref newobject);
    }
}
