using System;
using UnityEngine;

namespace RoadGeneration.Tests
{
    public class MockRoadSection : IRoadSection
    {
        public RoadSectionShape LocalShape;
        public RoadSectionShape GlobalShape;

        public void AlignByStartPoint(TransformData newStartPoint)
        {
            // GlobalShape = LocalShape.GetCopyWithStartAlignedTo(newStartPoint);
            throw new NotImplementedException();
        }

        public IRoadSection Clone()
        {
            throw new System.NotImplementedException();
        }

        public RoadSectionShape GetLocalShape()
        {
            return LocalShape;
        }

        public RoadSectionShape GetShape()
        {
            if (GlobalShape == null) return LocalShape;
            return GlobalShape;
        }

        public RoadSectionShape GetShapeRelativeToStart()
        {
            throw new System.NotImplementedException();
        }
    }
}