using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Ugly ass code
/// Definitely not AI generated
/// Does it really matter? It's tested good
/// </summary>
public static class ConvexHullUtility
{
    private class Vector2EqualityComparer : IEqualityComparer<Vector2>
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

    public static List<Vector2> GetConvexHullAxes(List<Vector2> points)
    {
        List<Vector2> axes = new List<Vector2>();
        foreach (Vector2 tangent in GetConvexHullTangents(points)) {
            Vector2 axis = new Vector2(-tangent.y, tangent.x);
            if (axis.x < 0) axis = -axis;
            if (axis.y < 0) axis = -axis;
            axes.Add(axis);
        }
        return axes;
    }

    public static List<Vector2> GetConvexHullTangents(List<Vector2> points)
    {
        List<Vector2> tangents = new List<Vector2>();
        foreach ((Vector2, Vector2) edge in GetConvexHull(points))
        {
            Vector2 tangent = edge.Item1 - edge.Item2;

            // Ensure up (and if horizontal, right)
            if (tangent.x < 0) tangent = -tangent;
            if (tangent.y < 0) tangent = -tangent;

            tangents.Add(tangent.normalized);
        }
        return tangents.Distinct(new Vector2EqualityComparer()).ToList(); ;
    }

    private static List<(Vector2, Vector2)> GetConvexHull(List<Vector2> points)
    {
        if (points.Count < 3)
            return new List<(Vector2, Vector2)>();

        List<Vector2> hullPoints = new List<Vector2>();

        // Find the leftmost point
        int leftmostIndex = 0;
        for (int i = 1; i < points.Count; i++)
        {
            if (points[i].x < points[leftmostIndex].x)
                leftmostIndex = i;
        }

        int currentIndex = leftmostIndex;
        int nextIndex;

        do
        {
            hullPoints.Add(points[currentIndex]);
            nextIndex = (currentIndex + 1) % points.Count;

            for (int i = 0; i < points.Count; i++)
            {
                if (IsCounterClockwise(points[currentIndex], points[i], points[nextIndex]))
                    nextIndex = i;
            }

            currentIndex = nextIndex;
        }
        while (currentIndex != leftmostIndex);

        return GetEdges(hullPoints);
    }

    private static bool IsCounterClockwise(Vector2 a, Vector2 b, Vector2 c)
    {
        float crossProduct = (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x);
        return crossProduct > 0;
    }

    private static List<(Vector2, Vector2)> GetEdges(List<Vector2> points)
    {
        List<(Vector2, Vector2)> edges = new List<(Vector2, Vector2)>();

        int numPoints = points.Count;

        for (int i = 0; i < numPoints; i++)
        {
            Vector2 currentPoint = points[i];
            Vector2 nextPoint = points[(i + 1) % numPoints]; // Wrap around to the first point

            edges.Add((currentPoint, nextPoint));
        }

        return edges;
    }
}
