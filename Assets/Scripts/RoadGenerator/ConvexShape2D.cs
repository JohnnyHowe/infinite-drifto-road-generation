using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RoadGeneration
{
    public class ConvexShape2D
    {
        public class Vector2EqualityComparer : IEqualityComparer<Vector2>
        {
            public bool Equals(Vector2 v1, Vector2 v2)
            {
                return Vector2.Distance(v1, v2) < Mathf.Epsilon;
            }

            public int GetHashCode(Vector2 vector)
            {
                return vector.GetHashCode();
            }
        }

        private List<Vector2> _vertices;
        private List<Vector2> _axes;

        public ConvexShape2D(List<Vector2> vertices)
        {
            _vertices = vertices;
            _axes = ConvexHullUtility.GetConvexHullAxes(vertices);
        }

        public List<Vector2> GetAxes()
        {
            return _axes;
        }

        public FloatRange GetProjection(Vector2 axis)
        {
            float min = Mathf.Infinity;
            float max = -Mathf.Infinity;
            foreach (Vector2 vertex in _vertices)
            {
                float vertexProjection = _Project(axis, vertex);
                min = Mathf.Min(min, vertexProjection);
                max = Mathf.Max(max, vertexProjection);
            }
            return new FloatRange(min, max);
        }

        public List<Vector2> GetVertices()
        {
            return _vertices;
        }

        private static float _Project(Vector2 axis, Vector2 point)
        {
            return Vector2.Dot(axis, point) / axis.magnitude;
        }

        public bool Equals(ConvexShape2D other)
        {
            return new HashSet<Vector2>(other.GetVertices()).SetEquals(new HashSet<Vector2>(GetVertices()));
        }

        public override bool Equals(object obj) => this.Equals(obj as ConvexShape2D);

        public override int GetHashCode() => (new HashSet<Vector2>(GetVertices())).GetHashCode();

        public static bool operator ==(ConvexShape2D lhs, ConvexShape2D rhs)
        {
            if (lhs is null)
            {
                if (rhs is null) return true;
                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(ConvexShape2D lhs, ConvexShape2D rhs) => !(lhs == rhs);
    }
}