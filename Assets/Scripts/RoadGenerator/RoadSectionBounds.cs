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

        public static Vector3 TransformPoint(Vector3 point, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Vector3 scaledPoint = new Vector3(point.x * scale.x, point.y * scale.y, point.z * scale.z);
            Vector3 rotatedPoint = rotation * scaledPoint;
            Vector3 transformedPoint = rotatedPoint + position;
            return transformedPoint;
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