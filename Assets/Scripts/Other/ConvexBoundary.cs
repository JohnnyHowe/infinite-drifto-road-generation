using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Other
{
    public class ConvexBoundary
    {
        public class Vector2EqualityComparer : IEqualityComparer<Vector2>
        {
            private const float VECTOR_DUPLICATE_MIN_DISTANCE = 0.001f;
            public bool Equals(Vector2 v1, Vector2 v2)
            {
                return Vector2.Distance(v1, v2) <= VECTOR_DUPLICATE_MIN_DISTANCE;
            }

            public int GetHashCode(Vector2 v)
            {
                return v.GetHashCode();
            }
        }

        public class TransformData
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

            public static TransformData FromTransform(Transform transform)
            {
                return new TransformData(transform.position, transform.rotation, transform.lossyScale);
            }
        }

        // yes it is just a 2d extruded shape. it works for this application. TODO - not this
        internal FloatRange _heightRange;
        internal ConvexShape2D _topology;

        public void DrawDebug(Color lineColor)
        {
            List<Vector2> vertices = _topology.GetVertices();
            foreach (Vector2 start2d in vertices)
            {
                Vector3 start3dLow = new Vector3(start2d.x, _heightRange.Min, start2d.y);
                Vector3 start3dHigh = new Vector3(start2d.x, _heightRange.Max, start2d.y);
                Debug.DrawLine(start3dHigh, start3dLow, lineColor);
                foreach (Vector2 end2d in vertices)
                {
                    Vector3 end3dLow = new Vector3(end2d.x, _heightRange.Min, end2d.y);
                    Vector3 end3dHigh = new Vector3(end2d.x, _heightRange.Max, end2d.y);
                    Debug.DrawLine(start3dLow, end3dLow, lineColor);
                    Debug.DrawLine(start3dHigh, end3dHigh, lineColor);
                }
            }
        }

        public bool DoesOverlapWith(ConvexBoundary other)
        {
            if (!_heightRange.DoesOverlapWith(other._heightRange)) return false;
            if (!_topology.DoesOverlapWith(other._topology)) return false;
            return true;
        }

        /// <summary>
        /// Shorthand for _GetTopology(mesh, Vector3.zero, Quaternion.identity, Vector3.one)
        /// <summary>
        public static ConvexBoundary FromMesh(Mesh mesh)
        {
            return FromMesh(mesh, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        public static ConvexBoundary FromMesh(Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            ConvexBoundary convexBoundary = new ConvexBoundary();

            convexBoundary._heightRange = new FloatRange(
                _TransformPoint(mesh.bounds.min, position, rotation, scale).y,
                _TransformPoint(mesh.bounds.max, position, rotation, scale).y
            );
            convexBoundary._topology = new ConvexShape2D(_GetTopology(mesh, position, rotation, scale));
            return convexBoundary;
        }

        public static ConvexBoundary FromMesh(Mesh mesh, TransformData meshTransformData, TransformData relativeTo)
        {
            return FromMeshVertices(mesh.vertices, meshTransformData, relativeTo);
        }

        public static ConvexBoundary FromMeshVertices(Vector3[] vertices)
        {
            return FromMeshVertices(vertices, new TransformData(Vector3.zero, Quaternion.identity, Vector3.one), new TransformData(Vector3.zero, Quaternion.identity, Vector3.one));
        }

        public static ConvexBoundary FromMeshVertices(Vector3[] vertices, TransformData meshTransformData, TransformData relativeTo)
        {
            ConvexBoundary convexBoundary = new ConvexBoundary();

            List<Vector2> topology = new List<Vector2>();
            float minHeight = Mathf.Infinity;
            float maxHeight = -Mathf.Infinity;
            foreach (Vector3 vertex in vertices)
            {
                Vector3 transformedVertex = _InverseTransformPoint(_TransformPoint(vertex, meshTransformData), relativeTo);
                topology.Add(new Vector2(transformedVertex.x, transformedVertex.z));
                minHeight = Mathf.Min(minHeight, transformedVertex.y);
                maxHeight = Mathf.Max(maxHeight, transformedVertex.y);
            }
            convexBoundary._topology = new ConvexShape2D(topology);
            convexBoundary._heightRange = new FloatRange(minHeight, maxHeight);
            return convexBoundary;
        }

        private static Vector3 _TransformPoint(Vector3 point, TransformData transformData)
        {
            point = new Vector3(point.x * transformData.Scale.x, point.y * transformData.Scale.y, point.z * transformData.Scale.z);
            point = transformData.Rotation * point;
            point += transformData.Position;
            return point;
        }

        private static Vector3 _InverseTransformPoint(Vector3 point, TransformData transformData)
        {
            Matrix4x4 matrix = Matrix4x4.TRS(transformData.Position, transformData.Rotation, transformData.Scale);
            Matrix4x4 inverse = matrix.inverse;
            return inverse.MultiplyPoint3x4(point);
        }

        /// <summary>
        /// Shorthand for _GetTopology(mesh, Vector3.zero, Quaternion.identity, Vector3.one)
        /// </summary>
        private static List<Vector2> _GetTopology(Mesh mesh)
        {
            return _GetTopology(mesh, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        private static List<Vector2> _GetTopology(Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            List<Vector2> topology = new List<Vector2>();
            foreach (Vector3 vertex in mesh.vertices)
            {
                Vector3 transformedVertex = _TransformPoint(vertex, position, rotation, scale);
                Vector2 squashed = new Vector2(transformedVertex.x, transformedVertex.z);
                topology.Add(squashed);
            }
            return topology.Distinct(new Vector2EqualityComparer()).ToList();
        }

        internal static Vector3 _TransformPoint(Vector3 point, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            point = new Vector3(point.x * scale.x, point.y * scale.y, point.z * scale.z);
            point = rotation * point;
            point += position;
            return point;
        }

        /// <summary>
        /// Applies in order of scale, rotation, then position
        /// </summary>
        public ConvexBoundary GetCopyTranslated(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            throw new NotImplementedException();
        }

        public bool Equals(ConvexBoundary other)
        {
            return _heightRange == other._heightRange && _topology == other._topology;
        }
    }
}