using System;
using System.Collections.Generic;
using Other;
using UnityEngine;

public class MeshBoundsDemo : MonoBehaviour
{
    [SerializeField] private Transform _relativeTo;
    [SerializeField] private MeshFilter _mesh;

    private void Update()
    {
        List<Vector3> transformedVertices = new List<Vector3>();
        foreach (Vector3 vertex in _mesh.mesh.vertices)
        {
            transformedVertices.Add(_relativeTo.InverseTransformPoint(_mesh.transform.TransformPoint(vertex)));
        }

        foreach (Vector3 v1 in transformedVertices)
        {
            foreach (Vector3 v2 in transformedVertices)
            {
                Debug.DrawLine(v1, v2);
            }
        }
    }
}
