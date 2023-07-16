using System;
using System.Collections.Generic;
using UnityEngine;

namespace Other
{
    public class ConvexBoundary
    {
        // yes it is just a 2d extruded shape. it works for this application. TODO - not this
        private FloatRange _heightRange;
        private ConvexShape2D _topology;

        public bool DoesOverlapWith(ConvexBoundary other)
        {
            throw new NotImplementedException();
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
            // List<Vector2> topology = new List<Vector2>();
            // foreach (Vector3 vertic)
            throw new NotImplementedException();
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