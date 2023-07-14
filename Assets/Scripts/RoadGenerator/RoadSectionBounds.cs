using UnityEngine;

namespace RoadGeneration
{
    public class RoadSectionBounds
    {
        public FloatRange GlobalYRange;
        public ConvexShape2D Topology;

        public static RoadSectionBounds FromMesh(Mesh mesh, Vector3 positionOffset, Quaternion rotationOffset, Vector3 scaleOffset)
        {
            return null;
        }

        public bool Equals(RoadSectionBounds other)
        {
            return other.GlobalYRange == GlobalYRange && other.Topology == Topology;
        }

        public override bool Equals(object obj) => this.Equals(obj as RoadSectionBounds);

        public override int GetHashCode() => (GlobalYRange, Topology).GetHashCode();

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