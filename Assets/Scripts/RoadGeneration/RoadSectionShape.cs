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

            public bool Equals(EndPoint other)
            {
                return Position == other.Position && Rotation == other.Rotation;
            }
        }

        public ConvexBoundary Boundary;
        public EndPoint Start;
        public EndPoint End;
        private Mesh _mesh;

        /// <summary>
        /// Shorthand for SetBoundryFromMesh(mesh, Vector3.zero, Quaternion.identity, Vector3.one)
        /// </summary>
        public void SetBoundaryFromMesh(Mesh mesh)
        {
            SetBoundaryFromMesh(mesh, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        public void SetBoundaryFromMesh(Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            _mesh = mesh;
            Boundary = ConvexBoundary.FromMesh(mesh, position, rotation, scale);
        }

        public void SetStartPointFromTransformLocal(Transform transform)
        {
            Start = _GetEndPointFromTranformLocal(transform);
        }

        public void SetEndPointFromTransformLocal(Transform transform)
        {
            End = _GetEndPointFromTranformLocal(transform);
        }

        private EndPoint _GetEndPointFromTranformLocal(Transform transform)
        {
            return new EndPoint(transform.localPosition, transform.localRotation);
        }

        public RoadSectionShape GetCopyWithStartAlignedTo(EndPoint targetStart)
        {
            Vector3 positionOffset = targetStart.Position - Start.Position;
            Vector3 rotationOffsetEuler = targetStart.Rotation.eulerAngles - Start.Rotation.eulerAngles;
            Quaternion rotationOffset = Quaternion.Euler(rotationOffsetEuler);

            return GetCopyAt(positionOffset, rotationOffset);
        }

        public RoadSectionShape GetCopyAt(Vector3 position, Quaternion rotation)
        {
            RoadSectionShape newShape = new RoadSectionShape();
            newShape.Start = new EndPoint(_TransformPoint(Start.Position, position, rotation, Vector3.one), Start.Rotation * rotation);
            newShape.End = new EndPoint(_TransformPoint(End.Position, position, rotation, Vector3.one), End.Rotation * rotation);
            newShape.SetBoundaryFromMesh(_mesh, position, rotation, Vector3.one);

            return newShape;

        }

        private static Vector3 _TransformPoint(Vector3 point, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            point = new Vector3(point.x * scale.x, point.y * scale.y, point.z * scale.z);
            point = rotation * point;
            point += position;
            return point;
        }

        public bool DoesOverlapWith(RoadSectionShape other)
        {
            return Boundary.DoesOverlapWith(other.Boundary);
        }

        public bool Equals(RoadSectionShape other)
        {
            return (
                Start.Equals(other.Start) &&
                End.Equals(other.End) &&
                Boundary.Equals(other.Boundary)
            );
        }
    }
}