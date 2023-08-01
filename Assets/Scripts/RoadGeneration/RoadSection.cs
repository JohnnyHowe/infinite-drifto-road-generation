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

        private void Update() {
            _shape.DebugDraw();
        }

        private void _SetShape()
        {
            _shape = new RoadSectionShape();
            _shape.SetBoundaryFromMesh(_boundingMesh.mesh, TransformData.FromTransform(_boundingMesh.transform), TransformData.FromTransform(_startPoint));
            _shape.Start = TransformData.FromTransform(_startPoint);
            _shape.End = TransformData.FromTransform(_endPoint);
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