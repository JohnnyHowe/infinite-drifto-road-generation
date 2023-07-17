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

        public void AlignByStartPoint(RoadSectionShape.EndPoint newStartPoint)
        {
            throw new System.NotImplementedException();
        }

        public RoadSectionShape GetLocalShape()
        {
            throw new System.NotImplementedException();
        }

        public RoadSectionShape GetShape()
        {
            throw new System.NotImplementedException();
        }
    }
}