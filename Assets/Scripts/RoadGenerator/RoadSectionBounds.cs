using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Other;
using Shapes;

namespace RoadGeneration
{
    public class RoadSectionBounds
    {
        public FloatRange HeightRange;
        public ConvexShape2D Topology;

        public RoadSectionBounds() { }

        public RoadSectionBounds(FloatRange heightRange, ConvexShape2D topology)
        {
            HeightRange = heightRange;
            Topology = topology;
        }

        public static RoadSectionBounds FromMesh(Mesh mesh, Vector3 objectPosition, Quaternion objectRotation, Vector3 objectScale)
        {
            List<Vector2> vertices = new List<Vector2>();
            foreach (Vector3 vertex3d in mesh.vertices)
            {
                Vector3 globalVertex3d = TransformPoint(vertex3d, objectPosition, objectRotation, objectScale);
                Vector2 vertex2d = new Vector2(globalVertex3d.x, globalVertex3d.z);
                vertices.Add(vertex2d);
            }
            List<Vector2> distinct = vertices.Distinct().ToList();
            ConvexShape2D topology = new ConvexShape2D(distinct);

            FloatRange heightRange = new FloatRange(
                TransformPoint(mesh.bounds.min, objectPosition, objectRotation, objectScale).y,
                TransformPoint(mesh.bounds.max, objectPosition, objectRotation, objectScale).y
            );

            return new RoadSectionBounds(heightRange, topology);
        }

        /// <summary>
        /// Get a copy of this translated by the parameters.
        /// Relative to the current offsets of this 
        /// </summary>
        public RoadSectionBounds GetCopy(Vector3 positionOffset, Quaternion rotationOffset, Vector3 scaleMultiplier)
        {
            List<Vector2> vertices = new List<Vector2>();
            foreach (Vector2 vertex2d in Topology.GetVertices())
            {
                Vector3 globalVertex3d = TransformPoint(vertex2d, positionOffset, rotationOffset, scaleMultiplier);
                Vector2 translatedVertex = new Vector2(globalVertex3d.x, globalVertex3d.y);
                vertices.Add(translatedVertex);
            }
            List<Vector2> distinct = vertices.Distinct().ToList();
            ConvexShape2D topology = new ConvexShape2D(distinct);

            FloatRange heightRange = new FloatRange(
                TransformPoint(Vector3.up * HeightRange.Min, positionOffset, rotationOffset, scaleMultiplier).y,
                TransformPoint(Vector3.up * HeightRange.Max, positionOffset, rotationOffset, scaleMultiplier).y
            );

            return new RoadSectionBounds(heightRange, topology);
        }

        public bool WillCauseOverlapWith(RoadSectionBounds other, Vector3 thisOffset, float thisYAxisRotationDegrees)
        {
            RoadSectionBounds bounds = GetOffsetBy(thisOffset, thisYAxisRotationDegrees);

            // Is overlapping in y range?
            if (!_AreOverlapping(bounds.HeightRange, other.HeightRange)) return false;

            // Are shapes overlapping?
            if (!_AreOverlapping(bounds.Topology, other.Topology)) return false;

            // No overlap baby
            return true;
        }

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

        public static Vector3 TransformPoint(Vector3 point, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Vector3 scaledPoint = new Vector3(point.x * scale.x, point.y * scale.y, point.z * scale.z);
            Vector3 rotatedPoint = rotation * scaledPoint;
            Vector3 transformedPoint = rotatedPoint + position;
            return transformedPoint;
        }

        public static Vector2 TransformPoint2D(Vector2 point, Vector2 position, float yAxisRotation)
        {
            Vector2 rotatedPoint = Quaternion.Euler(0f, 0f, yAxisRotation) * point;
            Vector2 transformedPoint = rotatedPoint + position;
            return transformedPoint;
        }

        public static List<Vector2> TransformPoints2D(List<Vector2> pointsToTransform, Vector2 position, float yAxisRotation)
        {
            List<Vector2> points = new List<Vector2>();
            foreach (Vector2 point in pointsToTransform)
            {
                points.Add(TransformPoint2D(point, position, yAxisRotation));
            }
            return points;
        }

        public RoadSectionBounds GetOffsetBy(Vector3 position, float thisYAxisRotationDegrees)
        {
            RoadSectionBounds bounds = new RoadSectionBounds();
            bounds.HeightRange = new FloatRange(HeightRange.Min + position.y, HeightRange.Max + position.y);
            bounds.Topology = new ConvexShape2D(TransformPoints2D(Topology.GetVertices(), position, thisYAxisRotationDegrees));

            return bounds;
        }

        public bool Equals(RoadSectionBounds other)
        {
            return other.HeightRange == HeightRange && other.Topology == Topology;
        }

        public override bool Equals(object obj) => this.Equals(obj as RoadSectionBounds);

        public override int GetHashCode() => (HeightRange, Topology).GetHashCode();

        public static bool operator ==(RoadSectionBounds lhs, RoadSectionBounds rhs)
        {
            if (lhs is null)
            {
                if (rhs is null) return true;
                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(RoadSectionBounds lhs, RoadSectionBounds rhs) => !(lhs == rhs);
    }
}