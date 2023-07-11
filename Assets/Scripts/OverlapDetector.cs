using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class OverlapDetector : MonoBehaviour
{
    private struct ConvexShape2D
    {
        public List<Vector2> OrderedVertices;

        public ConvexShape2D(List<Vector2> orderedVertices)
        {
            OrderedVertices = orderedVertices;
        }

        public List<Vector2> GetAxes()
        {
            // TODO ignore internal connections
            List<Vector2> tangents = new List<Vector2>();
            foreach (Vector2 vertex1 in OrderedVertices)
            {
                foreach (Vector2 vertex2 in OrderedVertices)
                {
                    Vector2 axis = vertex1 - vertex2;
                    if (axis.magnitude == 0) continue;
                    tangents.Add(axis.normalized);
                }
            }
            return tangents;
        }

        public FloatRange GetProjection(Vector2 axis)
        {
            float min = Mathf.Infinity;
            float max = -Mathf.Infinity;
            foreach (Vector2 vertex in OrderedVertices)
            {
                float vertexProjection = _Project(axis, vertex);
                min = Mathf.Min(min, vertexProjection);
                max = Mathf.Max(max, vertexProjection);
            }
            return new FloatRange(min, max);
        }

        private static float _Project(Vector2 axis, Vector2 point)
        {
            return Vector2.Dot(axis, point) / axis.magnitude;
        }
    }

    private struct FloatRange
    {
        private float _min;
        private float _max;

        public float Min
        {
            get => _min;
        }
        public float Max
        {
            get => _max;
        }

        public FloatRange(float value1, float value2)
        {
            _min = Mathf.Min(value1, value2);
            _max = Mathf.Max(value1, value2);
        }
    }

    [SerializeField] private MeshFilter _object1BoundingMesh;
    [SerializeField] private MeshFilter _object2BoundingMesh;
    [SerializeField] private GameObject _enableOnDetection;

    private void Update()
    {
        _enableOnDetection.SetActive(_AreOverlapping(_object1BoundingMesh, _object2BoundingMesh));
    }

    private bool _AreOverlapping(MeshFilter object1, MeshFilter object2)
    {
        // Overlapping in y axis?
        FloatRange object1RangeY = _GetYRange(object1);
        FloatRange object2RangeY = _GetYRange(object2);
        if (!_AreOverlapping(object1RangeY, object2RangeY)) return false;

        // Overlapping 2D?
        ConvexShape2D object1Shape = _GetShape2D(object1);
        ConvexShape2D object2Shape = _GetShape2D(object2);
        return _AreOverlapping(object1Shape, object2Shape);
    }

    /// <summary>
    /// Separating axis theorem baby
    /// </summary>
    private bool _AreOverlapping(ConvexShape2D shape1, ConvexShape2D shape2)
    {
        // for each axis of both objects
        IEnumerable<Vector2> axes = shape1.GetAxes().Concat(shape2.GetAxes()).Distinct().ToList();
        foreach (Vector2 axis in axes)
        {
            // get the projection of each object on the axis
            FloatRange shape1ProjectionRange = shape1.GetProjection(axis);
            FloatRange shape2ProjectionRange = shape2.GetProjection(axis);

            // if there is a gap, return false - there is no overlap
            if (!_AreOverlapping(shape1ProjectionRange, shape2ProjectionRange)) return false;
        }

        // no gap found, must be touching
        return true;
    }

    private bool _AreOverlapping(FloatRange range1, FloatRange range2)
    {
        return range1.Min < range2.Max && range1.Max > range2.Min;
    }

    private FloatRange _GetYRange(MeshFilter object1)
    {
        return new FloatRange(
            object1.transform.TransformPoint(object1.mesh.bounds.min).y,
            object1.transform.TransformPoint(object1.mesh.bounds.max).y
        );
    }

    private ConvexShape2D _GetShape2D(MeshFilter object1)
    {
        List<Vector2> vertices = new List<Vector2>();
        foreach (Vector3 vertex3d in object1.mesh.vertices)
        {
            Vector3 globalVertex3d = object1.transform.TransformPoint(vertex3d);
            Vector2 vertex2d = new Vector2(globalVertex3d.x, globalVertex3d.z);
            vertices.Add(vertex2d);
        }
        List<Vector2> distinct = vertices.Distinct().ToList();
        return new ConvexShape2D(distinct);
    }
}
