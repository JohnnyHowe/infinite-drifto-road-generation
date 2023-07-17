using UnityEngine;

namespace RoadGeneration.Tests
{
    public class MockRoadSection : IRoadSection
    {
        public RoadSectionShape LocalShape;

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