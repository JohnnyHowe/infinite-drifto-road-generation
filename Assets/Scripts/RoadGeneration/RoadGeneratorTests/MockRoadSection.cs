using UnityEngine;

namespace RoadGeneration.Tests
{
    public class MockRoadSection : IRoadSection
    {
        public RoadSectionShape LocalShape;

        public RoadSectionShape GetLocalShape()
        {
            throw new System.NotImplementedException();
        }
    }
}