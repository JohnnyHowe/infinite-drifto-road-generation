using System;
using System.Collections;
using System.Collections.Generic;
using Other;
using UnityEngine;

namespace RoadGeneration
{
    /// <summary>
    /// Describes the shape of a road section
    /// Contains logic for bounding areas, and start and end position alignment.
    /// </summary>
    public class RoadSectionShape
    {
        [System.Serializable]
        public struct EndPoint
        {
            public Vector3 Position;
            public Quaternion Rotation;

            public EndPoint(Vector3 position, Quaternion rotation)
            {
                Position = position;
                Rotation = rotation;
            }
        }

        public ConvexBoundary Boundary;
        public EndPoint Start;
        public EndPoint End;

        /// <summary>
        /// Shorthand for SetBoundryFromMesh(mesh, Vector3.zero, Quaternion.identity, Vector3.one)
        /// </summary>
        public void SetBoundaryFromMesh(Mesh mesh)
        {
            SetBoundaryFromMesh(mesh, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        public void SetBoundaryFromMesh(Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            throw new NotImplementedException();
        }

        public void SetStartPointFromTransform()
        {
            throw new NotImplementedException();
        }

        public void SetEndPointFromTransform()
        {
            throw new NotImplementedException();
        }

        public RoadSectionShape GetCopyWithStartAlignedTo(EndPoint start)
        {
            throw new NotImplementedException();
        }

        public bool DoesOverlapWith(RoadSectionShape other)
        {
            throw new NotImplementedException();
        }
    }
}