using System.Collections.Generic;
using UnityEngine;

namespace RoadGeneration
{
    public struct ConvexShape2D
    {
        public List<Vector2> OrderedVertices;

        public ConvexShape2D(List<Vector2> orderedVertices)
        {
            OrderedVertices = orderedVertices;
        }

        public List<Vector2> GetAxes()
        {
            // TODO ignore internal connections
            List<Vector2> tangents = new List<Vector2>();
            foreach (Vector2 vertex1 in OrderedVertices)
            {
                foreach (Vector2 vertex2 in OrderedVertices)
                {
                    Vector2 tangent = vertex1 - vertex2;
                    if (tangent.normalized.magnitude == 0) continue;

                    Vector2 axis = new Vector2(-tangent.y, tangent.x);
                    tangents.Add(axis.normalized);
                }
            }
            return tangents;
        }

        public FloatRange GetProjection(Vector2 axis)
        {
            float min = Mathf.Infinity;
            float max = -Mathf.Infinity;
            foreach (Vector2 vertex in OrderedVertices)
            {
                float vertexProjection = _Project(axis, vertex);
                min = Mathf.Min(min, vertexProjection);
                max = Mathf.Max(max, vertexProjection);
            }
            return new FloatRange(min, max);
        }

        private static float _Project(Vector2 axis, Vector2 point)
        {
            return Vector2.Dot(axis, point) / axis.magnitude;
        }
    }
}