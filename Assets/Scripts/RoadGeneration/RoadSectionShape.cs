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
        private TransformData _handle;
        private List<Vector3> _boundaryVerticesRelativeToHandle;

        public void SetBoundaryFromMesh(Mesh mesh, TransformData meshGlobalTransform, TransformData handle)
        {
            _boundaryVerticesRelativeToHandle = new List<Vector3>();
            _handle = handle;
            foreach (Vector3 vertexLocalToMesh in mesh.vertices)
            {
                _boundaryVerticesRelativeToHandle.Add(_handle.InverseTransformPoint(meshGlobalTransform.TransformPoint(vertexLocalToMesh)));
            }
        }

        public RoadSectionShape GetTranslatedCopy(TransformData newHandlePosition)
        {
            throw new NotImplementedException();
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