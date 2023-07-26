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
        public TransformData Start;
        public TransformData End;
        public TransformData Handle;
        public List<Vector3> _boundaryVerticesRelativeToHandle;
        private FloatRange _heightRange;
        private ConvexShape2D _topology;

        public void SetBoundaryFromMesh(Mesh mesh, TransformData meshGlobalTransform, TransformData handle)
        {
            _boundaryVerticesRelativeToHandle = new List<Vector3>();
            Handle = handle;
            foreach (Vector3 vertexLocalToMesh in mesh.vertices)
            {
                _boundaryVerticesRelativeToHandle.Add(Handle.InverseTransformPoint(meshGlobalTransform.TransformPoint(vertexLocalToMesh)));
            }
            RecalculateCollisionBoundaries();
        }

        public RoadSectionShape GetTranslatedCopy(TransformData newHandlePosition)
        {
            RoadSectionShape newShape = new RoadSectionShape();
            newShape.Handle = newHandlePosition;
            newShape._boundaryVerticesRelativeToHandle = new List<Vector3>();
            foreach (Vector3 localVertex in _boundaryVerticesRelativeToHandle)
            {
                newShape._boundaryVerticesRelativeToHandle.Add(newHandlePosition.TransformPoint(localVertex));
            }

            // TODO translate start and end points
            newShape.Start = newHandlePosition;
            newShape.End = newHandlePosition.TransformPoint(Start.InverseTransformPoint(End));

            newShape.RecalculateCollisionBoundaries();
            return newShape;
        }

        public void RecalculateCollisionBoundaries()
        {
            List<Vector2> topology = new List<Vector2>();
            float _minHeight = Mathf.Infinity;
            float _maxHeight = -Mathf.Infinity;
            foreach (Vector3 vertex in _boundaryVerticesRelativeToHandle)
            {
                _minHeight = Mathf.Min(vertex.y, _minHeight);
                _maxHeight = Mathf.Max(vertex.y, _maxHeight);
                topology.Add(new Vector2(vertex.x, vertex.z));
            }
            _topology = new ConvexShape2D(topology);
            _heightRange = new FloatRange(_minHeight, _maxHeight);
        }

        public bool DoesOverlapWith(RoadSectionShape other)
        {
            if (!_heightRange.DoesOverlapWith(other._heightRange)) return false;
            if (!_topology.DoesOverlapWith(other._topology)) return false;
            return true;
        }

        public void DebugDraw()
        {
            foreach (Vector3 vertex1 in _boundaryVerticesRelativeToHandle)
            {
                foreach (Vector3 vertex2 in _boundaryVerticesRelativeToHandle)
                {
                    Debug.DrawLine(vertex1, vertex2);
                }
            }

            List<Vector2> topology = _topology.GetVertices();
            foreach (Vector2 vertex1 in topology)
            {
                Debug.DrawLine(new Vector3(vertex1.x, _heightRange.Min, vertex1.y), new Vector3(vertex1.x, _heightRange.Max, vertex1.y), Color.red);
                foreach (Vector2 vertex2 in topology)
                {
                    Debug.DrawLine(new Vector3(vertex1.x, _heightRange.Min, vertex1.y), new Vector3(vertex2.x, _heightRange.Min, vertex2.y), Color.red);
                    Debug.DrawLine(new Vector3(vertex1.x, _heightRange.Max, vertex1.y), new Vector3(vertex2.x, _heightRange.Max, vertex2.y), Color.red);
                }
            }
            DrawArrow.ForDebug(Start.Position, Start.Rotation * Vector3.forward);
            DrawArrow.ForDebug(End.Position, End.Rotation * Vector3.forward);
        }
    }
}