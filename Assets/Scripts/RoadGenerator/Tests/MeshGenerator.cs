using UnityEngine;

public class MeshGeneratorUtility
{
    public static Mesh GenerateUnitCubeMesh()
    {
        // Create a new empty mesh
        Mesh cubeMesh = new Mesh();

        // Define the vertices of the cube
        Vector3[] vertices =
        {
            new Vector3(-0.5f, -0.5f, -0.5f),  // Bottom-left-back
            new Vector3(0.5f, -0.5f, -0.5f),   // Bottom-right-back
            new Vector3(0.5f, -0.5f, 0.5f),    // Bottom-right-front
            new Vector3(-0.5f, -0.5f, 0.5f),   // Bottom-left-front
            new Vector3(-0.5f, 0.5f, -0.5f),   // Top-left-back
            new Vector3(0.5f, 0.5f, -0.5f),    // Top-right-back
            new Vector3(0.5f, 0.5f, 0.5f),     // Top-right-front
            new Vector3(-0.5f, 0.5f, 0.5f)     // Top-left-front
        };

        // Define the triangles of the cube
        int[] triangles =
        {
            // Bottom face
            0, 2, 1,
            0, 3, 2,
            // Top face
            4, 5, 6,
            4, 6, 7,
            // Front face
            3, 6, 2,
            3, 7, 6,
            // Back face
            0, 1, 5,
            0, 5, 4,
            // Left face
            0, 7, 3,
            0, 4, 7,
            // Right face
            1, 2, 6,
            1, 6, 5
        };

        // Assign the vertices, triangles, and normals to the mesh
        cubeMesh.vertices = vertices;
        cubeMesh.triangles = triangles;

        // Recalculate the bounds and normals of the mesh
        cubeMesh.RecalculateBounds();
        cubeMesh.RecalculateNormals();

        return cubeMesh;
    }
}
