using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RoadGeneration
{
    public class RoadSectionBounds
    {
        public FloatRange GlobalYRange;
        public ConvexShape2D GlobalTopology;

        public RoadSectionBounds() { }

        public RoadSectionBounds(FloatRange globalYRange, ConvexShape2D globalTopology)
        {
            GlobalYRange = globalYRange;
            GlobalTopology = globalTopology;
        }

        public static RoadSectionBounds FromMesh(Mesh mesh, Vector3 positionOffset, Quaternion rotationOffset, Vector3 scaleOffset)
        {
            List<Vector2> vertices = new List<Vector2>();
            foreach (Vector3 vertex3d in mesh.vertices)
            {
                Vector3 globalVertex3d = TransformPoint(vertex3d, positionOffset, rotationOffset, scaleOffset);
                Vector2 vertex2d = new Vector2(globalVertex3d.x, globalVertex3d.z);
                vertices.Add(vertex2d);
            }
            List<Vector2> distinct = vertices.Distinct().ToList();
            ConvexShape2D topology = new ConvexShape2D(distinct);

            FloatRange heightRange = new FloatRange(
                TransformPoint(mesh.bounds.min, positionOffset, rotationOffset, scaleOffset).y,
                TransformPoint(mesh.bounds.max, positionOffset, rotationOffset, scaleOffset).y
            );

            return new RoadSectionBounds(heightRange, topology);
        }

        public bool WillCauseOverlapWith(RoadSectionBounds other, Vector3 thisOffset, float thisYAxisRotationDegrees)
        {
            RoadSectionBounds bounds = GetOffsetBy(thisOffset, thisYAxisRotationDegrees);

            // Is overlapping in y range?
            if (!_AreOverlapping(bounds.GlobalYRange, other.GlobalYRange)) return false;

            // Are shapes overlapping?
            if (!_AreOverlapping(bounds.GlobalTopology, other.GlobalTopology)) return false;

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
            foreach (Vector2 point in pointsToTransform) {
                points.Add(TransformPoint2D(point, position, yAxisRotation));
            }
            return points;
        }

        public RoadSectionBounds GetOffsetBy(Vector3 position, float thisYAxisRotationDegrees)
        {
            RoadSectionBounds bounds = new RoadSectionBounds();
            bounds.GlobalYRange = new FloatRange(GlobalYRange.Min + position.y, GlobalYRange.Max + position.y);
            bounds.GlobalTopology = new ConvexShape2D(TransformPoints2D(GlobalTopology.GetVertices(), position, thisYAxisRotationDegrees));

            return bounds;
        }

        public bool Equals(RoadSectionBounds other)
        {
            return other.GlobalYRange == GlobalYRange && other.GlobalTopology == GlobalTopology;
        }

        public override bool Equals(object obj) => this.Equals(obj as RoadSectionBounds);

        public override int GetHashCode() => (GlobalYRange, GlobalTopology).GetHashCode();

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