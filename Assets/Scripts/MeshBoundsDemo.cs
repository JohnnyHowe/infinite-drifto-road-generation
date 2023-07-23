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
            Vector3 globalPoint = _GetGlobalMeshPoint(vertex, _mesh.transform.position, _mesh.transform.rotation, _mesh.transform.lossyScale);
            Vector3 pointRelativeTo = _GetPointRelativeTo(globalPoint, _relativeTo.position, _relativeTo.rotation, _relativeTo.lossyScale);
            transformedVertices.Add(pointRelativeTo);
        }

        foreach (Vector3 v1 in transformedVertices)
        {
            foreach (Vector3 v2 in transformedVertices)
            {
                Debug.DrawLine(v1, v2);
            }
        }
    }

    /// <summary>
    /// Transform.TransformPoint
    /// </summary>
    private Vector3 _GetGlobalMeshPoint(Vector3 point, Vector3 globalMeshPosition, Quaternion globalMeshRotation, Vector3 globalMeshScale)
    {
        point = new Vector3(point.x * globalMeshScale.x, point.y * globalMeshScale.y, point.z * globalMeshScale.z);
        point = globalMeshRotation * point;
        point += globalMeshPosition;
        return point;
    }

    /// <summary>
    /// Transform.InverseTransformPoint
    /// </summary>
    private Vector3 _GetPointRelativeTo(Vector3 point, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Matrix4x4 matrix = Matrix4x4.TRS(position, rotation, scale);
        Matrix4x4 inverse = matrix.inverse;
        return inverse.MultiplyPoint3x4(point);
    }
}
