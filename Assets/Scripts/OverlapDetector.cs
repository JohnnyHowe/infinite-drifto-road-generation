using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapDetector : MonoBehaviour
{
    private struct Shape2D
    {
        public List<Vector2> OrderedVertices;

        public Shape2D(List<Vector2> orderedVertices)
        {
            OrderedVertices = orderedVertices;
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

    private void Update()
    {
        Debug.Log(_AreOverlapping(_object1BoundingMesh, _object2BoundingMesh));
    }

    private bool _AreOverlapping(MeshFilter object1, MeshFilter object2)
    {
        // Overlapping in y axis?
        FloatRange object1RangeY = _GetYRange(object1);
        FloatRange object2RangeY = _GetYRange(object2);
        if (!_AreOverlapping(object1RangeY, object2RangeY)) return false;

        return true;
        // Overlapping 2D?
        Shape2D object1Shape = _GetShape2D(object1);
        Shape2D object2Shape = _GetShape2D(object2);
        return _AreOverlapping(object1Shape, object2Shape);
    }

    private bool _AreOverlapping(Shape2D shape1, Shape2D shape2)
    {
        throw new NotImplementedException();
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

    private Shape2D _GetShape2D(MeshFilter object1)
    {
        throw new NotImplementedException();
    }
}
