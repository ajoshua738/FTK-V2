using UnityEditor;
using UnityEngine;


[RequireComponent (typeof(MeshFilter))]
public class HexagonMeshCreator : MonoBehaviour
{
    Mesh hexagonMesh;
    Vector3[] vertices;
    int[] triangles;



    void Start()
    {
        // Create a new mesh
        hexagonMesh = new Mesh();

        GetComponent<MeshFilter>().mesh = hexagonMesh;

        CreateShape();
        UpdateMesh();
      
    }

    void CreateShape()
    {
        // Define the vertices of the hexagon
        vertices = new Vector3[7];
        for (int i = 0; i < 6; i++)
        {
            float angle = 60f * i;
            float x = Mathf.Cos(Mathf.Deg2Rad * angle);
            float z = Mathf.Sin(Mathf.Deg2Rad * angle);
            vertices[i] = new Vector3(x, 0f, z);
        }
        // Center vertex
        vertices[6] = Vector3.zero;
    



        // Define the triangles (vertex order matters)
        triangles = new int[18]; // 6 triangles
        for (int i = 0; i < 6; i++)
        {
            triangles[i * 3] = i;
            triangles[i * 3 + 1] = (i + 1) % 6;
            triangles[i * 3 + 2] = 6; // Center vertex
        }
       

    }

    void UpdateMesh()
    {
        hexagonMesh.Clear();

        hexagonMesh.vertices = vertices;
        hexagonMesh.triangles = triangles;

        hexagonMesh.RecalculateNormals();

        AssetDatabase.CreateAsset(hexagonMesh, "Assets/HexagonMesh.asset");
        AssetDatabase.SaveAssets();
    }
}
