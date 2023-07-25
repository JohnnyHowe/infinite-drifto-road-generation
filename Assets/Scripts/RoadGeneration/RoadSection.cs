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
        private RoadSectionShape _localShape
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

        private void Update()
        {
            _SetShape();    // TODO remove this - is just for testing
            _DrawLocalShape();
        }

        private void _DrawLocalShape()
        {
            throw new NotImplementedException();
            // GetShape().Boundary.DrawDebug(Color.green);
            // GetLocalShape().Boundary.DrawDebug(Color.white);
        }

        private void _SetShape()
        {
            _localShape = new RoadSectionShape();
            ConvexBoundary.TransformData meshTransformData = ConvexBoundary.TransformData.FromTransform(_boundingMesh.transform);
            ConvexBoundary.TransformData startTransformData = new ConvexBoundary.TransformData(_startPoint.position, _startPoint.rotation, Vector3.one);
            throw new NotImplementedException();
            // _localShape.SetBoundaryFromMesh(_boundingMesh.mesh, meshTransformData, startTransformData);
            // _localShape.SetStartPointFromTransformLocal(_startPoint);
            // _localShape.SetEndPointFromTransformLocal(_endPoint);
        }

        public void AlignByStartPoint(TransformData newStartPoint)
        {
            Vector3 positionOffset = newStartPoint.Position - GetShape().Start.Position;
            Vector3 rotationOffsetEuler = newStartPoint.Rotation.eulerAngles - GetShape().Start.Rotation.eulerAngles;
            Quaternion rotationOffset = Quaternion.Euler(rotationOffsetEuler);
            transform.position += positionOffset;
            transform.rotation *= rotationOffset;
        }

        public IRoadSection Clone()
        {
            GameObject clone = Instantiate(gameObject);
            clone.SetActive(true);
            return clone.GetComponent<IRoadSection>();
        }

        public RoadSectionShape GetLocalShape()
        {
            return _localShape;
        }

        public RoadSectionShape GetShape()
        {
            // return _localShape.GetCopyWithStartAlignedTo(new TransformData(_startPoint.position, _startPoint.rotation));
            throw new NotImplementedException();
        }

        public RoadSectionShape GetShapeRelativeToStart()
        {
            throw new System.NotImplementedException();
        }
    }
}