using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CircleMeshCreator : MonoBehaviour
{
    Mesh circleMesh;
    Vector3[] vertices;
    int[] triangles;

    [Range(3, 360)] // Adjust the range based on how many vertices you want in the circle
    public int vertexCount = 36;
    public float radius = 1.0f;

    void Start()
    {
        circleMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = circleMesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[vertexCount];

        for (int i = 0; i < vertexCount; i++)
        {
            float angle = 360f * i / vertexCount; // Calculate the angle for each vertex
            float x = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float z = radius * Mathf.Sin(Mathf.Deg2Rad * angle);
            vertices[i] = new Vector3(x, 0f, z);
        }
    }

    void UpdateMesh()
    {
        circleMesh.Clear();

        // Define triangles for a fan-shaped circle
        triangles = new int[(vertexCount - 2) * 3];
        for (int i = 0; i < vertexCount - 2; i++)
        {
            triangles[i * 3] = 0; // The center vertex
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        circleMesh.vertices = vertices;
        circleMesh.triangles = triangles;

        circleMesh.RecalculateNormals();

        // Optional: Save the circle mesh as an asset
        //AssetDatabase.CreateAsset(circleMesh, "Assets/CircleMesh.asset");
        //AssetDatabase.SaveAssets();
    }
}
