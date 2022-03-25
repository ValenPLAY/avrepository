using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Renderer))]
public class EmptyBlockGenerator : MonoBehaviour
{
    static public Mesh generatedCubeMesh;
    static public Renderer generatedRenderer;
    public static Material selectedMaterial;

    private void Start()
    {
        generateCube();




        Debug.Log("");
    }

    static public GameObject generateCube()
    {
        GameObject newCube = new GameObject();
        newCube.name = "New Block";
        Mesh generatedCubeMesh = new Mesh();
        newCube.AddComponent<MeshFilter>();
        newCube.AddComponent<MeshRenderer>();
        newCube.GetComponent<MeshFilter>().mesh = generatedCubeMesh;
        newCube.AddComponent<BoxCollider>();

        generatedCubeMesh.vertices = GenVertices();
        generatedCubeMesh.triangles = GenTriangles();
        generatedCubeMesh.name = "Generated Cube";

        Vector2[] uvs = new Vector2[generatedCubeMesh.vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(generatedCubeMesh.vertices[i].x, generatedCubeMesh.vertices[i].z);
        }
        generatedCubeMesh.uv = uvs;


        generatedCubeMesh.RecalculateNormals();

        generatedRenderer = newCube.GetComponent<Renderer>();
        generatedRenderer.material = selectedMaterial;
        return newCube;
    }

    static public Vector3[] GenVertices()
    {
        return new Vector3[]
        {
        //Bottom
        new Vector3(-0.5f, -0.5f, -0.5f), //0
        new Vector3(-0.5f, -0.5f, 0.5f), //1
        new Vector3(0.5f, -0.5f, 0.5f), //2
        new Vector3(0.5f, -0.5f, -0.5f), //3
        //Top
        new Vector3(-0.5f, 0.5f, -0.5f), //4
        new Vector3(-0.5f, 0.5f, 0.5f), //5
        new Vector3(0.5f, 0.5f, 0.5f), //6
        new Vector3(0.5f, 0.5f, -0.5f), //7

        //        //Bottom
        //new Vector3(0.0f, 0.0f, 0.0f), //0
        //new Vector3(0.0f, 0.0f, 1.0f), //1
        //new Vector3(1.0f, 0.0f, 1.0f), //2
        //new Vector3(1.0f, 0.0f, 0.0f), //3
        ////Top
        //new Vector3(0.0f, 1.0f, 0.0f), //4
        //new Vector3(0.0f, 1.0f, 1.0f), //5
        //new Vector3(1.0f, 1.0f, 1.0f), //6
        //new Vector3(1.0f, 1.0f, 0.0f), //7

        };
    }

    static public int[] GenTriangles()
    {
        return new int[] { 2, 1, 0, 3, 2, 0, 4, 5, 6, 4, 6, 7, 0, 4, 7, 0, 7, 3, 3, 7, 6, 3, 6, 2, 2, 6, 5, 2, 5, 1, 1, 5, 4, 1, 4, 0 };
    }
}


