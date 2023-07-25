using System;
using System.Collections.Generic;
using Other;
using UnityEngine;

public class MeshBoundsDemo : MonoBehaviour
{
    [SerializeField] private Transform _relativeTo;
    [SerializeField] private MeshFilter _mesh;
    [SerializeField] private Color _lineColor = Color.blue;

    private void Update()
    {
        ConvexBoundary.TransformData meshTransformData = ConvexBoundary.TransformData.FromTransform(_mesh.transform);
        ConvexBoundary.TransformData relativeTo = ConvexBoundary.TransformData.FromTransform(_relativeTo);
        ConvexBoundary localBoundary = ConvexBoundary.FromMesh(_mesh.mesh, meshTransformData, relativeTo);
        localBoundary.DrawDebug(_lineColor);
    }
}
