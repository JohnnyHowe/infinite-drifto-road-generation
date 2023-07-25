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

        public void SetBoundaryFromMesh(Mesh mesh, TransformData meshGlobalTransform, TransformData handle)
        {
            _boundaryVerticesRelativeToHandle = new List<Vector3>();
            Handle = handle;
            foreach (Vector3 vertexLocalToMesh in mesh.vertices)
            {
                _boundaryVerticesRelativeToHandle.Add(Handle.InverseTransformPoint(meshGlobalTransform.TransformPoint(vertexLocalToMesh)));
            }
        }

        public RoadSectionShape GetTranslatedCopy(TransformData newHandlePosition)
        {
            RoadSectionShape newShape = new RoadSectionShape();
            newShape.Handle = newHandlePosition;

            newShape._boundaryVerticesRelativeToHandle = new List<Vector3>();
            foreach (Vector3 localVertex in _boundaryVerticesRelativeToHandle) {
                newShape._boundaryVerticesRelativeToHandle.Add(newHandlePosition.TransformPoint(localVertex));
            }

            return newShape;
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

            DrawArrow.ForDebug(Start.Position, Start.Rotation * Vector3.forward);
            DrawArrow.ForDebug(End.Position, End.Rotation * Vector3.forward);
        }
    }
}