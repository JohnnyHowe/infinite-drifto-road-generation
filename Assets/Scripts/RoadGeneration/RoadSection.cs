using System.Collections;
using System.Collections.Generic;
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

        private void _SetShape()
        {
            _localShape = new RoadSectionShape();
            // Vector3 position = transform.InverseTransformPoint()
            _localShape.SetBoundaryFromMesh(_boundingMesh.mesh, _boundingMesh.transform.position, _boundingMesh.transform.rotation, _boundingMesh.transform.lossyScale);
            _localShape.SetStartPointFromTransformLocal(_startPoint);
            _localShape.SetEndPointFromTransformLocal(_endPoint);
        }

        public void AlignByStartPoint(RoadSectionShape.EndPoint newStartPoint)
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
            return _localShape.GetCopyAt(transform.position, transform.rotation);
        }
    }
}