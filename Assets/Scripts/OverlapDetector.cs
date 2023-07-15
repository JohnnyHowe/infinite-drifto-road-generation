using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Other;
using Shapes;

public class OverlapDetector : MonoBehaviour
{
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
            Debug.DrawRay(Vector3.zero, new Vector3(axis.x, 0, axis.y));
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
