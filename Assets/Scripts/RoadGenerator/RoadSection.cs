using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadGeneration
{
    public class RoadSection : MonoBehaviour, IRoadSection
    {
        [SerializeField] private MeshFilter _boundingMesh;

        public RoadSectionBounds GetBounds()
        {
            throw new System.NotImplementedException();
        }

        public Vector3 GetGlobalEndPosition()
        {
            throw new System.NotImplementedException();
        }

        public Quaternion GetGlobalEndRotation()
        {
            throw new System.NotImplementedException();
        }

        public Vector3 GetLocalStartPosition()
        {
            throw new System.NotImplementedException();
        }

        public Quaternion GetLocalStartRotation()
        {
            throw new System.NotImplementedException();
        }
    }
}