using UnityEngine;

namespace RoadGeneration
{
    [System.Serializable]
    public struct TransformData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public TransformData(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        /// <summary>
        /// Transform point from local space to world space
        /// </summary>
        public Vector3 TransformPoint(Vector3 point)
        {
            point = new Vector3(point.x * Scale.x, point.y * Scale.y, point.z * Scale.z);
            point = Rotation * point;
            point += Position;
            return point;
        }

        /// <summary>
        /// Transform point from world space to local space
        /// </summary>
        public Vector3 InverseTransformPoint(Vector3 point)
        {
            Matrix4x4 matrix = Matrix4x4.TRS(Position, Rotation, Scale);
            Matrix4x4 inverse = matrix.inverse;
            return inverse.MultiplyPoint3x4(point);
        }

        public bool Equals(TransformData other)
        {
            return Position == other.Position && Rotation == other.Rotation && Scale == other.Scale;
        }

        public static TransformData FromTransform(Transform transform)
        {
            return new TransformData(transform.position, transform.rotation, transform.lossyScale);
        }
    }
}