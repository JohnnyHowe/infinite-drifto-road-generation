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

        // yes it is just a 2d extruded shape. it works for this application. TODO - not this
        private FloatRange _heightRange;
        private ConvexShape2D _topology;

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
    }
}