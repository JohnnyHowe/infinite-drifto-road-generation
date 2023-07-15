using UnityEngine;

namespace RoadGeneration
{
    public class RoadSection : MonoBehaviour, IRoadSection
    {
        [SerializeField] private MeshFilter _boundingMesh;
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _end;
        private RoadSectionBounds _localBounds;

        private void Awake()
        {
            _localBounds = RoadSectionBounds.FromMesh(_boundingMesh.mesh, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        public IRoadSection GetAlignedTo(Vector3 startPosition, float yAxisRotationStart)
        {
            throw new System.NotImplementedException();
        }

        public RoadSectionBounds GetGlobalBounds()
        {
            return _localBounds.GetCopy(transform.position, transform.rotation, transform.lossyScale);
        }

        public Vector3 GetGlobalEndPosition()
        {
            return _end.position;
        }

        public Quaternion GetGlobalEndRotation()
        {
            return _end.rotation;
        }

        public Vector3 GetLocalStartPosition()
        {
            return _start.localPosition;
        }

        public Quaternion GetLocalStartRotation()
        {
            return _start.rotation;
        }
    }
}