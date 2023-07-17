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

        public static ConvexBoundary FromMesh(Mesh mesh)
        {
            ConvexBoundary convexBoundary = new ConvexBoundary();

            convexBoundary._heightRange = new FloatRange(mesh.bounds.min.y, mesh.bounds.max.y);
            convexBoundary._topology = new ConvexShape2D(_GetTopology(mesh));

            return convexBoundary;
        }

        private static List<Vector2> _GetTopology(Mesh mesh)
        {
            List<Vector2> topology = new List<Vector2>();

            foreach (Vector3 vertex in mesh.vertices)
            {
                Vector2 squashed = new Vector2(vertex.x, vertex.z);
                topology.Add(squashed);
            }
            return topology.Distinct(new Vector2EqualityComparer()).ToList();
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