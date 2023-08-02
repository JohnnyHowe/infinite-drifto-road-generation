using System;
using System.Collections;
using System.Collections.Generic;
using Other;
using UnityEngine;

namespace RoadGeneration
{
    public class RoadSection : MonoBehaviour, IRoadSection
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private MeshFilter _boundingMesh;
        [SerializeField] private bool _infiniteHeight = false;
        private RoadSectionShape _shape
        {
            get
            {
                if (_localShapeReal == null) _SetShape();
                return _localShapeReal;
            }
            set
            {
                _localShapeReal = value;
            }
        }
        private RoadSectionShape _localShapeReal;

        private void OnDrawGizmos()
        {
            _shape.DebugDraw();
            _DrawEndPoints();
        }

        private void _SetShape()
        {
            _shape = new RoadSectionShape();
            _shape.Start = TransformData.FromTransform(_startPoint);
            _shape.End = TransformData.FromTransform(_endPoint);
            _shape.Start.Scale = Vector3.one;
            _shape.End.Scale = Vector3.one;
            _shape.SetBoundaryFromMesh(_boundingMesh.mesh, TransformData.FromTransform(_boundingMesh.transform), _shape.Start, _infiniteHeight);
        }

        private void _DrawEndPoints() {
            Vector3 startDir = _startPoint.rotation * Quaternion.Euler(0, 0, 1).eulerAngles;
            DrawArrow.ForGizmo(_startPoint.position - startDir, startDir);
            DrawArrow.ForGizmo(_endPoint.position, _endPoint.rotation * Quaternion.Euler(0, 0, 1).eulerAngles);
        }

        public void AlignByStartPoint(TransformData newStartPoint)
        {
            _shape = _shape.GetTranslatedCopy(newStartPoint);
            transform.position += newStartPoint.Position;
            transform.rotation = newStartPoint.Rotation;
        }

        public IRoadSection Clone()
        {
            GameObject clone = Instantiate(gameObject);
            clone.SetActive(true);
            return clone.GetComponent<IRoadSection>();
        }

        public RoadSectionShape GetShape()
        {
            return _shape;
        }
    }
}